using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace NeoSmart.Localization
{
    public class StringDump
    {
        private Dictionary<string, Dictionary<string, string>> _strings = new Dictionary<string, Dictionary<string, string>>();
        private List<StringCollection> _stringCollections = new List<StringCollection>();
        private List<string> _failedControls = new List<string>(); 

        private string _currentForm;

        private static readonly List<Type> ValidCollections = new List<Type>
                                                {
                                                    typeof (Control.ControlCollection),
                                                    typeof (TableLayoutControlCollection),
                                                };

        private static readonly List<String> ValidNames = new List<string>
                                                {
                                                    @"Controls"
                                                };

        private static readonly List<string> MicrosoftTokens = new List<string>
                                                {
                                                    BitConverter.ToString(new byte[] {0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89}),
                                                    BitConverter.ToString(new byte[] {0x31, 0xbf, 0x38, 0x56, 0xad, 0x36, 0x4e, 0x35}),
                                                    BitConverter.ToString(new byte[] {0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0x0a, 0x3a})
                                                };

        public IEnumerable<string> FailedControls
        {
            get { return _failedControls; }
        }

        public bool ErrorsEncountered
        {
            get { return _failedControls.Count != 0; }
        }

        public static bool IsFrameworkType(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            byte[] publicKeyToken = type.Assembly.GetName().GetPublicKeyToken();

            return publicKeyToken != null && publicKeyToken.Length == 8 && MicrosoftTokens.Contains(BitConverter.ToString(publicKeyToken));
        }

        private Control CreateControlWithoutConstructor(Type asmType)
        {
            try
            {
                Control control = (Form) FormatterServices.GetUninitializedObject(asmType);

                //Huge hack coming through: we'll load the constructor from the base type (form), then call it on the derived type
                //Then we'll manually call InitializeComponent... if it exists :)

                var ctor = typeof (Form).GetConstructor(new Type[] {});
                Debug.Assert(ctor != null);

                ctor.Invoke(control, new object[] {});

                var initComponent = asmType.GetMethod(@"InitializeComponent",
                                                      BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default | BindingFlags.NonPublic);
                if (initComponent == null)
                    return null;

                initComponent.Invoke(control, new object[] {});

                return control;
            }
            catch
            {
                return null;
            }
        }

        private void LoadStrings(IEnumerable<Type> types)
        {
            foreach (var asmType in types)
            {
                if (!asmType.IsSubclassOf(typeof (Control)))
                    continue;

                PropertyInfo controls = null;
                foreach (var property in asmType.GetProperties())
                {
                    //We can't just use GetProperty() because it sometimes throws AmbiguousMatchException for unknown reasons
                    //It's not like there can be two properties called "Controls" for a form now, is there? WTF, Microsoft?
                    if (ValidNames.Contains(property.Name))
                    {
                        controls = property;
                        break;
                    }
                }

                if (controls == null)
                    continue;

                if (!ValidCollections.Contains(controls.PropertyType))
                    continue;

                var isForm = asmType.IsSubclassOf(typeof (Form));

                if (isForm)
                {
                    _currentForm = asmType.Name;
                    if (_strings.ContainsKey(_currentForm))
                    {
                        //This form has already been traversed
                        continue;
                    }

                    var stringCollection = new StringCollection(_currentForm);
                    _stringCollections.Add(stringCollection);
                    _strings[_currentForm] = stringCollection.StringsTable;
                }

                Control control;

                if (IsFrameworkType(asmType))
                {
                    //Only try calling the default constructor on MS-provided types
                    try
                    {
                        control = (Control) Activator.CreateInstance(asmType, new object[] {});
                    }
                    catch (MissingMethodException)
                    {
                        if (!isForm)
                        {
                            //We can't do anything about it, as we must hard-code each known type
                            _failedControls.Add(asmType.Name);
                            continue;
                        }

                        //Create the object without initializing it
                        control = CreateControlWithoutConstructor(asmType);
                    }
                }
                else
                {
                    control = CreateControlWithoutConstructor(asmType);
                }

                if (control == null)
                {
                    _failedControls.Add(asmType.Name);
                    continue; //Give up already!
                }

                foreach (var item in (IEnumerable) controls.GetValue(control, null))
                {
                    if (!(item is Control))
                        continue;

                    var subControl = item as Control;

                    if (!string.IsNullOrEmpty(subControl.Text))
                    {
                        _strings[_currentForm].Add(subControl.Name, subControl.Text);
                    }

                    LoadStrings(new[] {subControl.GetType()});
                }
            }
        }

        public void AddControlCollectionTypes(IEnumerable<Type> propertyTypes)
        {
            ValidCollections.AddRange(propertyTypes);
        }

        public void AddControlCollectionNames(IEnumerable<string> names)
        {
            ValidNames.AddRange(names);
        }

        public List<StringCollection> GetAssemblyStrings(string path)
        {
            _strings = new Dictionary<string, Dictionary<string, string>>();
            _stringCollections = new List<StringCollection>();
            _failedControls = new List<string>();

            var assembly = Assembly.LoadFrom(path);

            LoadStrings(assembly.GetTypes());

            return _stringCollections;
        }
    }
}
