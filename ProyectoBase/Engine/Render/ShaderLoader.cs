using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class ShaderLoader : AssetLoader
    {
        GL openGL;

        public ShaderLoader(GL gl)
        {
            openGL = gl;
        }

        public override object LoadAsset(string path)
        {
            String[] paths = path.Split('|');

            List<String> vertexShadersPath = new List<String>();
            List<String> fragmentShadersPath = new List<String>();

            foreach (String shaderPath in paths)
            {
                if(shaderPath.Contains("Vertex")) 
                {
                    vertexShadersPath.Add(shaderPath);
                }
                else
                {
                    fragmentShadersPath.Add(shaderPath);
                }
            }

            Shader s = new Shader(openGL, vertexShadersPath[0], fragmentShadersPath[0]);

            return s;
        }
        public override void UnloadAsset(object asset)
        {
            Shader s = (Shader)asset;
            s.Dispose();
        }
    }
}
