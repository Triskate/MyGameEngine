using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal abstract class Component : Updateable, Activable
    {
        public bool active;

        protected GameObject gameObject;

        public void setGameObject(GameObject go)
        {
            gameObject = go;
        }

        public abstract void Start();
        public virtual void Update(float deltaTime) { }
        public abstract void FixedUpdate(float deltaTime);
        public virtual void Render(float deltaTime) { }
        public virtual void SetView(float deltaTime) { }
        public abstract void Stop();
        public abstract void SetActive(bool active);
    }
}
