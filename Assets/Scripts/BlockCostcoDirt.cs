using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockCostcoDirt : Block
{
    public BlockCostcoDirt()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                tile.x = 1;
                tile.y = 9;
                return tile;
        }
        tile.x = 5;
        tile.y = 14;
        return tile;
    }
    public override string ToString()
    {
        return "Costco Dirt Block";
    }

}
