using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockGreenLeaves : Block
{
    public BlockGreenLeaves()
        : base()
    {
    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 3;
        tile.y = 10;
        return tile;
    }
    public override bool IsSolid(Direction direction)
    {
        return false;
    }

    public override string ToString()
    {
        return "Stock " + company + " Block";
    }
}