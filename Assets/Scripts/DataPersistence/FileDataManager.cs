using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Experimental.RestService;
using UnityEngine;

public class FileDataManager
{
    string dataDirPath = "";
    string dataFileName = "";

    bool useEncryption = false;
    readonly string encryptionKey = "Sleep";

    public FileDataManager(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        //Use Path.Combine to account for different OS's having different  path deparators.
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                //Load the serialized data from the file
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Optionally, decrypt the data.
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //Deserialize data from Json into C# GameData obj
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error Occured when loading Data from File: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        //Use Path.Combine to account for different OS's having different  path deparators.
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //Create the directory path in case it doesn't exist.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize C# gamedata obj into json.
            string dataToStore = JsonUtility.ToJson(data, true);

            //Optionally, encrypt the data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //Write the serialized data to the file.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error Occured Trying To Save PlayerData To File: " + fullPath + "\n" + e);
        }
    }

    //A simple implementation of XOR encryption.
    string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
        }
        return modifiedData;
    }
}
