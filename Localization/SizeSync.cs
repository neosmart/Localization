using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NeoSmart.Localization
{
    public static class SizeSync
    {
        public static void Sync(IEnumerable<Control> controls, bool syncWidth, bool syncHeight)
        {
            foreach (var i in controls)
            {
                var i1 = i;
                foreach (var j in controls)
                {
                    var j1 = j;
                    if (i1 == j1)
                        continue;

                    var handlerDisabled = false;

                    EventHandler handler = (sender, e) =>
                    {
                        if (syncWidth)
                        {
                            if (!handlerDisabled)
                            {
                                handlerDisabled = true;
                                i1.Width = j1.Width;
                                handlerDisabled = false;
                            }
                        }
                        else if (syncHeight)
                        {
                            if (!handlerDisabled)
                            {
                                handlerDisabled = true;
                                i1.Height = j1.Height;
                                handlerDisabled = false;
                            }
                        }
                    };

                    j.Resize += handler;
                }
            }
        }
    }
}
