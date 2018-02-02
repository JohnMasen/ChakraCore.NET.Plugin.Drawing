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
        public ImageSharpEffectFactory EffectFactory { get; private set; } = new ImageSharpEffectFactory();
        public ImageSharpDrawingInstaller()
        {
            EffectFactory.RegisterEffect(new Effects.BlurEffect());
        }
        protected override ImageSharpDrawingSurface GetDrawingSurface(SizeF size, string expetectProfileName)
        {
            LastDrawingSurface= new ImageSharpDrawingSurface(size,Fonts,EffectFactory);
            return LastDrawingSurface;
        }

        protected override bool IsProfileSupported(string profileName)
        {
            return profileName == "0.1";
        }

        protected override Effect LoadEffect(string effectName)
        {
            return EffectFactory.GetEffectWithDefaultConfig(effectName);
        }

        protected override RectangleF MeasureTextBound(string text, Font font)
        {
            RendererOptions options = new RendererOptions(Fonts.CreateFont(font.Name, font.Size));
            return FromRectangleF(TextMeasurer.MeasureBounds(text, options));
        }
    }
}
