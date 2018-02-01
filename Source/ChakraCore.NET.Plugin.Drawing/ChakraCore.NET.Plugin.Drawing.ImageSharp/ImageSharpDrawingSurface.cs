using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpDrawingSurface : IDrawingSurface<ImageSharpTexture,ImageSharpSpritBatch>
    {
        public Image<Rgba32> Image { get; private set; }
        ImageSharpSpritBatch batch;
        FontCollection fontCollection;
        public ImageSharpDrawingSurface(SizeF size,FontCollection fonts)
        {
            Image = new Image<Rgba32>((int)size.Width, (int)size.Height);
            batch = new ImageSharpSpritBatch(Image, fonts);
        }
        public ImageSharpSpritBatch CreateSpritBatch()
        {
            return batch;
        }

        public string GetCurrentProfile()
        {
            return "0.1";
        }

        public ImageSharpTexture SaveToTexture()
        {
            return new ImageSharpTexture(Image.Clone());
        }
    }
}
