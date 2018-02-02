using System;
using System.Collections.Generic;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public struct Effect
    {
        public string Name;
        public string ConfigJson;
        public static Effect Empty = new Effect() { Name = string.Empty, ConfigJson = string.Empty };
    }
}
