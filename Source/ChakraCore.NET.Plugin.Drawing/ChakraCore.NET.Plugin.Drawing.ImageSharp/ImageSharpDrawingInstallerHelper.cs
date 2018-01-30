using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
#if UAP10_0_16299
using Windows.Storage;
#endif

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public static class ImageSharpDrawingInstallerHelper
    {
        public static void SetTextureRoot(this ImageSharpDrawingInstaller source, string path)
        {
            source.TextureLoader.Add((name) =>
            {
                string p = Path.Combine(path, name);
                return new ImageSharpTexture(Image.Load(p));
            });

        }
#if UAP10_0_16299
        public static void SetTextureRoot(this ImageSharpDrawingInstaller source,StorageFolder rootFolder)
        {
            source.TextureLoader.Add((name) =>
            {
                var t = loadTextureFromRoot(rootFolder, name);
                t.Wait();
                return t.Result;
            });
        }

        private static async Task<ImageSharpTexture> loadTextureFromRoot(StorageFolder rootFolder,string name)
        {
            using (var stream = await rootFolder.OpenStreamForReadAsync(name))
            {
                return new ImageSharpTexture(Image.Load(stream));
            }
        }
#endif
    }
}
