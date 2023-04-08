using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Profile Info")]
    public long lastUpdatedStamp;


    PlayerStat_Data pData;

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

    //Values set by this constructor serve as default values that will be loaded if there is no save data. 
    //Normally these would be hard coded, but we have these values set in the PlayerStats_Data scriptable object, we can pull from there instead.
    public GameData()
    {
        hp = pData.lvlData.base_HP;
        stamina = pData.lvlData.base_STAM;

        m_ATK = pData.lvlData.base_MATK;
        r_ATK = pData.lvlData.base_RATK;

        exp = pData.current_XP;     //Or make it zero.
        level = pData.lvl;          //Or make it zero.
    }

}
