using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Sun : Component
    {
        public override void Start()
        {

        }

        public override void FixedUpdate(float deltaTime)
        {

        }

        public override void Stop()
        {

        }

        public override void SetActive(bool active)
        {

        }

        public override void Update(float deltaTime)
        {
            gameObject.transform.rotation.Y += 30 * deltaTime;
        }
    }
}
