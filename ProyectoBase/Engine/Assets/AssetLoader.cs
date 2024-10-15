using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal abstract class AssetLoader
    {
        public abstract object LoadAsset(string path);

        public abstract void UnloadAsset(object asset);
    }
}
