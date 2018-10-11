using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockGrass : Block
{
    public BlockGrass()
        : base()
    {

    }
    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.up:
                if (!upsideDown)
                {
                    tile.x = 4;
                    tile.y = 8;
                }
                else
                {
                    tile.x = 5;
                    tile.y = 13;
                }

                return tile;
            case Direction.down:
                if (!upsideDown)
                {
                    tile.x = 5;
                    tile.y = 13;
                }
                else
                {
                    tile.x = 4;
                    tile.y = 8;
                }

                return tile;
        }
        tile.x = 5;
        tile.y = 14;
        return tile;
    }
    public override string ToString()
    {
        return "Grass Block";
    }
}
