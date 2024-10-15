using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class SceneManager
    {
        private static Scene activeScene;
        public static void Init(IWindow window)
        {
            SceneLoader loader = new SceneLoader();
            Assets.RegisterAssetLoader("scene", loader);

            window.Update += Update;
        }
        public static void Finish()
        {

        }
        public static void Start()
        {

        }
        public static void Stop()
        {

        }
        public static void Update(double deltaTime)
        {
            activeScene.Update((float)deltaTime);
        }
        public static void FixedUpdate(double deltaTime)
        {

        }
        public static void Render(float deltaTime)
        {
            activeScene.Render((float)deltaTime);
        }
        public static void SetView(float deltaTime)
        {
            activeScene.SetView(deltaTime);
        }
        public static void SetActiveScene(String sceneID)
        {
            Scene s = Assets.GetLoadedAsset<Scene>(sceneID);
            activeScene = s;
        }

        public static Scene GetActiveScene()
        {
            return activeScene;
        }
    }
}
