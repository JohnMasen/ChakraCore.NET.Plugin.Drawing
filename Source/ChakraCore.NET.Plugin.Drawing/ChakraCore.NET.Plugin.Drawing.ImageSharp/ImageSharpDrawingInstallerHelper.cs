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

        public static void SetFontRoot(this ImageSharpDrawingInstaller source, string path)
        {
            source.FontLoader.Add((name) =>
            {
                string p = Path.Combine(path, name);
                var family = source.Fonts.Install(p);
                return new Font() { Name = family.Name, Size = 10 };
            });
        }
#if UAP10_0_16299
        public static void SetTextureRoot(this ImageSharpDrawingInstaller source,StorageFolder rootFolder)
        {
            source.TextureLoader.Add((name) =>
            {
                var t = withFileStream<ImageSharpTexture>(rootFolder,name,(s) =>
                {
                    return new ImageSharpTexture(Image.Load(s));
                });
                t.Wait();
                return t.Result;
            });
        }

        public static void SetFontRoot(this ImageSharpDrawingInstaller source, StorageFolder rootFolder)
        {
            source.FontLoader.Add((name) =>
            {
                var t = withFileStream<Font>(rootFolder, name, (s) =>
                    {
                        var family=source.Fonts.Install(s);
                        return new Font() { Name = family.Name, Size = 10 };
                    });
                t.Wait();
                return t.Result;
            });
        }


        private static async Task<T> withFileStream<T>(StorageFolder rootFolder,string name,Func<Stream,T> callback)
        {
            using (var stream = await rootFolder.OpenStreamForReadAsync(name))
            {
                return callback(stream);
            }
        }

#endif
    }
}
