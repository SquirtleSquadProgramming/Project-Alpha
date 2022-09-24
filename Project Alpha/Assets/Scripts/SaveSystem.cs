using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "/player.dat";

    public static void SaveData()
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(fs, PlayerData.SerialisedPlayer);

        fs.Close();
    }

    public static SerialisablePlayerData LoadData()
    {
        if (!File.Exists(path))
            return null;

        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        PlayerData.SerialisedPlayer = (SerialisablePlayerData)formatter.Deserialize(fs);

        fs.Close();

        return PlayerData.SerialisedPlayer;
    }
}
