using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class BlockWhiteWood : Block
{
    public BlockWhiteWood()
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
                tile.y = 6;
                return tile;
            case Direction.down:
                tile.x = 0;
                tile.y = 6;
                return tile;
        }
        tile.x = 0;
        tile.y = 5;
        return tile;
    }
    public override string ToString()
    {
        return "White Wood Block";
    }
}