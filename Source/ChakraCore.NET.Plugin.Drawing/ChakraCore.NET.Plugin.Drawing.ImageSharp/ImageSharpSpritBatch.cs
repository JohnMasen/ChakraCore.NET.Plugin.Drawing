using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Pens;
using static ChakraCore.NET.Plugin.Drawing.ImageSharp.ValueConvertHelper;
using static SixLabors.Primitives.Matrix3x2Extensions;
namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpSpritBatch : ISpritBatch<ImageSharpTexture>
    {
        Image<Rgba32> image;
        Matrix3x2 world = Matrix3x2.Identity;
        Stack<Matrix3x2> matrixStack = new Stack<Matrix3x2>();
        public ImageSharpSpritBatch(Image<Rgba32> image)
        {
            this.image = image;
        }
        
        public void Begin()
        {
        }

        public void End()
        {
        }

        public void DrawText(PointF position, string text, string color, int penWidth)
        {
            throw new NotImplementedException();
        }

        public void DrawLines(IEnumerable<PointF> points, string color, int penWidth)
        {
            image.Mutate(ctx => ctx.DrawLines(Rgba32.FromHex(color), penWidth, points.ToPointFArray(world)));
        }


        public void DrawEclipse(PointF position, SizeF size, string color, int penWidth, bool isFill)
        {
            if (isFill)
            {
                image.Mutate(ctx =>
                {
                    ctx.Fill(
                        Rgba32.FromHex(color)
                        , ToEllipsePolygon(position, size, world));
                });
            }
            else
            {
                image.Mutate(ctx =>
                {
                    ctx.Draw(
                        Rgba32.FromHex(color)
                        , penWidth
                        , ToEllipsePolygon(position,size,world));
                });
            }
            
        }


        public void DrawImage(PointF position, SizeF size, ImageSharpTexture texture)
        {
            image.Mutate(ctx =>
            {
                ctx.DrawImage(texture.Image, 1, ToSize(size, world), ToPoint(position, world));
            });
        }

        public void Fill(string color, RectangleF region)
        {
            image.Mutate(ctx =>
            {
                ctx.Fill(Rgba32.FromHex(color), ToRectangleF(region,world));
            });
        }

        public void Translate(PointF value)
        {
            world= Matrix3x2.CreateTranslation(value.X, value.Y)* world;
        }

        public void Scale(PointF value)
        {
            world = Matrix3x2.CreateScale(value.X, value.Y)*world;
        }

        public void Rotate(float angel)
        {
            world = Matrix3x2.CreateRotation((float)Math.PI * angel / 180f)*world;
        }

        public int PushMatrix()
        {
            matrixStack.Push(world);
            return matrixStack.Count;
        }

        public int PopMatrix()
        {
            world = matrixStack.Pop();
            return matrixStack.Count;
        }

        public void ResetMatrix()
        {
            world = Matrix3x2.Identity;
            matrixStack.Clear();
        }

        

        
    }
}
