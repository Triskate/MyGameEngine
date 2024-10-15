using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class SceneLoader : AssetLoader
    {
        public override object LoadAsset(string path)
        {
            var scene = new Scene();

            scene.name = "New scene";

            GameObject go;
            Component c;
            Transform t;
            Renderer r;
            Camera cam;
            CameraMovement cm;
            DirectionalLight dl;
            PointLight pl;

            GameObject sirMartinO;
            Lizzard lizzardC;

            // GameManager
            go = new GameObject();
            go.name = "GameManager";

            c = new GameManager();
            go.AddComponent(c);

            scene.AddGameObject(go);

            // Sun
            go = new GameObject();
            go.name = "Sun";

            
            dl = new DirectionalLight();
            dl.color = new Vector3(0.7f, 0.5f, 0);
            dl.intensity = 2;
            go.AddComponent(dl);
            

            pl = new PointLight();
            pl.color = new Vector3(0.7f, 0.5f, 0);
            pl.intensity = 2;
            go.AddComponent(pl);

            c = new Sun();
            go.AddComponent(c);

            go.transform.rotation.X = -45;

            scene.AddGameObject(go);

            /*
            // LightPoint
            go = new GameObject();
            go.name = "LightPoint";

            c = new LightPoint();
            go.AddComponent(c);

            pl = new PointLight();
            pl.intensity = 0.3f;
            go.AddComponent(pl);

            go.transform.position = new Vector3(0, 0, -10);
            go.transform.rotation = new Vector3(0, 0, 0);

            scene.AddGameObject(go) ;
            */

            // Ground
            go = new GameObject();
            go.name = "Ground";

            c = new Ground();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "Terrain.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Grass.png";
            go.AddComponent(r);

            go.transform.position = new Vector3(0, -0.5f, 0);

            scene.AddGameObject(go);

            // Sir Martin
            go = new GameObject();
            go.name = "SirMartin";

            c = new SirMartin();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "Model2.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Wood.png";
            go.AddComponent(r);

            go.transform.position = new Vector3(0, -0.5f, 0);

            sirMartinO = go;
            scene.AddGameObject(go);

            // Gargoyle
            go = new GameObject();
            go.name = "Gargoyle";

            c = new SirMartin();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "Gargoyle.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Rock.png";
            go.AddComponent(r);

            lizzardC = new Lizzard();
            lizzardC.target = sirMartinO.transform;
            go.AddComponent(lizzardC);

            go.transform.position = new Vector3(0, 0, -6);
            go.transform.rotation = new Vector3(0, 0, 0);

            scene.AddGameObject(go);

            // Rock1

            go = new GameObject();
            go.name = "Rock1";

            c = new Rock1();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "RockMedium1.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Rock.png";
            go.AddComponent(r);

            go.transform.position = new Vector3(20, 0, -10);

            scene.AddGameObject(go);

            // Rock2

            go = new GameObject();
            go.name = "Rock2";

            c = new Rock2();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "RockMedium2.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Rock.png";
            go.AddComponent(r);

            go.transform.position = new Vector3(-20, 0, 10);

            scene.AddGameObject(go);

            // Box
            go = new GameObject();
            go.name = "Box";

            c = new Box();
            go.AddComponent(c);

            r = new Renderer();
            r.modelId = "Model1.obj";
            r.shaderId = "Shaders1.cg";
            r.textureId = "Texture1.png";
            go.AddComponent(r);

            go.transform.position = new Vector3(2, 0, 0);

            scene.AddGameObject(go);

            // Camera
            go = new GameObject();
            go.name = "Camera";

            cam = new Camera();
            cam.fov = 45f;
            go.AddComponent(cam);

            cm = new CameraMovement();
            cm.Start();
            go.AddComponent(cm);

            scene.AddGameObject(go);

            Console.WriteLine("Main lodaded");

            return scene;
        }
        public override void UnloadAsset(object o) 
        {
        }
    }
}
