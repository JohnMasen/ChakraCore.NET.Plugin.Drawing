using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public abstract class DrawingPluginInstaller<TDrawingSurface, TTexture,TSpritBatch> : Hosting.IPluginInstaller 
        where TTexture : class,ITexture
        where TSpritBatch:class,ISpritBatch<TTexture>
        where TDrawingSurface:class,IDrawingSurface<TTexture,TSpritBatch>
        
    {
        public string Name => "Plugin.Drawing";

        public string GetSDK() => Properties.Resources.sdk;
        public Hosting.ProtocolResolver<TTexture> TextureLoader = new Hosting.ProtocolResolver<TTexture>("file");
        public Hosting.ProtocolResolver<Font> FontLoader = new Hosting.ProtocolResolver<Font>("file");

        protected abstract TDrawingSurface GetDrawingSurface(SizeF size,string expetectProfileName);
        protected abstract bool IsProfileSupported(string profileName);

        public void Install(JSValue target)
        {
            var converter = target.ServiceNode.GetService<IJSValueConverterService>();
            registerBasicTypes(converter);
            registerTTexture(converter);
            registerTSprintBatch(converter);
            registerTDrawingSurface(converter);
            registerFont(converter);
            target.Binding.SetFunction<SizeF,string, TDrawingSurface>(nameof(GetDrawingSurface), GetDrawingSurface);
            target.Binding.SetFunction<string, TTexture>("LoadTexture", TextureLoader.Process);
            target.Binding.SetFunction<string, bool>(nameof(IsProfileSupported), IsProfileSupported);
            target.Binding.SetFunction<string, Font>("LoadFont", FontLoader.Process);
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
            converter.RegisterStructConverter<RectangleF>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("X", value.X);
                    jsvalue.WriteProperty("Y", value.Y);
                    jsvalue.WriteProperty("Width", value.Width);
                    jsvalue.WriteProperty("Height", value.Height);
                },
                (jsvalue) =>
                {
                    return new RectangleF(
                        jsvalue.ReadProperty<float>("X")
                        , jsvalue.ReadProperty<float>("Y")
                        ,jsvalue.ReadProperty<float>("Width")
                        , jsvalue.ReadProperty<float>("Height"));
                });
            converter.RegisterArrayConverter<PointF>();

            converter.RegisterConverter<BlendModeEnum>(
                (node,value)=>
                {
                    return node.GetService<IContextSwitchService>().With(() =>
                    {
                        return API.JavaScriptValue.FromInt32((int)value);
                    });
                },
                (node,jsvalue)=>
                {
                    return node.GetService<IContextSwitchService>().With(() =>
                    {
                        return (BlendModeEnum)jsvalue.ToInt32();
                    });
                }


                );

        }

        private void registerTTexture(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<TTexture>(
                (binding, obj, node) =>
                {
                    binding.SetFunction<SizeF>(nameof(obj.GetSize), () => obj.GetSize);
                });
        }

        private void registerTSprintBatch(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<TSpritBatch>(
                (binding, obj, node) =>
                {
                    binding.SetMethod<PointF,string,string,int>(nameof(obj.DrawText), obj.DrawText);
                    binding.SetMethod<IEnumerable<PointF>, string, int>(nameof(obj.DrawLines), obj.DrawLines);
                    binding.SetMethod<PointF, SizeF, string, int, bool>(nameof(obj.DrawEclipse), obj.DrawEclipse);
                    binding.SetMethod<PointF, SizeF, TTexture,float>(nameof(obj.DrawImage), obj.DrawImage);
                    binding.SetMethod<string, RectangleF>(nameof(obj.Fill), obj.Fill);
                    binding.SetMethod<PointF>(nameof(obj.Translate), obj.Translate);
                    binding.SetMethod<PointF>(nameof(obj.Scale), obj.Scale);
                    binding.SetMethod<float>(nameof(obj.Rotate), obj.Rotate);
                    binding.SetFunction<int>(nameof(obj.PushMatrix), obj.PushMatrix);
                    binding.SetFunction<int>(nameof(obj.PopMatrix), obj.PopMatrix);
                    binding.SetMethod(nameof(obj.ResetMatrix), obj.ResetMatrix);
                    binding.SetMethod<BlendModeEnum>(nameof(obj.Begin), obj.Begin);
                    binding.SetMethod(nameof(obj.End), obj.End);
                }
                );
        }
        
        private void registerTDrawingSurface(IJSValueConverterService converter)
        {
            converter.RegisterProxyConverter<TDrawingSurface>(
                (binding, obj, node) =>
                {
                    binding.SetFunction(nameof(obj.CreateSpritBatch), obj.CreateSpritBatch);
                });
        }

        private void registerFont(IJSValueConverterService converter)
        {
            converter.RegisterStructConverter<Font>(
                (jsvalue, obj) =>
                {
                    jsvalue.WriteProperty<string>(nameof(obj.Name), obj.Name);
                    jsvalue.WriteProperty<float>(nameof(obj.Size), obj.Size);
                },
                (jsvalue) =>
                {
                    return new Font()
                    {
                        Name = jsvalue.ReadProperty<string>(nameof(Font.Name)),
                        Size = jsvalue.ReadProperty<float>(nameof(Font.Size))
                    };
                });
        }
        
    }
}
