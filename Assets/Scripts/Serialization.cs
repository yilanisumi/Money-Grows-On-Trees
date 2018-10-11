using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Serialization
{
    //folder we save to 
    public static string saveFolderName = "voxelGameSaves";

    public static string SaveLocation(string worldName)
    {
        string saveLocation = saveFolderName + "/" + worldName + "/";
        //create directory if doesn't exist
        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }
        return saveLocation;
    }

    public static string FileName(WorldPos chunkLocation)
    {
        //adds .bin because it's a binary file
        string fileName = chunkLocation.x + "," + chunkLocation.y + "," + chunkLocation.z + ".bin";
        return fileName;
    }

    public static void SaveChunk(Chunk chunk)
    {
        Save save = new Save(chunk);
        if (save.blocks.Count == 0)
        {
            return;
        }
        //create file path
        string saveFile = SaveLocation(chunk.world.worldName);
        saveFile += FileName(chunk.pos);

        //serialize blocks array
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static bool Load(Chunk chunk)
    {
        //get file path
        string saveFile = SaveLocation(chunk.world.worldName);
        saveFile += FileName(chunk.pos);
        //check if file exists
        if (!File.Exists(saveFile))
        {
            return false;
        }

        //deserialize blocks array
        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFile, FileMode.Open);
        Save save = (Save)formatter.Deserialize(stream);
        foreach (var block in save.blocks)
        {
            chunk.blocks[block.Key.x, block.Key.y, block.Key.z] = block.Value;
        }
        stream.Close();
        return true;
    }
}
