using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public class DrawingPluginInstaller<TTexture> : Hosting.IPluginInstaller
        where TTexture : ITexture
        
    {
        public string Name => "Plugin.Drawing";

        public string GetSDK()
        {
            throw new NotImplementedException();
        }

        public void Install(JSValue target)
        {
            var converter = target.ServiceNode.GetService<IJSValueConverterService>();
            registerBasicTypes(converter);
            registerITexture(converter);
            registerISprintBatch(converter);
            registerIDrawingSurface(converter);
        }

        private void registerBasicTypes(IJSValueConverterService converter)
        {
            converter.RegisterStructConverter<PointF>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("X", value.X);
                    jsvalue.WriteProperty("Y", value.Y);
                },
                (jsvalue) =>
                {
                    return new PointF(
                        jsvalue.ReadProperty<float>("X")
                        , jsvalue.ReadProperty<float>("Y"));
                });

            converter.RegisterStructConverter<SizeF>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("Width", value.Width);
                    jsvalue.WriteProperty("Height", value.Height);
                },
                (jsvalue) =>
                {
                    return new SizeF(
                        jsvalue.ReadProperty<float>("Width")
                        , jsvalue.ReadProperty<float>("Height"));
                });
        }

        private void registerITexture(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<ITexture>(
                (binding, obj, node) =>
                {
                    binding.SetFunction<SizeF>("GetSize", () => obj.Size);
                });
        }

        private void registerISprintBatch(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<ISpritBatch<TTexture>>(
                (binding, obj, node) =>
                {
                    binding.SetMethod<PointF,string,int>(nameof(obj.DrawText), obj.DrawText);
                    binding.SetMethod<PointF, PointF, int, int>(nameof(obj.DrawLine), obj.DrawLine);
                    binding.SetMethod<PointF, SizeF, int, bool>(nameof(obj.DrawRectangle), obj.DrawRectangle);
                    binding.SetMethod<PointF, RectangleF, int, bool>(nameof(obj.DrawEclipse), obj.DrawEclipse);
                    binding.SetMethod<PointF, SizeF, TTexture>(nameof(obj.DrawImage), obj.DrawImage);
                    binding.SetMethod<int, RectangleF>(nameof(obj.Fill), obj.Fill);
                    binding.SetMethod<PointF>(nameof(obj.Translate), obj.Translate);
                    binding.SetMethod<PointF>(nameof(obj.Scale), obj.Scale);
                    binding.SetMethod<float>(nameof(obj.Rotate), obj.Rotate);
                    binding.SetFunction<int>(nameof(obj.PushMatrix), obj.PopMatrix);
                    binding.SetFunction<int>(nameof(obj.PopMatrix), obj.PopMatrix);
                    binding.SetMethod(nameof(obj.ResetMatrix), obj.ResetMatrix);
                }
                );
        }
        
        private void registerIDrawingSurface(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<IDrawingSurface<TTexture>>(
                (binding, obj, node) =>
                {
                    binding.SetFunction(nameof(obj.CreateSpritBatch), obj.CreateSpritBatch);
                    binding.SetFunction<string,TTexture>(nameof(obj.LoadTexture), obj.LoadTexture);
                });
        }
    }
}
