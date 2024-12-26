using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using Solar_system_Nudel.Textures;

namespace Solar_system_Nudel.Classes.OpenGLFolder
{
    public static class TextureRender
    {
        // private static Dictionary<string,uint> TextureData= new Dictionary<string,uint>();
       
        public static uint LoadTextur(string FileName)
        {
            var image = ImgLoad.GetTexture(FileName);
            if (image == null)
            {
                throw new Exception($"Texture '{FileName}' not found.");
            }

            // Generate and bind texture ID
            uint[] textureId = new uint[1];
            OpenGL.glGenTextures(1, textureId);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, textureId[0]);

            // Set texture parameters
            OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            OpenGL.glTexParameteri(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);

            // Copy pixel data
            Rgba32[] pixelData = new Rgba32[image.Width * image.Height];
            image.CopyPixelDataTo(pixelData);

            // Pin pixel data and upload to OpenGL
            GCHandle pinnedArray = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            OpenGL.glTexImage2D(
                OpenGL.GL_TEXTURE_2D,
                0,
                (int)OpenGL.GL_RGBA,
                image.Width,
                image.Height,
                0,
                OpenGL.GL_RGBA,
                OpenGL.GL_UNSIGNED_byte,
                pointer
            );

            // Free pinned memory
            pinnedArray.Free();

            // Dispose of the image after uploading it to OpenGL
            image.Dispose();

            return textureId[0];
        }
    }
}

