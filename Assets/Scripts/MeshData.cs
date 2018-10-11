using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//does not inherit from monobehavior because we won't be using it in the scene
public class MeshData
{
    //mesh vertices, triangles, and texture coordiantes
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();

    //collider mesh vertices and triangles
    public List<Vector3> colVertices = new List<Vector3>();
    public List<int> colTriangles = new List<int>();

    public bool useRenderDataForCol;
    public MeshData()
    {

    }
    //vertices are added in clockwise order
    public void AddQuadTriangles()
    {
        //first triangle
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        //second triangle
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (useRenderDataForCol)
        {
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 3);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 1);

        }
    }

    //add vertex
    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex);
        if (useRenderDataForCol)
        {
            colVertices.Add(vertex);
        }
    }

    public void AddTriangle(int tri)
    {
        triangles.Add(tri);
        if (useRenderDataForCol)
        {
            //need to adjust the value because triangles list entreis correspond to indexes in the vertices list
            colTriangles.Add(tri - (vertices.Count - colVertices.Count));
        }
    }
}
