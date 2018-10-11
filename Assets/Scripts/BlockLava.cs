using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class BlockLava : Block
{

    public BlockLava()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 3;
        tile.y = 6;
        return tile;
    }
    public override string ToString()
    {
        return "Lava Block";
    }
}
