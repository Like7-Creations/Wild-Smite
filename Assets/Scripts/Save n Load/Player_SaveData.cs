using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_SaveData
{
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

    public Player_SaveData(PlayerStat_Data pStats)
    {
        hp = pStats.hp;
        stamina = pStats.stamina;

        m_ATK = pStats.m_ATK;
        r_ATK = pStats.r_ATK;

        exp = pStats.current_XP;
        level = pStats.lvl;

    }
}
