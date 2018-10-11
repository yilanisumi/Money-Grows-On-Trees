using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class BlockStoneSnow : Block
{

    public BlockStoneSnow()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.down:
                if (!upsideDown)
                {
                    tile.x = 2;
                    tile.y = 9;
                }
                else
                {
                    tile.x = 2;
                    tile.y = 8;
                }

                return tile;
            case Direction.up:
                if (!upsideDown)
                {
                    tile.x = 2;
                    tile.y = 8;
                }
                else
                {
                    tile.x = 2;
                    tile.y = 9;
                }


                return tile;
        }
        tile.x = 1;
        tile.y = 10;
        return tile;
    }
    /*
      public override Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);
        float precision = 0.001f;
        //ONLY WORKS FOR THIS WEIRD TEXTURE PACK BY KENNEY
        //bottom right
        UVs[1] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + 2.0f / width + precision);
        //top right
        UVs[0] = new Vector2(tileXSize * tilePos.x + tileXSize - 2.0f / width - precision, tileYSize * tilePos.y + tileYSize - precision);
        //top left
        UVs[3] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + tileYSize - precision);
        //bottom left
        UVs[2] = new Vector2(tileXSize * tilePos.x + 2.0f / width + precision, tileYSize * tilePos.y + 2.0f / width + precision);

        return UVs;
    }
    */

    public override string ToString()
    {
        return "Stone Snow Block";
    }
}
