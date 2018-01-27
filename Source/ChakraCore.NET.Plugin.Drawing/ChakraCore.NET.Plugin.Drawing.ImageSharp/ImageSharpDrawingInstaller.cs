using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SixLabors.Fonts;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpDrawingInstaller : DrawingPluginInstaller<ImageSharpDrawingSurface, ImageSharpTexture, ImageSharpSpritBatch>
    {
        string resourceFolder;
        FontCollection Fonts = new FontCollection();
        public ImageSharpDrawingSurface LastDrawingSurface { get; private set; }
        public ImageSharpDrawingInstaller(string resourceFolder)
        {
            this.resourceFolder = resourceFolder;
        }
        protected override ImageSharpDrawingSurface GetDrawingSurface(SizeF size, string expetectProfileName)
        {
            LastDrawingSurface= new ImageSharpDrawingSurface(size);
            return LastDrawingSurface;
        }

        protected override bool IsProfileSupported(string profileName)
        {
            return profileName == "0.1";
        }

        protected override bool LoadFont(string resourceName)
        {
            string path = getResourcePath(resourceName);
            Fonts.Install(path);
            return true;
        }

        protected override ImageSharpTexture LoadTexutre(string resourceName)
        {
            string path = getResourcePath(resourceName);
            return new ImageSharpTexture(Image.Load(path));
        }

        private string getResourcePath(string name)
        {
            return Path.Combine(resourceFolder, name);
        }
    }
}
