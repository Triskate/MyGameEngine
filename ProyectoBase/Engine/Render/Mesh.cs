using Silk.NET.OpenGL;

namespace TestOpenGL
{
    public class Mesh : IDisposable
    {
        public float[] vertices { get; set; }
        public uint[] indices { get; set; }
        public IReadOnlyList<Texture> textures { get; set; }
        public VertexArrayObject<float, uint> vertexArrayObject { get; set; }
        public BufferObject<float> vertexBufferObject { get; set; }
        public BufferObject<uint> elementBufferObject { get; set; }
        public GL openGL { get; }

        public Mesh(GL gl, float[] _vertices, uint[] _indices, List<Texture> _textures)
        {
            openGL = gl;
            vertices = _vertices;
            indices = _indices;
            textures = _textures;
            SetupMesh();
        }

        public void SetupMesh()
        {
            elementBufferObject = new BufferObject<uint>(openGL, indices, BufferTargetARB.ElementArrayBuffer);
            vertexBufferObject = new BufferObject<float>(openGL, vertices, BufferTargetARB.ArrayBuffer);
            vertexArrayObject = new VertexArrayObject<float, uint>(openGL, vertexBufferObject, elementBufferObject);
            vertexArrayObject.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
            vertexArrayObject.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 8, 3);
            vertexArrayObject.VertexAttributePointer(2, 3, VertexAttribPointerType.Float, 8, 5);
        }

        public void Bind()
        {
            vertexArrayObject.Bind();
        }

        public void Dispose()
        {
            textures = null;
            vertexArrayObject.Dispose();
            vertexBufferObject.Dispose();
            elementBufferObject.Dispose();
        }
    }
}
