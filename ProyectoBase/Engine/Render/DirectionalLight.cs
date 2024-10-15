using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class DirectionalLight : Component
    {

        public Vector3 color = new Vector3(1, 1, 1);
        public float intensity = 1.0f;

        public override void Update(float deltaTime)
        {
            Vector3 direction = gameObject.transform.TransformDirection(Vector3.UnitZ);
            TestOpenGL.Render.SetDirectionalLight(direction, color, intensity);
        }
        public override void FixedUpdate(float deltaTime) { }

        public override void Render(float deltaTime) { }

        public override void SetActive(bool active) { }

        public override void Start() { }

        public override void Stop() { }
    }
}
