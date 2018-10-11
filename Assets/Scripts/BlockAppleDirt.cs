using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockAppleDirt : Block
{
    public BlockAppleDirt()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                tile.x = 2;
                tile.y = 10;
                return tile;
        }
        tile.x = 5;
        tile.y = 14;

        return tile;
    }
    public override string ToString()
    {
        return "Apple Dirt Block";
    }

}
