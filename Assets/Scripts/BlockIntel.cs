using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockIntel : Block
{
    public BlockIntel()
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
        tile.x = 5;
        tile.y = 3;
        return tile;
    }
    public override string ToString()
    {
        return "Stock Intel Block";
    }

}
