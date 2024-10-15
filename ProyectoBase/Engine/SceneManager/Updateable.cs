using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal interface Updateable
    {
        public void Start() { }
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void Render(float deltaTime) { }
        public void Stop() { }
    }
}
