using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public interface IImageSharpEffect
    {
        string Name { get; }
        void SetConfig(string configJson);
        void Process(IImageProcessingContext<Rgba32> context);
        string DefaultConfigJson { get; }
    }
}
