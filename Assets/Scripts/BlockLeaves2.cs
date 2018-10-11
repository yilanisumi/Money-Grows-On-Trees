using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockLeaves2 : Block
{
    public BlockLeaves2()
        : base()
    {
    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 1;
        tile.y = 13;
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