using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    internal class Transform : Component
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Transform()
        {
            position = new Vector3(0,0,0);
            rotation = new Vector3(0,0,0);
            scale = new Vector3(1,1,1);
        }

        public Matrix4x4 GetLocalToWorld()
        {
            var matrix = Matrix4x4.CreateRotationZ(rotation.Z * MathF.PI / 180) *
                  Matrix4x4.CreateRotationY(rotation.Y * MathF.PI / 180) *
                  Matrix4x4.CreateRotationX(rotation.X * MathF.PI / 180) *
                  Matrix4x4.CreateTranslation(position);

            return matrix;
        }

        public Vector3 TransformDirection(Vector3 dir)
        {
            Matrix4x4 localToWorld = GetLocalToWorld();
            Vector4 dir4 = new Vector4(dir, 0);
            Vector4 result4 = Vector4.Transform(dir4, localToWorld);
            Vector3 result = new Vector3(result4.X, result4.Y, result4.Z);

            return result;
        }

        public Vector3 TransformPosition(Vector3 pos)
        {
            Matrix4x4 localToWorld = GetLocalToWorld();
            Vector4 pos4 = new Vector4(pos, 0);
            Vector4 result4 = Vector4.Transform(pos4, localToWorld);
            Vector3 result = new Vector3(result4.X, result4.Y, result4.Z);

            return result;
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

        public override void Update(float deltaTime)
        {

        }
    }
}
