using System;

namespace ChakraCore.NET.Plugin.Drawing
{
    public interface IDrawingSurface<TTexture,TSprintBatch> 
        where TTexture:ITexture
        where TSprintBatch:ISpritBatch<TTexture>
    {
        TSprintBatch CreateSpritBatch();
        string GetCurrentProfile();
        TTexture SaveToTexture();
    }
}
