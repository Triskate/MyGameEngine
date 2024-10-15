using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Renderer : Component
    {
        public string modelId;
        public string shaderId;
        public string textureId;

        public override void Render(float deltaTime)
        {
            Transform tf = gameObject.transform;
            Model m = Assets.GetLoadedAsset<Model>(modelId);
            Shader s = Assets.GetLoadedAsset<Shader>(shaderId);
            Texture t = Assets.GetLoadedAsset<Texture>(textureId);
            
            TestOpenGL.Render.DrawModel(tf.position, tf.rotation, tf.scale, m, s, t);
        }

        public override void FixedUpdate(float deltaTime)
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

        public override void Update(float deltaTime)
        {

        }
    }
}
