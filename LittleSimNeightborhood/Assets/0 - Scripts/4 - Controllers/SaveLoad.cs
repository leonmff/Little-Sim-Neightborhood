using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    //public static PlayerInfo playerInfo = new PlayerInfo();

    const string SAVE_FILE_NAME = "littlesimneighborhood.game";

    public static void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, "/", SAVE_FILE_NAME)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, "/", SAVE_FILE_NAME), FileMode.Open);
            PlayerInfo t_playerInfo = (PlayerInfo)bf.Deserialize(file);
            file.Close();
            PlayerInfo.instance = t_playerInfo;
        }
        else
        {
            Save();
        }
    }

    public static void Save()
    {
        if (ReferenceEquals(PlayerInfo.instance, null))
            PlayerInfo.instance = new PlayerInfo();

        //playerInfo = PlayerInfo.playerInfo;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, "/", SAVE_FILE_NAME));
        bf.Serialize(file, PlayerInfo.instance);
        file.Close();
    }
}