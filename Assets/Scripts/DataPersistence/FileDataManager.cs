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

    public GameData Load(string profileID)
    {
        // Base Case - If profileID is null, return right away.
        if(profileID== null)
        {
            return null;
        }

        //Use Path.Combine to account for different OS's having different  path deparators.
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);

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

    public void Save(GameData data, string profileID)
    {
        // Base Case - If profileID is null, return right away.
        if (profileID == null)
        {
            return;
        }

        //Use Path.Combine to account for different OS's having different  path deparators.
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);

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

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            //check if save data exists at the current directory.
            //If it doesn't, this folder isn't a relevant profile and should be ignored.
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning($"No Relevant Data '{profileId}' Found. Skipping Directory.");
                continue;
            }

            //Load game data for this profile and store it in the dictionary.
            GameData profileData = Load(profileId);

            //Ensure the profile isn't null.
            //If it is, send out a warning.
            if(profileData!= null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load data. But Something Went Wrong at Profile: " + profileId);
            }

        }


        return profileDictionary;
    }

    public string GetRecentlyUpdatedProfileID()
    {
        string mostRecentProfileID = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();

        foreach(KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileID = pair.Key;
            GameData data = pair.Value;

            //Skip entry if gamedata is null.
            if(data == null)
            {
                continue;
            }
            
            //If this is the first data we've come across that exists, its the most recent so far.
            if(mostRecentProfileID== null)
            {
                mostRecentProfileID = profileID;
            }
            //Otherwise, compare to see which date is the most recent
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileID].lastUpdatedStamp);
                DateTime newDateTime = DateTime.FromBinary(data.lastUpdatedStamp);

                //The greatest DateTime value is the most recent
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileID = profileID;
                }
            }
        }

        return mostRecentProfileID;
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
