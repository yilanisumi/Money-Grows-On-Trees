using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockYahoo : Block
{
    public BlockYahoo()
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
        tile.x = 4;
        tile.y = 6;
        return tile;
    }
    public override string ToString()
    {
        return "Stock Yahoo Block";
    }

}
