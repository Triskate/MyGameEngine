using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Scene
    {
        List<GameObject> gameObjects;

        public string name;

        public Scene()
        {
            gameObjects = new List<GameObject>();
        }

        public void AddGameObject(GameObject go)
        {
            gameObjects.Add(go);
        }
        public void Update(float deltaTime)
        {
            for(int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(deltaTime);
            }
        }
        public void Render(float  deltaTime) 
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Render(deltaTime);
            }
        }
        public void SetView(float deltaTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].SetView(deltaTime);
            }
        }
        public List<GameObject> GetGameObjects() { return gameObjects; }
    }
}
