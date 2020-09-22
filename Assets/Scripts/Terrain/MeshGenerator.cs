using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMutiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int heigth = heightMap.GetLength(1);

        // Get the top left coordinate of mesh
        float topleftX = (width - 1) / -2f;
        float topLeftZ = (heigth - 1) / 2f;

        // Set the simplification of mesh, if this value = 0 set 1
        int meshSimplificationIncrement = (levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1; 

        // Initialize mesh data
        MeshData meshData = new MeshData(verticesPerLine);
        // Initialize index to keep track the vertex
        int vertexIndex = 0;

        // loop through map
        for (int y = 0; y < heigth; y += meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x += meshSimplificationIncrement)
            {
                // Initialize vertices at the vertex index
                meshData.vertices[vertexIndex] = new Vector3(topleftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMutiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)heigth);

                // Add triangle if the coordinate is not at final right or final bottom
                if (x < width - 1 && y < heigth - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        //meshData.FlatShading();
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    // Initialize index to keep track the triangle
    int trianglesIndex;

    // Initialize vertices, uv and triangles 
    public MeshData(int verticesPerLine)
    {
        vertices = new Vector3[verticesPerLine * verticesPerLine];
        uvs = new Vector2[verticesPerLine * verticesPerLine];
        triangles = new int[(verticesPerLine - 1) * (verticesPerLine - 1) * 6];
    }

    // Add triangles to mesh by 3 vertices
    public void AddTriangle(int a, int b, int c)
    {
        triangles[trianglesIndex] = a;
        triangles[trianglesIndex + 1] = b;
        triangles[trianglesIndex + 2] = c;
        trianglesIndex+=3;
    }

    // Flatshading for low poly
    //public void FlatShading()
    //{
    //   Vector3[] flatShadedVertices = new Vector3[triangles.Length];
    //    Vector2[] flatShadedUvs = new Vector2[triangles.Length];

    //    for (int i = 0; i < triangles.Length; i++)
    //   {
    //        flatShadedVertices[i] = vertices[triangles[i]];
    //        flatShadedUvs[i] = uvs[triangles[i]];
    //        triangles[i] = i;
    //    }

    //    vertices = flatShadedVertices;
    //    uvs = flatShadedUvs;
    //}

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
