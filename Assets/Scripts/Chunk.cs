using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{
    public static int chunkSize = 16;
    public bool update = false;
    public World world;
    public WorldPos pos;
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];
    public bool highlighted = false;
    //checks if block is rendered
    public bool rendered;
    MeshFilter filter;
    MeshCollider coll;
    //  Shader diffuseShader;
    //  Shader highlightShader;
    //   Material mat;
    // Use this for initialization
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        coll = GetComponent<MeshCollider>();
        //     diffuseShader = Shader.Find("Legacy Shaders/Transparent/Cutout/Diffuse");
        //      highlightShader = Shader.Find("Legacy Shaders/Self-Illumin/Diffuse");
        //     mat = renderer.material;
    }


    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }
    /*
     * void LateUpdate()
        {
            if (highlighted)
            {
                if (mat.shader != highlightShader)
                {
                    mat.shader = highlightShader;
                }
            }
            else
            {
                if (mat.shader != diffuseShader)
                {
                    mat.shader = diffuseShader;
                }
            }
            highlighted = false;
        }
     * */


    //check if coordinates in range
    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
        {
            return false;
        }
        return true;
    }

    //get block at coordinate
    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            return blocks[x, y, z];
        }
        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    //Updates chunk based on its contents
    void UpdateChunk()
    {
        rendered = true;
        MeshData meshData = new MeshData();
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }
    /*
     * Sends the calculated mesh information
     * to the mesh and collision components
     */
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();

        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        coll.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        coll.sharedMesh = mesh;
    }
    //set all blocks to not changed by player
    //used when we automatically generate terrain
    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.changed = false;
        }
    }
}
