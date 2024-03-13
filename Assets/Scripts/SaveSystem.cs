using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerStats playerStats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dr";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        
        PlayerData data = new PlayerData(playerStats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void ErasePlayer()
    {
        string path = Application.persistentDataPath + "/player.dr";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
        
            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.Log("Error: SaveFile Not Found");
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dr";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data;
            try
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            catch (System.Exception)
            {
                Debug.Log("Error Reading File");
                return null;
                throw;
            }
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Error: SaveFile Not Found");
            return null;
        }
    }
}
