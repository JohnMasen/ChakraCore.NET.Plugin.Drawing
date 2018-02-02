using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp.Effects
{
    public struct BlurConfig
    {
        public float sigma;
        public BlurConfig(float sigma)
        {
            this.sigma = sigma;
        }
        public static BlurConfig Default = new BlurConfig(1f);
    }
    public class BlurEffect : ImageSharpEffectBase<BlurConfig>
    {
        public BlurEffect():base("Blur")
        {

        }
        public override void Process(IImageProcessingContext<Rgba32> context)
        {
            context.GaussianBlur(Config.sigma);
        }

        protected override BlurConfig GetDefaultConfig()
        {
            return BlurConfig.Default;
        }
    }
}
