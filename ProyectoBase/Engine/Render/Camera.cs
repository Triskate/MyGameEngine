using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Camera : Component
    {
        public float fov = 100f;
        public Color backgroundColor = Color.AliceBlue;
        // Añadir a la funcion
        public float zNear = 0.1f;
        public float zFar = 1000.0f;
        public override void Update(float deltaTime)
        {
            Transform t = gameObject.transform;
            TestOpenGL.Render.SetView(t.position, t.rotation, fov, backgroundColor);
        }
        public override void FixedUpdate(float deltaTime)
        {

        }

        public override void Render(float deltaTime)
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
