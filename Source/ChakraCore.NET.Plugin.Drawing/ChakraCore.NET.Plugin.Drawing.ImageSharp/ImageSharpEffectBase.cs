using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public abstract class ImageSharpEffectBase<TConfig> :IImageSharpEffect where TConfig:struct
    {
        public ImageSharpEffectBase(string name)
        {
            this.Name = name;
        }
        public TConfig Config;
        public string Name { get; private set; }
        protected abstract TConfig GetDefaultConfig();
        public void SetConfig(string configJson)
        {
            if (string.IsNullOrEmpty(configJson))
            {
                return;
            }
            Config=JsonConvert.DeserializeObject<TConfig>(configJson);
        }

        public abstract void Process(IImageProcessingContext<Rgba32> context);
        public string DefaultConfigJson
        {
            get
            {
                return JsonConvert.SerializeObject(GetDefaultConfig());
            }
        }
    }
}
