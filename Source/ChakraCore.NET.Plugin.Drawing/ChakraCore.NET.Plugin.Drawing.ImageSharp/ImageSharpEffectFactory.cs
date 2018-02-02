using System;
using System.Collections.Generic;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public class ImageSharpEffectFactory
    {
        Dictionary<string,IImageSharpEffect> effects = new Dictionary<string, IImageSharpEffect>();
        public void RegisterEffect(IImageSharpEffect effect)
        {
            effects.Add(effect.Name, effect);
        }
        public Effect GetEffectWithDefaultConfig(string name)
        {
            Effect result;
            if (effects.ContainsKey(name))
            {
                result.Name = name;
                result.ConfigJson= effects[name].DefaultConfigJson;
            }
            else
            {
                result = Effect.Empty;            }
            return result;
        }

        public IImageSharpEffect GetImageSharpEffect(Effect value)
        {
            if (effects.ContainsKey(value.Name))
            {
                var result = effects[value.Name];
                result.SetConfig(value.ConfigJson);
                return result;
            }
            return null;
        }
    }
}
