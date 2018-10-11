using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockGlass : Block
{
    public BlockGlass()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 4;
        tile.y = 4;
        return tile;
    }
    public override string ToString()
    {
        return "Glass Block";
    }

}
