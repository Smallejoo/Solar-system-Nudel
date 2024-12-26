using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Solar_system_Nudel.Textures
{
    public static class ImgLoad
    {
        private static readonly Dictionary<string, Image<Rgba32>> _textures = new Dictionary<string, Image<Rgba32>>();
        static ImgLoad()
        {
            LoadAll();
            
            
        }
       
        private static void LoadAll()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var Imeges= assembly.GetManifestResourceNames();

            foreach (var img in Imeges)
            {
                if(img.EndsWith(".jpg",StringComparison.OrdinalIgnoreCase))
                {
                    using var stream=assembly.GetManifestResourceStream(img);
                    if (stream == null)
                        continue;

                    var ImgName = img.Split('.')[2];
                    _textures[ImgName] = SixLabors.ImageSharp.Image.Load<Rgba32>(stream);

                    Console.WriteLine($"Loaded texture: {ImgName}");
                }

            }

        }

        public static void Dis(string TextureName)
        {
            _textures.TryGetValue(TextureName, out var texture);
            texture.Dispose();
        }
        public static Image<Rgba32> GetTexture(string TextureName)
        {
            if (_textures.TryGetValue(TextureName, out var texture))
            {
                return texture.Clone();
            }
            else
            {
                return null;
            }
        }
    }
}
