using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PrefabBlockTexture : MonoBehaviour
{
    public struct Tile { public int x; public int y; }
    const float tilesPerSide = 4.0f;
    const float tileSize = 1.0f / tilesPerSide;
    //ONLY FOR THIS TEXTURE PACK BY KENNEY
    const float tileXSize = 1.0f / 6.0f;
    const float tileYSize = 1.0f / 15.0f;
    const float width = 780f;
    const float height = 1950f;
    MeshData meshData;
    MeshFilter filter;
    public string blockType;
    //player changed this block
    public bool upsideDown = false;
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        meshData = new MeshData();
        Blockdata(0, 0, 0);
        RenderMesh();
    }


    void RenderMesh()
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();

        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();
    }
    //checks if the block is solid in that direction
    public virtual bool IsSolid(Block.Direction direction)
    {
        switch (direction)
        {
            case Block.Direction.north:
                return true;
            case Block.Direction.east:
                return true;
            case Block.Direction.south:
                return true;
            case Block.Direction.west:
                return true;
            case Block.Direction.up:
                return true;
            case Block.Direction.down:
                return true;
        }
        return false;
    }

    //For each face, we need to check if the block in that direction is solid
    public virtual MeshData Blockdata(int x, int y, int z)
    {
        //remember to include this when overriding BlockData
        // meshData.useRenderDataForCol = true;
        meshData = FaceDataUp(x, y, z);
        meshData = FaceDataDown(x, y, z);
        meshData = FaceDataNorth(x, y, z);
        meshData = FaceDataSouth(x, y, z);
        meshData = FaceDataEast(x, y, z);
        meshData = FaceDataWest(x, y, z);
        return meshData;
    }
    //If the block facing this face is not solid, this face is visible
    //so we add data for the vertices for the side
    protected virtual MeshData FaceDataUp(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.up));
        return meshData;
    }

    protected virtual MeshData FaceDataDown(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.down));
        return meshData;
    }

    protected virtual MeshData FaceDataNorth(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.north));
        return meshData;
    }

    protected virtual MeshData FaceDataEast(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.east));
        return meshData;
    }

    protected virtual MeshData FaceDataSouth(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.south));
        return meshData;
    }

    protected virtual MeshData FaceDataWest(int x, int y, int z)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(Block.Direction.west));
        return meshData;
    }

    public virtual Block.Tile TexturePosition(Block.Direction direction)
    {
        Block.Tile tile = new Block.Tile();

        switch (blockType)
        {
            case "Stone":
                Block blockStone = new Block();
                blockStone.upsideDown = upsideDown;
                tile = blockStone.TexturePosition(direction);
                return tile;
            case "Grass":
                BlockGrass blockGrass = new BlockGrass();
                blockGrass.upsideDown = upsideDown;
                tile = blockGrass.TexturePosition(direction);
                return tile;
            case "BrownWood":
                BlockBrownWood blockBrownWood = new BlockBrownWood();
                blockBrownWood.upsideDown = upsideDown;
                tile = blockBrownWood.TexturePosition(direction);
                return tile;
            case "Lava":
                BlockLava blockLava = new BlockLava();
                blockLava.upsideDown = upsideDown;
                tile = blockLava.TexturePosition(direction);
                return tile;
            case "GreenLeaves":
                BlockGreenLeaves blockGreenLeaves = new BlockGreenLeaves();
                blockGreenLeaves.upsideDown = upsideDown;
                tile = blockGreenLeaves.TexturePosition(direction);
                return tile;
            case "OrangeLeaves":
                BlockOrangeLeaves blockOrangeLeaves = new BlockOrangeLeaves();
                blockOrangeLeaves.upsideDown = upsideDown;
                tile = blockOrangeLeaves.TexturePosition(direction);
                return tile;
            case "StoneSnow":
                BlockStoneSnow blockStoneSnow = new BlockStoneSnow();
                blockStoneSnow.upsideDown = upsideDown;
                tile = blockStoneSnow.TexturePosition(direction);
                return tile;
            case "Ice":
                BlockIce blockIce = new BlockIce();
                blockIce.upsideDown = upsideDown;
                tile = blockIce.TexturePosition(direction);
                return tile;
            case "Glass":
                BlockGlass blockGlass = new BlockGlass();
                blockGlass.upsideDown = upsideDown;
                tile = blockGlass.TexturePosition(direction);
                return tile;
            case "WhiteWood":
                BlockWhiteWood blockWhiteWood = new BlockWhiteWood();
                blockWhiteWood.upsideDown = upsideDown;
                tile = blockWhiteWood.TexturePosition(direction);
                return tile;
            case "Apple":
                BlockApple blockApple = new BlockApple();
                blockApple.upsideDown = upsideDown;
                tile = blockApple.TexturePosition(direction);
                return tile;
            case "Alphabet":
                BlockAlphabet blockAlphabet = new BlockAlphabet();
                blockAlphabet.upsideDown = upsideDown;
                tile = blockAlphabet.TexturePosition(direction);
                return tile;
            case "Amazon":
                BlockAmazon blockAmazon = new BlockAmazon();
                blockAmazon.upsideDown = upsideDown;
                tile = blockAmazon.TexturePosition(direction);
                return tile;
            case "Facebook":
                BlockFacebook blockFacebook = new BlockFacebook();
                blockFacebook.upsideDown = upsideDown;
                tile = blockFacebook.TexturePosition(direction);
                return tile;
            case "IBM":
                BlockIBM blockIBM = new BlockIBM();
                blockIBM.upsideDown = upsideDown;
                tile = blockIBM.TexturePosition(direction);
                return tile;
            case "Intel":
                BlockIntel blockIntel = new BlockIntel();
                blockIntel.upsideDown = upsideDown;
                tile = blockIntel.TexturePosition(direction);
                return tile;
            case "Microsoft":
                BlockMicrosoft blockMicrosoft = new BlockMicrosoft();
                blockMicrosoft.upsideDown = upsideDown;
                tile = blockMicrosoft.TexturePosition(direction);
                return tile;
            case "Netflix":
                BlockNetflix blockNetflix = new BlockNetflix();
                blockNetflix.upsideDown = upsideDown;
                tile = blockNetflix.TexturePosition(direction);
                return tile;
            case "Twitter":
                BlockTwitter blockTwitter = new BlockTwitter();
                blockTwitter.upsideDown = upsideDown;
                tile = blockTwitter.TexturePosition(direction);
                return tile;
            case "Yahoo":
                BlockYahoo blockYahoo = new BlockYahoo();
                blockYahoo.upsideDown = upsideDown;
                tile = blockYahoo.TexturePosition(direction);
                return tile;
            case "Leaves0":
                BlockLeaves0 blockLeaves0 = new BlockLeaves0();
                blockLeaves0.upsideDown = upsideDown;
                tile = blockLeaves0.TexturePosition(direction);
                return tile;
            case "Leaves1":
                BlockLeaves1 blockLeaves1 = new BlockLeaves1();
                blockLeaves1.upsideDown = upsideDown;
                tile = blockLeaves1.TexturePosition(direction);
                return tile;
            case "Leaves2":
                BlockLeaves2 blockLeaves2 = new BlockLeaves2();
                blockLeaves2.upsideDown = upsideDown;
                tile = blockLeaves2.TexturePosition(direction);
                return tile;
            case "Leaves3":
                BlockLeaves3 blockLeaves3 = new BlockLeaves3();
                blockLeaves3.upsideDown = upsideDown;
                tile = blockLeaves3.TexturePosition(direction);
                return tile;
            case "Leaves4":
                BlockLeaves4 blockLeaves4 = new BlockLeaves4();
                blockLeaves4.upsideDown = upsideDown;
                tile = blockLeaves4.TexturePosition(direction);
                return tile;
        }
        //just in case it's not registered
        Block blockAir = new BlockAir();
        tile = blockAir.TexturePosition(direction);
        return tile;
    }

    public virtual Vector2[] FaceUVs(Block.Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Block.Tile tilePos = TexturePosition(direction);
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
}
