using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockMicrosoftDirt : Block
{
    public BlockMicrosoftDirt()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                tile.x = 3;
                tile.y = 14;
                return tile;
        }
        tile.x = 5;
        tile.y = 14;
        return tile;
    }
    public override string ToString()
    {
        return "Microsoft Dirt Block";
    }

}
