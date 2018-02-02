using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Pens;
using SixLabors.ImageSharp.PixelFormats;
using static ChakraCore.NET.Plugin.Drawing.ImageSharp.ValueConvertHelper;
using static SixLabors.Primitives.Matrix3x2Extensions;
namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpSpritBatch : ISpritBatch<ImageSharpTexture>
    {
        Image<Rgba32> image;
        Matrix3x2 world = Matrix3x2.Identity;
        Stack<Matrix3x2> matrixStack = new Stack<Matrix3x2>();
        Queue<Action<IImageProcessingContext<Rgba32>>> commandQueue = new Queue<Action<IImageProcessingContext<Rgba32>>>();
        GraphicsOptions currentOption;
        FontCollection fontCollection;
        ImageSharpEffectFactory effectFactory;
        Effect currentEffect;
        private bool isBegin = false;
        public ImageSharpSpritBatch(Image<Rgba32> image,FontCollection fontCollection,ImageSharpEffectFactory effectFactory)
        {
            this.image = image;
            this.fontCollection = fontCollection;
            this.effectFactory = effectFactory;
        }

        public void Begin(BlendModeEnum blend,Effect effect)
        {
            if (isBegin)
            {
                throw new InvalidOperationException("Can not Call Begin() twice, please call End() before another call of Begin()");
            }
            currentOption = new GraphicsOptions() { BlenderMode = (PixelBlenderMode)(int)blend };
            currentEffect = effect;
        }

        public void End()
        {
            image.Mutate(ctx =>
            {
                while (commandQueue.Count > 0)
                {
                    commandQueue.Dequeue().Invoke(ctx);
                }
                effectFactory.GetImageSharpEffect(currentEffect)?.Process(ctx);
            });

        }

        public void DrawText(PointF position, string text,Font font, string color, int penWidth)
        {
            var p = ToPointF(position,world);
            commandQueue.Enqueue(ctx =>
            {
                ctx.DrawText(text, fontCollection.CreateFont(font.Name, font.Size), Rgba32.FromHex(color), p, currentOption);
            });
        }

        public void DrawLines(IEnumerable<PointF> points, string color, int penWidth)
        {
            var p = points.ToPointFArray(world);
            commandQueue.Enqueue(ctx => ctx.DrawLines(Rgba32.FromHex(color), penWidth, p));
        }
        public void DrawRectangle(RectangleF rect, string color, int penWidth, bool isFill)
        {
            var r = ToRectangleF(rect, world);
            if (isFill)
            {
                commandQueue.Enqueue(cts =>
                {
                    cts.Fill(Rgba32.FromHex(color), r, currentOption);
                   
                });
            }
            else
            {
                commandQueue.Enqueue(cts =>
                {
                    cts.Draw(Rgba32.FromHex(color), penWidth, r, currentOption);
                });
            }
            
        }

        public void DrawTriangle(PointF a, PointF b, PointF c, string color, int penWidth, bool isFill)
        {
            var a1 = ToPointF(a, world);
            var b1 = ToPointF(b, world);
            var c1 = ToPointF(c, world);
            var points = new SixLabors.Primitives.PointF[] { a1, b1, c1, a1 };
            if (isFill)
            {
                commandQueue.Enqueue(ctx =>
                {
                    ctx.FillPolygon(Rgba32.FromHex(color), points, currentOption);
                });
            }
            else
            {
                commandQueue.Enqueue(ctx =>
                {
                    ctx.DrawPolygon(Rgba32.FromHex(color),penWidth, points, currentOption);
                });
            }
        }


        public void DrawEclipse(PointF position, SizeF size, string color, int penWidth, bool isFill)
        {
            var polygon = ToEllipsePolygon(position, size, world);
            if (isFill)
            {
                commandQueue.Enqueue(ctx =>
                {
                    ctx.Fill(
                        Rgba32.FromHex(color)
                        , polygon
                        , currentOption);
                });
            }
            else
            {
                commandQueue.Enqueue(ctx =>
                {
                    ctx.Draw(
                        Rgba32.FromHex(color)
                        , penWidth
                        , polygon
                        , currentOption);
                });
            }

        }


        public void DrawImage(PointF position, SizeF size, ImageSharpTexture texture, float opacity)
        {
            var s = ToSize(size, world);
            var p = ToPoint(position, world);
            commandQueue.Enqueue(ctx =>
            {
                ctx.DrawImage(texture.Image, opacity, s,p );
            });
        }

        public void Fill(string color, RectangleF region)
        {
            var r = ToRectangleF(region, world);
            commandQueue.Enqueue(ctx =>
            {
                ctx.Fill(Rgba32.FromHex(color), r);
            });
        }

        public void Translate(PointF value)
        {
            world = Matrix3x2.CreateTranslation(value.X, value.Y) * world;
        }

        public void Scale(PointF value)
        {
            world = Matrix3x2.CreateScale(value.X, value.Y) * world;
        }

        public void Rotate(float angel)
        {
            world = Matrix3x2.CreateRotation((float)Math.PI * angel / 180f) * world;
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
