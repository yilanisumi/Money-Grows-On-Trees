using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockLeaves1 : Block
{
    public BlockLeaves1()
        : base()
    {
    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 3;
        tile.y = 9;
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