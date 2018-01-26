using System;

namespace ChakraCore.NET.Plugin.Drawing
{
    public interface IDrawingSurface<TTexture> 
        where TTexture:ITexture
    {
        TTexture LoadTexture(string name);
        ISpritBatch<TTexture> CreateSpritBatch();
    }
}
