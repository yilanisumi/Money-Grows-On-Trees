using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//world transform position of chunks
[Serializable]
public struct WorldPos
{
    public int x, y, z;
    public WorldPos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        if (GetHashCode() == obj.GetHashCode())
        {
            return true;
        }
        return false;
    }
    //unique hash for each world pos
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 47;
            hash = hash * 227 + x.GetHashCode();
            hash = hash * 227 + y.GetHashCode();
            hash = hash * 227 + z.GetHashCode();
            return hash;
        }
    }

}
