using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockAlphabetDirt : Block
{
    public BlockAlphabetDirt()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                tile.x = 4;
                tile.y = 12;
                return tile;
        }
        tile.x = 5;
        tile.y = 14;
        return tile;
    }
    public override string ToString()
    {
        return "Alphabet Dirt Block";
    }

}
