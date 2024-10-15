using Silk.NET.Assimp;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SixLabors.ImageSharp;
using System.Drawing;
using System.IO;
using System.Numerics;

namespace TestOpenGL
{
    internal class Render
    {
        static Vector3 directionalLightDirection;
        static Vector3 directionalLightColor;
        static float directionalLightIntensity;

        static Vector3 pointLightPosition;
        static Vector3 pointLightColor;
        static float pointLightIntensity;

        public static Vector3 cameraPosition { get; set; } = new Vector3(0.0f, 0.0f, 3.0f);
        public static float cameraYaw { get; set; } = -90f;
        public static float cameraPitch { get; set; } = 0f;

        public static float cameraZoom { get; set; }  = 45f;

        public static Vector3 cameraFront { get; private set; } = new Vector3(0.0f, 0.0f, -1.0f);
        public static Vector3 cameraUp { get; private set; } = Vector3.UnitY;
        public static Vector3 cameraDirection { get; private set; } = Vector3.Zero;

        static Matrix4x4 viewMatrix;
        static Matrix4x4 projectionMatrix;

        static IWindow window;
        static GL openGL;

        public static void Init(IWindow _window)
        {
            openGL = GL.GetApi(_window);

            window = _window;

            _window.Render += OnRender;
            _window.FramebufferResize += OnFramebufferResize;

            Assets.RegisterAssetLoader("png", new TextureLoader(openGL));
            Assets.RegisterAssetLoader("obj", new ModelLoader(openGL));
            Assets.RegisterAssetLoader("cg", new ShaderLoader(openGL));
        }


        public static void Finish()
        {

        }

        public static void SetView(Vector3 p, Vector3 r, float fov, System.Drawing.Color backgroundColor) 
        {
            openGL.Enable(EnableCap.DepthTest);
            openGL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            openGL.ClearColor(backgroundColor);

            var size = window.FramebufferSize;

            var dir = new Vector3();
            float yawRads = MathUtils.DegreesToRadians(cameraYaw);
            float pitchRads = MathUtils.DegreesToRadians(cameraPitch);

            dir.X = MathF.Cos(yawRads) * MathF.Cos(pitchRads);
            dir.Y = MathF.Sin(pitchRads);
            dir.Z = MathF.Sin(yawRads) * MathF.Cos(pitchRads);
            cameraFront = Vector3.Normalize(dir);

            float zoomRads = MathUtils.DegreesToRadians(fov);

            viewMatrix = Matrix4x4.CreateLookAt(cameraPosition, cameraPosition + cameraFront, cameraUp);
            projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(zoomRads, (float)size.X / size.Y, 0.1f, 100.0f);
        }
        public static void SetDirectionalLight(Vector3 direction, Vector3 color, float intensity)
        {
            Console.WriteLine(direction.X + ", " + direction.Y + ", " + direction.Z);
            directionalLightDirection = direction;
            directionalLightColor = color;
            directionalLightIntensity = intensity;
        }
        public static void SetPointLight(Vector3 position, Vector3 color, float intensity) 
        {
            Console.WriteLine(position.X + ", " + position.Y + ", " + position.Z);
            pointLightPosition = position;
            pointLightColor = color;
            pointLightIntensity = intensity;
        }
        public static void DrawModel(Vector3 position, Vector3 rotation, Vector3 scale, Model model, Shader shader, Texture texture) 
        {

            texture.Bind();
            shader.Use();
            shader.SetUniform("uTexture0", 0);
            shader.SetUniform("uView", viewMatrix);
            shader.SetUniform("uProjection", projectionMatrix);

            Vector4 lightColor = new Vector4(directionalLightColor, directionalLightIntensity);
            shader.SetUniform("uDirectionalLightColor", lightColor);
            shader.SetUniform("uDirectionalLightDirection", -directionalLightDirection);

            Vector4 plightColor = new Vector4(pointLightColor, pointLightIntensity);
            shader.SetUniform("uPointLightColor", plightColor);
            shader.SetUniform("uPointLightPosition", pointLightPosition);

            float rotX = MathUtils.DegreesToRadians(rotation.X);
            float rotY = MathUtils.DegreesToRadians(rotation.Y);
            float rotZ = MathUtils.DegreesToRadians(rotation.Z);

            var modelMatrix = Matrix4x4.CreateScale(scale) *
                              Matrix4x4.CreateRotationZ(rotZ) * 
                              Matrix4x4.CreateRotationY(rotY) * 
                              Matrix4x4.CreateRotationX(rotX) *
                              Matrix4x4.CreateTranslation(position);

            int count = model.meshes.Count;

            for (int i = 0; i < count; i++) 
            {
                Mesh mesh = model.meshes[i];
                mesh.Bind();
                shader.SetUniform("uModel", modelMatrix);

                openGL.DrawArrays(Silk.NET.OpenGL.PrimitiveType.Triangles, 0, (uint)mesh.vertices.Length);
            }
        }

        static unsafe void OnRender(double deltaTime)
        {
            SceneManager.SetView((float)deltaTime);
            SceneManager.Render((float)deltaTime);
        }

        static void OnFramebufferResize(Vector2D<int> newSize)
        {
            openGL.Viewport(newSize);
        }
    }
}
