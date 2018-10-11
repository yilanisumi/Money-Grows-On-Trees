using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockGarmin : Block
{
    public BlockGarmin()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                tile.x = 0;
                tile.y = 4;
                return tile;
            case Direction.down:
                tile.x = 0;
                tile.y = 4;
                return tile;
        }
        tile.x = 1;
        tile.y = 5;
        return tile;
    }
    public override string ToString()
    {
        return "Stock Garmin Block";
    }

}
