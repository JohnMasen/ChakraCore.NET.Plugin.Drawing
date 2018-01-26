using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public interface ISpritBatch< TTexture> where TTexture:ITexture 
    {
        void Begin();
        void End();
        void DrawText(PointF position, string text,int color);
        void DrawLine(PointF start,PointF end, int color,int penWidth);
        void DrawRectangle(PointF position,SizeF size, int color,bool isFill);
        void DrawEclipse(PointF position,RectangleF region, int color,bool isFill);
        void DrawImage(PointF position,SizeF size,TTexture texture);
        void Fill(int color, RectangleF region);
        

        void Translate(PointF value);
        void Scale(PointF value);
        void Rotate(float angel);
        int PushMatrix();
        int PopMatrix();
        void ResetMatrix();
    }
}
