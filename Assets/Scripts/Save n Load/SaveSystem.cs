using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayerData(PlayerStat_Data player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/playerSave.sux";

        FileStream stream = new FileStream(filePath, FileMode.Create);

        Player_SaveData data = new Player_SaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Player_SaveData LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/playerSave.sux";

        if(File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            Player_SaveData data = formatter.Deserialize(stream) as Player_SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save not found");
            return null;
        }
    }
}
