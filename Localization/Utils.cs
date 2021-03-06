﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Localization
{
	static class Utils
	{
		public static string NormalizeLineBreaks(string input)
		{
			//Written by Jon Skeet at http://stackoverflow.com/a/841453/17027

			// Allow 10% as a rough guess of how much the string may grow.
			// If we're wrong we'll either waste space or have extra copies -
			// it will still work
			var builder = new StringBuilder((int)(input.Length * 1.1));

			bool lastWasCR = false;

			foreach (char c in input)
			{
				if (lastWasCR)
				{
					lastWasCR = false;
					if (c == '\n')
					{
						continue; // Already written \r\n
					}
				}
				switch (c)
				{
					case '\r':
						builder.Append("\r\n");
						lastWasCR = true;
						break;
					case '\n':
						builder.Append("\r\n");
						break;
					default:
						builder.Append(c);
						break;
				}
			}
			return builder.ToString();
		}
	}
}
