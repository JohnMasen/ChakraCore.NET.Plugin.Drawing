﻿using System.Drawing;
using SixLabors.ImageSharp;
namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpTexture : ITexture
    {
        public Image<Rgba32> Image { get; set; }
        public ImageSharpTexture(Image<Rgba32> image )
        {
            Image = image;
        }
        public SizeF GetSize => new SizeF(Image.Width,Image.Height );
    }
}
