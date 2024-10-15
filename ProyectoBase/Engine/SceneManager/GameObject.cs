using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TestOpenGL
{
    internal class GameObject : Updateable, Activable
    {

        List<Component> components;

        public string name;
        public bool active;

        public Transform transform;

        public GameObject(bool addTransform = true) 
        {
            name = "";
            active = true;

            components = new List<Component>();

            if (addTransform)
            {
                transform = new Transform();
                components.Add(transform);
                transform.setGameObject(this);
            }
        }
        public void AddComponent(Component c)
        {
            components.Add(c);
            c.setGameObject(this);
        }
        public List<Component> getComponents() { return components; }

        public void Start() { }
        public void Update(float deltaTime) 
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(deltaTime);
            }
        } 
        public void FixedUpdate(float deltaTime) { }
        public void Render(float deltaTime) 
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Render(deltaTime);
            }
        }
        public void SetView(float deltaTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].SetView(deltaTime);
            }
        }
        public void Stop() { }
        public void SetActive(bool active) { }
    }
}
