using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Profile Info")]
    public long lastUpdatedStamp;

    public List<PlayerConfig> pConfigs;

    public List<PlayerGameData> playerData;

    //Values set by this constructor serve as default values that will be loaded if there is no save data. 
    //Normally these would be hard coded, but we have these values set in the PlayerStats_Data scriptable object, we can pull from there instead.
    public GameData()
    {
        pConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();

        playerData = new List<PlayerGameData>();

        for (int i = 0; i < pConfigs.Count; i++)
        {
            if (pConfigs[i].playerStats != null)
            {
                Debug.Log($"Currently Checking {pConfigs[i].Name} with an index of {pConfigs[i].PlayerIndex}.");
                playerData.Add(new PlayerGameData(pConfigs[i].playerStats, pConfigs[i].Name, pConfigs[i].PlayerIndex));
            }
        }
    }

}

[System.Serializable]
public class PlayerGameData
{
    public PlayerStat_Data pData;

    #region Player Info
    public string name = "";
    public int pIndex;

    #endregion

    #region Player_Stat Variables
    public int hp;
    public int stamina;

    public int m_ATK;
    public int r_ATK;
    #endregion

    #region XP & Leveling Variables
    public int exp;
    public int level;
    #endregion  

    public PlayerGameData(PlayerStat_Data data, string pName, int index)
    {
        //Saving Basic Info
        pData = data;

        name = pName;
        pIndex = index;
        //Saving Basic Info


        //Saving Basic Stats
        hp = data.hp;
        stamina = data.stamina;

        m_ATK = data.m_ATK;
        r_ATK = data.r_ATK;
        //Saving Basic Stats


        //Saving XP & Lvl
        exp = data.current_XP;
        level = data.lvl;
        //Saving XP & Lvl

    }
}
