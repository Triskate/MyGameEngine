using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Numerics;
using System.Reflection;


namespace TestOpenGL
{
    internal class Program
    {

        private static IWindow window;

        static void Main(string[] args)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "My Game Engine";
            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Closing += OnClose;

            window.Run();

            window.Dispose();
        }

        static void OnLoad()
        {
            Assets.Init();
            Input.Init(window);
            Input.onKeyDown += OnKeyDown;
            SceneManager.Init(window);
            Render.Init(window);

            Assets.LoadAssets();
            SceneManager.SetActiveScene("Main.scene");
        }


        private static void OnUpdate(double deltaTime)
        {

        }
        private static void OnClose()
        {
            Assets.UnloadAssets();

            Render.Finish();
            SceneManager.Finish();
            Input.Finish();
            Assets.Finish();
        }

        static void OnKeyDown(Key key, int arg3)
        {
            if (key == Key.Escape)
            {
                window.Close();
            }
            else if(key == Key.F5) 
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneSerializer.SerializeScene(scene, Assets.GetAssetsPath() + "\\Saved.scene");
            }
            else if(key == Key.F9) 
            {
                SceneSerializer.DeserializeScene(Assets.GetAssetsPath() + "\\Saved.scene");
            }
        }
    }
}
