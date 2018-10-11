using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Block
{
    public struct Tile { public int x; public int y; }
    public enum Direction { north, east, south, west, up, down };
    const float tilesPerSide = 4.0f;
    const float tileSize = 1.0f / tilesPerSide;
    //ONLY FOR THIS TEXTURE PACK BY KENNEY
    public const float tileXSize = 1.0f / 6.0f;
    public const float tileYSize = 1.0f / 15.0f;
    public const float width = 780f;
    public const float height = 1950f;
    //player changed this block
    public bool changed = true;
    public bool upsideDown = false;
    public bool selling = false;

    public string company = "";
    //Base block constructor
    public Block()
    {

    }
    public void SetCompany(string name)
    {
        company = name;
    }
    //checks if the block is solid in that direction
    public virtual bool IsSolid(Direction direction)
    {
        switch (direction)
        {
            case Direction.north:
                return true;
            case Direction.east:
                return true;
            case Direction.south:
                return true;
            case Direction.west:
                return true;
            case Direction.up:
                return true;
            case Direction.down:
                return true;
        }
        return false;
    }

    //For each face, we need to check if the block in that direction is solid
    public virtual MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        //remember to include this when overriding BlockData
        meshData.useRenderDataForCol = true;
        if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.down))
        {
            meshData = FaceDataUp(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.up))
        {
            meshData = FaceDataDown(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.south))
        {
            meshData = FaceDataNorth(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.north))
        {
            meshData = FaceDataSouth(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.west))
        {
            meshData = FaceDataEast(chunk, x, y, z, meshData);
        }
        if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.east))
        {
            meshData = FaceDataWest(chunk, x, y, z, meshData);
        }
        return meshData;
    }
    //If the block facing this face is not solid, this face is visible
    //so we add data for the vertices for the side
    protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.up));
        return meshData;
    }

    protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.down));
        return meshData;
    }

    protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.north));
        return meshData;
    }

    protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.east));
        return meshData;
    }

    protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.south));
        return meshData;
    }

    protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Direction.west));
        return meshData;
    }

    public virtual Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        //Stone block
        tile.x = 2;
        tile.y = 9;
        return tile;
    }

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);
        float precision = 0.001f;
        //ONLY WORKS FOR THIS WEIRD TEXTURE PACK BY KENNEY
        if (!upsideDown)
        {
            //bottom right
            UVs[0] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + 2.0f / width + precision);
            //top right
            UVs[1] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + tileYSize - precision);
            //top left
            UVs[2] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + tileYSize - precision);
            //bottom left
            UVs[3] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + 2.0f / width + precision);

        }
        else
        {
            //bottom right
            UVs[1] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + 2.0f / width + precision);
            //top right
            UVs[0] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + tileYSize - precision);
            //top left
            UVs[3] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + tileYSize - precision);
            //bottom left
            UVs[2] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + 2.0f / width + precision);
        }

        return UVs;
    }
    public override string ToString()
    {
        return "Stone Block";
    }
}
