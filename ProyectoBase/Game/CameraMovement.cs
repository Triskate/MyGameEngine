using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class CameraMovement : Component
    {
        private static Vector2 lastMousePosition;
        public override void Update(float deltaTime)
        {

            Input.onMouseMove += OnMouseMove;
            Input.onMouseWheel += OnMouseWheel;

            var moveSpeed = 2.5f * (float)deltaTime;

            if (Input.IsKeyPressed(Key.W))
            {
                TestOpenGL.Render.cameraPosition += moveSpeed * TestOpenGL.Render.cameraFront;
            }
            if (Input.IsKeyPressed(Key.S))
            {
                TestOpenGL.Render.cameraPosition -= moveSpeed * TestOpenGL.Render.cameraFront;
            }
            if (Input.IsKeyPressed(Key.A))
            {
                TestOpenGL.Render.cameraPosition -= Vector3.Normalize(Vector3.Cross(TestOpenGL.Render.cameraFront, TestOpenGL.Render.cameraUp)) * moveSpeed;
            }
            if (Input.IsKeyPressed(Key.D))
            {
                TestOpenGL.Render.cameraPosition += Vector3.Normalize(Vector3.Cross(TestOpenGL.Render.cameraFront, TestOpenGL.Render.cameraUp)) * moveSpeed;
            }
        }
        private static void OnMouseMove(Vector2 position)
        {
            var lookSensitivity = 0.1f;
            if (lastMousePosition == default)
            {
                lastMousePosition = position;
            }
            else
            {
                var xOffset = (position.X - lastMousePosition.X) * lookSensitivity;
                var yOffset = (position.Y - lastMousePosition.Y) * lookSensitivity;
                lastMousePosition = position;

                TestOpenGL.Render.cameraYaw += xOffset;
                TestOpenGL.Render.cameraPitch -= yOffset;

                TestOpenGL.Render.cameraPitch = Math.Clamp(TestOpenGL.Render.cameraPitch, -89.0f, 89.0f);
            }
        }

        private static void OnMouseWheel(ScrollWheel scrollWheel)
        {
            TestOpenGL.Render.cameraZoom = Math.Clamp(TestOpenGL.Render.cameraZoom - scrollWheel.Y, 1.0f, 45f);
        }
        public override void FixedUpdate(float deltaTime)
        {

        }

        public override void SetActive(bool active)
        {

        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {

        }
    }
}
