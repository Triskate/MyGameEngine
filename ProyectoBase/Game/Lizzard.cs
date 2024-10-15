using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Lizzard : Component
    {
        public Transform target;

        public override void Update(float deltaTime)
        {
            Vector3 direction = Vector3.Normalize(target.position - gameObject.transform.position);

            gameObject.transform.position += direction * deltaTime;

        }

        public override void FixedUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public override void SetActive(bool active)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
