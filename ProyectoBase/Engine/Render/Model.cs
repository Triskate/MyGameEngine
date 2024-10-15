using Silk.NET.Assimp;
using Silk.NET.OpenGL;
using System.Numerics;
using AssimpMesh = Silk.NET.Assimp.Mesh;

namespace TestOpenGL
{
    public class Model : IDisposable
    {
        public Model(GL gl, string path, bool gamma = false)
        {
            assimp = Assimp.GetApi();
            openGL = gl;
            LoadModel(path);
        }

        readonly GL openGL;
        Assimp assimp;
        List<Texture> textures = new List<Texture>();
        public string directory { get; protected set; } = string.Empty;
        public List<Mesh> meshes { get; protected set; } = new List<Mesh>();

        unsafe void LoadModel(string path)
        {
            Silk.NET.Assimp.Scene* scene = assimp.ImportFile(path, (uint)PostProcessSteps.Triangulate);

            if (scene == null || scene->MFlags == Silk.NET.Assimp.Assimp.SceneFlagsIncomplete || scene->MRootNode == null)
            {
                var error = assimp.GetErrorStringS();
                throw new Exception(error);
            }

            directory = path;
            ProcessNode(scene->MRootNode, scene);
        }

        unsafe void ProcessNode(Node* node, Silk.NET.Assimp.Scene* scene)
        {
            for (var i = 0; i < node->MNumMeshes; i++)
            {
                var mesh = scene->MMeshes[node->MMeshes[i]];
                meshes.Add(ProcessMesh(mesh, scene));

            }

            for (var i = 0; i < node->MNumChildren; i++)
            {
                ProcessNode(node->MChildren[i], scene);
            }
        }

        unsafe Mesh ProcessMesh(AssimpMesh* mesh, Silk.NET.Assimp.Scene* scene)
        {
            // data to fill
            List<Vertex> vertices = new List<Vertex>();
            List<uint> indices = new List<uint>();
            List<Texture> textures = new List<Texture>();

            // walk through each of the mesh's vertices
            for (uint i = 0; i < mesh->MNumVertices; i++)
            {
                Vertex vertex = new Vertex();
                vertex.BoneIds = new int[Vertex.MAX_BONE_INFLUENCE];
                vertex.Weights = new float[Vertex.MAX_BONE_INFLUENCE];

                vertex.Position = mesh->MVertices[i];

                // normals
                if (mesh->MNormals != null)
                    vertex.Normal = mesh->MNormals[i];
                // tangent
                if (mesh->MTangents != null)
                    vertex.Tangent = mesh->MTangents[i];
                // bitangent
                if (mesh->MBitangents != null)
                    vertex.Bitangent = mesh->MBitangents[i];

                // texture coordinates
                if (mesh->MTextureCoords[0] != null) // does the mesh contain texture coordinates?
                {
                    // a vertex can contain up to 8 different texture coordinates. We thus make the assumption that we won't 
                    // use models where a vertex can have multiple texture coordinates so we always take the first set (0).
                    Vector3 texcoord3 = mesh->MTextureCoords[0][i];
                    vertex.TexCoords = new Vector2(texcoord3.X, texcoord3.Y);
                }

                vertices.Add(vertex);
            }

            // now wak through each of the mesh's faces (a face is a mesh its triangle) and retrieve the corresponding vertex indices.
            for (uint i = 0; i < mesh->MNumFaces; i++)
            {
                Face face = mesh->MFaces[i];
                // retrieve all indices of the face and store them in the indices vector
                for (uint j = 0; j < face.MNumIndices; j++)
                    indices.Add(face.MIndices[j]);
            }

            // process materials
            Material* material = scene->MMaterials[mesh->MMaterialIndex];
            // we assume a convention for sampler names in the shaders. Each diffuse texture should be named
            // as 'texture_diffuseN' where N is a sequential number ranging from 1 to MAX_SAMPLER_NUMBER. 
            // Same applies to other texture as the following list summarizes:
            // diffuse: texture_diffuseN
            // specular: texture_specularN
            // normal: texture_normalN

            // 1. diffuse maps
            var diffuseMaps = LoadMaterialTextures(material, TextureType.Diffuse, "texture_diffuse");
            if (diffuseMaps.Any())
                textures.AddRange(diffuseMaps);
            // 2. specular maps
            var specularMaps = LoadMaterialTextures(material, TextureType.Specular, "texture_specular");
            if (specularMaps.Any())
                textures.AddRange(specularMaps);
            // 3. normal maps
            var normalMaps = LoadMaterialTextures(material, TextureType.Height, "texture_normal");
            if (normalMaps.Any())
                textures.AddRange(normalMaps);
            // 4. height maps
            var heightMaps = LoadMaterialTextures(material, TextureType.Ambient, "texture_height");
            if (heightMaps.Any())
                textures.AddRange(heightMaps);

            // return a mesh object created from the extracted mesh data
            var result = new Mesh(openGL, BuildVertices(vertices), BuildIndices(indices), textures);
            return result;
        }

        unsafe List<Texture> LoadMaterialTextures(Material* mat, TextureType type, string typeName)
        {
            var textureCount = assimp.GetMaterialTextureCount(mat, type);
            List<Texture> textures = new List<Texture>();
            for (uint i = 0; i < textureCount; i++)
            {
                AssimpString path;
                assimp.GetMaterialTexture(mat, type, i, &path, null, null, null, null, null, null);
                bool skip = false;
                for (int j = 0; j < textures.Count; j++)
                {
                    if (textures[j].Path == path)
                    {
                        textures.Add(textures[j]);
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    var texture = new Texture(openGL, directory, type);
                    texture.Path = path;
                    textures.Add(texture);
                    textures.Add(texture);
                }
            }
            return textures;
        }

        float[] BuildVertices(List<Vertex> vertexCollection)
        {
            var vertices = new List<float>();

            foreach (var vertex in vertexCollection)
            {
                vertices.Add(vertex.Position.X);
                vertices.Add(vertex.Position.Y);
                vertices.Add(vertex.Position.Z);
                vertices.Add(vertex.TexCoords.X);
                vertices.Add(vertex.TexCoords.Y);
                vertices.Add(vertex.Normal.X);
                vertices.Add(vertex.Normal.Y);
                vertices.Add(vertex.Normal.Z);
            }

            return vertices.ToArray();
        }

        uint[] BuildIndices(List<uint> indices)
        {
            return indices.ToArray();
        }

        public void Dispose()
        {
            foreach (var mesh in meshes)
            {
                mesh.Dispose();
            }

            textures = null;
        }
    }
}
