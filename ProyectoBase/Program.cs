using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Numerics;
using System.Reflection;


namespace TestOpenGL
{
    internal class Program
    {

        private static IWindow window;

        static void Main(string[] args)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "My Game Engine";
            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Closing += OnClose;

            window.Run();

            window.Dispose();
        }

        static void OnLoad()
        {
            Assets.Init();
            Input.Init(window);
            Input.onKeyDown += OnKeyDown;
            SceneManager.Init(window);
            Render.Init(window);

            Assets.LoadAssets();
            SceneManager.SetActiveScene("Main.scene");
        }


        private static void OnUpdate(double deltaTime)
        {

        }
        private static void OnClose()
        {
            Assets.UnloadAssets();

            Render.Finish();
            SceneManager.Finish();
            Input.Finish();
            Assets.Finish();
        }

        static void OnKeyDown(Key key, int arg3)
        {
            if (key == Key.Escape)
            {
                window.Close();
            }
            else if(key == Key.F5) 
            {
                Scene scene = SceneManager.GetActiveScene();
                SerializeScene(scene, Assets.GetAssetsPath() + "\\Saved.scene");
            }
            else if(key == Key.F9) 
            {
                DeserializeScene(Assets.GetAssetsPath() + "\\Saved.scene");
            }
        }

        static void SerializeScene(Scene scene, string path)
        {
            int lastId = 0;
            var componentToId = new Dictionary<Component, int>();

            List<GameObject> gameObjects = scene.GetGameObjects();

            // First pass

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject go = gameObjects[i];
                List<Component> components = go.getComponents();

                for (int j = 0; j < components.Count; j++)
                {
                    Component c = components[j];

                    componentToId[c] = lastId;

                    lastId++;
                }
            }

            var writer = new StreamWriter(path);

            writer.WriteLine("gameObjectsCount:" + gameObjects.Count);

            // Second pass

            for (int i = 0; i < gameObjects.Count; i++) 
            {
                GameObject go = gameObjects[i];
                writer.WriteLine("name:" + go.name);
                writer.WriteLine("active:" + go.active);

                List<Component> components = go.getComponents();
                writer.WriteLine("componentsCount:" + components.Count);

                for (int j = 0; j < components.Count; j++) 
                {
                    Component c = components[j];

                    Type type = c.GetType();
                    writer.WriteLine("___id:" + componentToId[c]);
                    writer.WriteLine("___type:" + type.Name);
                    writer.WriteLine("active:" + c.active);

                    FieldInfo[] fields = type.GetFields();
                    writer.WriteLine("fieldsCount:" + fields.Length);

                    for (int k = 0; k < fields.Length; k++)
                    {
                        FieldInfo f = fields[k];
                        writer.WriteLine("___name:" + f.Name);
                        writer.WriteLine("___type:" + f.FieldType.Name);

                        object value = f.GetValue(c);

                        if (f.FieldType.Name == "Vector3")
                        {
                            Vector3 v3 = (Vector3)value;
                            writer.WriteLine("___value:" + v3.X + "|" + v3.Y + "|" + v3.Z);
                        }
                        else if(f.FieldType.Name == "Vector4")
                        {
                            Vector4 v4 = (Vector4)value;
                            writer.WriteLine("___value:" + v4.X + "|" + v4.Y + "|" + v4.Z);
                        }
                        else if (f.FieldType.IsSubclassOf(typeof(Component)))
                        {
                            writer.WriteLine("___value:" + componentToId[(Component)value]);
                        }
                        else
                        {
                            writer.WriteLine("___value:" + value.ToString());
                        }
                    }
                }
            }
            Console.WriteLine("Saved scene to file: " + path);
            writer.Close();
        }

        static Scene DeserializeScene(string path)
        {
            var idToComponent = new Dictionary<int, Component>();
            Scene scene = new Scene();

            StreamReader reader;

            for(int p =  0; p < 2; p++)
            {
                reader = new StreamReader(path);
                string line;

                line = reader.ReadLine();
                int gameObjectCount = Int32.Parse(line.Split(':')[1]);

                for (int i = 0; i < gameObjectCount; i++)
                {
                    GameObject go = new GameObject(false);

                    line = reader.ReadLine();
                    go.name = line.Split(':')[1];

                    line = reader.ReadLine();
                    go.active = line.Split(':')[1] == true.ToString();

                    List<Component> components = new List<Component>();

                    line = reader.ReadLine();
                    int componentsCount = Int32.Parse(line.Split(':')[1]);

                    for (int j = 0; j < componentsCount; j++)
                    {
                        line = reader.ReadLine();
                        int id = Int32.Parse(line.Split(':')[1]);

                        line = reader.ReadLine();
                        string typeName = line.Split(":")[1];

                        Type type = Type.GetType("TestOpenGL." + typeName);

                        Object componentObject = Activator.CreateInstance(type);
                        Component component = (Component)componentObject;

                        if (typeName == "Transform")
                        {
                            go.transform = (Transform)component;
                        }

                        if (p == 0) { idToComponent[id] = component; }

                        line = reader.ReadLine();
                        component.active = line.Split(':')[1] == true.ToString();

                        line = reader.ReadLine();
                        int fieldsCount = Int32.Parse(line.Split(':')[1]);

                        for (int k = 0; k < fieldsCount; k++)
                        {
                            line = reader.ReadLine();
                            string fieldName = line.Split(":")[1];

                            line = reader.ReadLine();
                            string fieldTypeName = line.Split(":")[1];

                            line = reader.ReadLine();
                            string fieldValueString = line.Split(":")[1];

                            FieldInfo field = type.GetField(fieldName);
                            object fieldValue = null;


                            if (fieldTypeName == "Int32")
                            {
                                fieldValue = Int32.Parse(fieldValueString);
                            }
                            else if (fieldTypeName == "Single")
                            {
                                fieldValue = Single.Parse(fieldValueString);
                            }
                            else if (fieldTypeName == "Boolean")
                            {
                                fieldValue = (fieldValueString == "True");
                            }
                            else if (fieldTypeName == "Vector3")
                            {
                                string[] parts = fieldValueString.Split("|");
                                float x = Single.Parse(parts[0]);
                                float y = Single.Parse(parts[1]);
                                float z = Single.Parse(parts[2]);
                                fieldValue = new Vector3(x, y, z);
                            }
                            else if (fieldTypeName == "Vector4")
                            {
                                string[] parts = fieldValueString.Split("|");
                                float x = Single.Parse(parts[0]);
                                float y = Single.Parse(parts[1]);
                                float z = Single.Parse(parts[2]);
                                float w = Single.Parse(parts[3]);
                                fieldValue = new Vector4(x, y, z, w);
                            }
                            else if (field.FieldType.IsSubclassOf(typeof(Component)) && p == 1)
                            {
                                int fieldId = Int32.Parse(fieldValueString);
                                fieldValue = idToComponent[fieldId];
                            }

                            field.SetValue(componentObject, fieldValue);

                        }// Fields Loop

                        go.AddComponent(component);

                    }// Component Loop

                    scene.AddGameObject(go);

                }// GameObject Loop

                reader.Close();
            }// Pass Loop
            
            return scene;
        }
    }
}
