using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class TextureLoader : AssetLoader
    {
        GL openGL;

        public TextureLoader(GL gl)
        {
            openGL = gl;
        }

        public override object LoadAsset(string path)
        {
            Texture t = new Texture(openGL, path);

            return t;
        }
        public override void UnloadAsset(object asset)
        {
            Texture t = (Texture)asset;
            t.Dispose();
        }
    }
}
