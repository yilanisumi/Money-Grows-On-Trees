using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class BlockBrownWood : Block
{
    public BlockBrownWood()
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
        tile.x = 0;
        tile.y = 3;
        return tile;
    }
    public override string ToString()
    {
        return "Brown Wood Block";
    }
}