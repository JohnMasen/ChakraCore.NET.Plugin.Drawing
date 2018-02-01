using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SixLabors.Fonts;
using static ChakraCore.NET.Plugin.Drawing.ImageSharp.ValueConvertHelper;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpDrawingInstaller : DrawingPluginInstaller<ImageSharpDrawingSurface, ImageSharpTexture, ImageSharpSpritBatch>
    {
        public FontCollection Fonts { get; private set; } = new FontCollection();
        public ImageSharpDrawingSurface LastDrawingSurface { get; private set; }
        protected override ImageSharpDrawingSurface GetDrawingSurface(SizeF size, string expetectProfileName)
        {
            LastDrawingSurface= new ImageSharpDrawingSurface(size,Fonts);
            return LastDrawingSurface;
        }

        protected override bool IsProfileSupported(string profileName)
        {
            return profileName == "0.1";
        }

        protected override RectangleF MeasureTextBound(string text, Font font)
        {
            RendererOptions options = new RendererOptions(Fonts.CreateFont(font.Name, font.Size));
            return FromRectangleF(TextMeasurer.MeasureBounds(text, options));
        }
    }
}
