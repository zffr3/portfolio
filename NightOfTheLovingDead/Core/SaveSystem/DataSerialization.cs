using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataSerialization 
{
    public static void SerializeSave(PlayerSave save)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs= new FileStream(Path.Combine(Directory.GetCurrentDirectory(), @"\Saves\", $"{System.DateTime.Now.Day}-{System.DateTime.Now.Month}-{System.DateTime.Now.Year}-{System.DateTime.Now.Hour}-{System.DateTime.Now.Minute}.noldsave"), FileMode.CreateNew))
        {
            formatter.Serialize(fs, save);
        }
    }

    public static PlayerSave DeserializeSave(string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        PlayerSave save = null;
        using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), fileName), FileMode.OpenOrCreate))
        {
            save = (PlayerSave)formatter.Deserialize(fs);
        }

        return save;
    }
}
