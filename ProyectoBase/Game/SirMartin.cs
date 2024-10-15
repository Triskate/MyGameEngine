using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class SirMartin : Component
    {
        public override void Update(float deltaTime)
        {
            Vector3 p = gameObject.transform.position;

            p += new Vector3(0, 0, 5) * deltaTime;

            gameObject.transform.position = p;
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
