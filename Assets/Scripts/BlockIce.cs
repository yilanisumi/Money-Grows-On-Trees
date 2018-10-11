using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class BlockIce : Block
{

    public BlockIce()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 3;
        tile.y = 5;
        return tile;
    }
    public override string ToString()
    {
        return "Ice Block";
    }

}
