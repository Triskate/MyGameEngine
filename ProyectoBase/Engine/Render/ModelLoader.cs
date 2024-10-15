using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class ModelLoader : AssetLoader
    {
        GL openGL;

        public ModelLoader(GL gl)
        {
            openGL = gl;
        }

        public override object LoadAsset(string path)
        {
            Model m = new Model(openGL, path);

            return m;
        }
        public override void UnloadAsset(object asset)
        {
            Model m = (Model)asset;
            m.Dispose();
        }
    }
}
