using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    //We dont use a reference here because the scripts only care about reading the GameData provided to them.
    void LoadData(GameData data);

    //We use a reference here so that the scripts that call this function can modify the GameData that's provided to them.
    void SaveData(ref GameData data);
}
