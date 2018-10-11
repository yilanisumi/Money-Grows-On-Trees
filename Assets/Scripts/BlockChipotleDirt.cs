using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockChipotleDirt : Block
{
    public BlockChipotleDirt()
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
                tile.y = 9;
                return tile;

        }

        tile.x = 5;
        tile.y = 14;

        return tile;
    }
    public override string ToString()
    {
        return "Chipotle Dirt Block";
    }

}
