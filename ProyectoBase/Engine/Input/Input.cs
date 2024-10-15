using Silk.NET.Input;
using Silk.NET.Windowing;
using System.Numerics;

namespace TestOpenGL
{
    internal class Input
    {
        public static Action<Vector2> onMouseMove;
        public static Action<ScrollWheel> onMouseWheel;
        public static Action<Key, int> onKeyDown;

        static IKeyboard keyboard;
        static IMouse mouse;        


        public static void Init(IWindow window)
        {
            IInputContext input = window.CreateInput();
            keyboard = input.Keyboards.FirstOrDefault();
            mouse = input.Mice.FirstOrDefault();
            mouse.Cursor.CursorMode = CursorMode.Raw;

            keyboard.KeyDown += OnKeyDown;
            mouse.MouseMove += OnMouseMove;
            mouse.Scroll += OnMouseWheel;
        }

        public static void Finish()
        {
            // Nothing to do
        }

        public static bool IsKeyPressed(Key k)
        {
            return keyboard.IsKeyPressed(k);
        }

        private static void OnMouseMove(IMouse mouse, Vector2 position)
        {
            onMouseMove?.Invoke(position);
        }

        private static void OnMouseWheel(IMouse mouse, ScrollWheel scrollWheel)
        {
            onMouseWheel?.Invoke(scrollWheel);
        }

        static void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            onKeyDown?.Invoke(key, arg3);
        }


    }


}
