using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class PointLight : Component
    {
        public Vector3 color = new Vector3(1, 1, 1);
        public float intensity = 1.0f;

        public override void Update(float deltaTime)
        {
            Vector3 position = gameObject.transform.position;
            TestOpenGL.Render.SetPointLight(position, color, intensity);
        }
        public override void FixedUpdate(float deltaTime){}

        public override void SetActive(bool active){}

        public override void Start(){}

        public override void Stop(){}
    }
}
