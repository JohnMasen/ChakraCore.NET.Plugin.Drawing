using System;

namespace ChakraCore.NET.Plugin.Drawing
{
    public interface IDrawingSurface<TTexture> 
        where TTexture:ITexture
    {
        ISpritBatch<TTexture> CreateSpritBatch();
        string GetCurrentProfile();
    }
}
