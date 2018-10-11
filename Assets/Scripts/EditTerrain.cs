using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditTerrain
{
    //get block position from vector3 by rounding it to nearest block position
    public static WorldPos GetBlockPos(Vector3 pos)
    {
        WorldPos blockPos = new WorldPos(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        return blockPos;
    }

    //get position of block hit by raycast
    //if adjacent is true, gets the block adjacent to the face you hit
    public static WorldPos GetBlockPos(RaycastHit hit, bool adjacent = false)
    {
        Vector3 pos = new Vector3(
            MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
            MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
            MoveWithinBlock(hit.point.z, hit.normal.z, adjacent));
        return GetBlockPos(pos);
    }

    //makes sure we're not between two blocks
    static float MoveWithinBlock(float pos, float norm, bool adjacent = false)
    {
        //pos is only between two blocks if it has a decimal of 0.5
        if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
        {
            if (adjacent)
            {
                pos += (norm / 2);
            }
            else
            {
                pos -= (norm / 2);
            }
        }
        return (float)pos;
    }

    public static bool SetBlock(RaycastHit hit, Block block, bool adjacent = false)
    {
        Chunk chunk = hit.collider.GetComponent<Chunk>();
        if (chunk == null)
        {
            return false;
        }
        WorldPos pos = GetBlockPos(hit, adjacent);
        chunk.world.SetBlock(pos.x, pos.y, pos.z, block);
        //save block
        //Serialization.SaveChunk(chunk);
        return true;
    }
    public static bool SetBlock(Vector3 trans)
    {
      //  Chunk chunk = hit.collider.GetComponent<Chunk>();
       // if (chunk == null)
      //  {
            return false;
      //  }
        WorldPos pos = GetBlockPos(trans);
        
        //chunk.world.SetBlock(pos.x, pos.y, pos.z, block);
        //save block
       // Serialization.SaveChunk(chunk);
        return true;
    }
    public static Block GetBlock(RaycastHit hit, bool adjacent = false)
    {
        Chunk chunk = hit.collider.GetComponent<Chunk>();
        if (chunk == null)
        {
            return null;
        }
        WorldPos pos = GetBlockPos(hit, adjacent);

        Block block = chunk.world.GetBlock(pos.x, pos.y, pos.z);
        return block;
    }


}
