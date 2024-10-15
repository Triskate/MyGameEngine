using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Assets
    {
        static Dictionary<string, AssetLoader> loaders;
        static Dictionary<string, object> assets;

        public static void Init()
        {
            loaders = new Dictionary<string, AssetLoader> ();
            assets = new Dictionary<string, object> ();
        }

        public static void LoadAssets()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Assets");

            object obj;
            int shadersCount = 0;
            List<String> vertexShaders = new List<string>();
            List<String> fragmentShaders = new List<string>();

            foreach (string file in files) 
            {
                switch (Path.GetExtension(file).Trim('.'))
                {
                    case "png":
                        obj = loaders["png"].LoadAsset(file);
                        assets[file] = obj;
                        break;
                    case "obj":
                        obj = loaders["obj"].LoadAsset(file);
                        assets[file] = obj;
                        break;
                    case "cg":
                        if (file.Contains("Vertex"))
                        {
                            vertexShaders.Add(file);
                            shadersCount++;
                        }
                        else if (file.Contains("Fragment"))
                        {
                            fragmentShaders.Add(file);
                            shadersCount++;
                        }
                        break;
                    case "scene":
                        obj = loaders["scene"].LoadAsset(file);
                        assets[file] = obj;
                        break;
                    default:
                        break;
                }
            }

            String shadersPath = "";
            for( int i = 0; i < shadersCount/2; i++)
            {
                shadersPath = vertexShaders[i] + "|" + fragmentShaders[i];
                obj = loaders["cg"].LoadAsset(shadersPath);
                assets["Shaders" + (i+1) + ".cg"] = obj;
            }
        }

        public static void UnloadAssets()
        {

        }

        public static void Finish()
        {

        }

        public static void RegisterAssetLoader(string ext, AssetLoader loader)
        {
            loaders[ext] = loader;
        }

        public static T GetLoadedAsset<T>(string path, bool isRelative = true)
        {
            if (path.StartsWith("Shaders"))
            {
                return (T)assets[path];
            }
            else
            {
                return (T)assets[(isRelative ? GetAssetsPath() + "\\" : "") + path];
            }
        }

        public static string GetAssetsPath() 
        {
            return Directory.GetCurrentDirectory() + "\\Assets";
        }

        public static string LoadText(string path) 
        {
            return path;
        }
    }
}
