using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public interface ISpritBatch< TTexture> where TTexture:ITexture 
    {
        void Begin(BlendModeEnum blend);
        void End();
        void DrawText(PointF position, string text, string color,int penWidth);
        void DrawLines(IEnumerable<PointF> points, string color,int penWidth);
        void DrawEclipse(PointF position,SizeF region, string color, int penWidth, bool isFill);
        void DrawImage(PointF position,SizeF size,TTexture texture,float opacity);
        void Fill(string color, RectangleF region);
        void DrawRectangle(RectangleF rect, string color, int penWidth, bool isFill);
        void DrawTriangle(PointF a, PointF b, PointF c, string color, int penWidth, bool isFill);

        void Translate(PointF value);
        void Scale(PointF value);
        void Rotate(float angel);
        int PushMatrix();
        int PopMatrix();
        void ResetMatrix();
    }
}
