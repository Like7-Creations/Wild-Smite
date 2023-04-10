using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/PlayerStat_Data", order = 1)]
[System.Serializable]
public class PlayerStat_Data : ScriptableObject, IDataPersistence
{
    [SerializeField] ExperienceData expData;
    public Leveling_Data lvlData;

    public PlayerConfig config;
    public string playerName;
    public int playerIndex;
    public string selectedCharacter;


    //[Header("Stats")]
    [field: SerializeField]
    public int hp { get; private set; }

    [field: SerializeField]
    public int stamina { get; private set; }

    [field: SerializeField]
    public int m_ATK { get; private set; }

    [field: SerializeField]
    public int r_ATK { get; private set; }

    [field: SerializeField]
    public int current_XP { get; private set; }

    [field: SerializeField]
    public int lvl { get; private set; }

    [field: SerializeField]
    public int Stat_Points { get; private set; }

    //[Header("Recovery Rates")]

    [field: SerializeField]
    public float recovRate_HP { get; private set; }

    [field: SerializeField]
    public float recovRate_STAMINA { get; private set; }

    //[Header("Consumption Rates")]


    [field: SerializeField]
    public float dash { get; private set; }             //Fixed amount

    [field: SerializeField]
    public float sprint { get; private set; }           //Amount per second

    [field: SerializeField]
    public float aoe_TAP { get; private set; }          //Fixed amount

    [field: SerializeField]
    public float aoe_HOLD { get; private set; }         //Max Charge Amount

    [field: SerializeField]
    public float aoe_ChargeRate { get; private set; }   //Amount per second

    public void init(string name, int index, ExperienceData xpData, Leveling_Data lvlingData, PlayerConfig c)
    {
        playerName = name;
        playerIndex = index;
        expData = xpData;
        lvlData = lvlingData;
        config = c;

        hp = lvlData.base_HP;
        stamina = lvlData.base_STAM;
        m_ATK = lvlData.base_MATK;
        r_ATK = lvlData.base_RATK;

        recovRate_HP = 5;
        recovRate_STAMINA = 5;

        sprint = 1;
        dash = 10;

        aoe_ChargeRate = 1;
        aoe_TAP = 10;
        aoe_HOLD = 1;
    }

    #region Save & Load Functions [Trevor Mock Edition]

    public void LoadData(GameData data)
    {
        hp = data.hp;
        stamina = data.stamina;

        m_ATK = data.m_ATK;
        r_ATK = data.r_ATK;

        current_XP = data.exp;
        lvl = data.level;
    }

    public void SaveData(ref GameData data)
    {
        data.hp = hp;
        data.stamina = stamina;

        data.m_ATK= m_ATK;
        data.r_ATK= r_ATK;

        data.level = lvl;
        data.exp = current_XP;
    }

    #endregion 

    #region Save & Load Functions [Brackey's Edition] (Deprecate Later)
    public void SaveData()
    {
        //SaveSystem.SavePlayerData(this);
    }

    public void LoadData()
    {
        //Player_SaveData pData = SaveSystem.LoadPlayerData();

        //hp = pData.hp;
        //stamina = pData.stamina;

        //m_ATK = pData.m_ATK;
        //r_ATK = pData.r_ATK;

        //current_XP = pData.exp;
        //lvl = pData.level;
    }
    #endregion

    //Increase player level based on Leveling Data class.
    void LevelUp()
    {
        //hp += lvlData.HP_Increment;
        //stamina += lvlData.STAM_Increment;
        //m_ATK += lvlData.M_ATK_Increment;
        //r_ATK += lvlData.R_ATK_Increment;

        Debug.Log("Adding StatPoints to Player");
        Stat_Points += lvlData.pointsPerLvl;
    }

    //Apply gained experience to the player's current level.
    public void XPGained(int gainedXP)
    {
        //Check if the current XP has hit a certain milestone from Experience Data class.
        int currentLvl = lvl;
        Debug.Log("Current Level is: " + currentLvl);
        int possibleLvl = expData.CheckLevel(current_XP + gainedXP);
        Debug.Log("Possible Level is: " + possibleLvl);

        int lvlIncreases = possibleLvl - currentLvl;
        Debug.Log("Level Increased by: " + lvlIncreases);

        for (int i = 0; i < lvlIncreases; i++)
        {
            LevelUp();
        }

        if (lvl != possibleLvl)
            lvl = possibleLvl;
        current_XP += gainedXP;
        //If true, run the LevelUp() function.
    }

    //Increase Stats based on the stat points allocated by the player
    public void AllocateStatPoints(int[] pointAllocations)
    {
        Stat_Points = 0;
        //Repeat for all stats
        hp += pointAllocations[0];
        stamina += pointAllocations[1];
        m_ATK += pointAllocations[2];
        r_ATK += pointAllocations[3];
    }

    public void LoadStats(int hp, int stamina, int melee, int range, int index, int currentXP, int level)
    {
        Debug.Log($"Loading data for {playerName}");

        this.hp = hp;
        this.stamina =stamina;

        m_ATK = melee;
        r_ATK = range;

        playerIndex = index;

        current_XP = currentXP;
        lvl = level;
    }

    public void SaveStats(PlayerStat_Data data)
    {
        Debug.Log("Player Index matches");

        data.playerIndex = playerIndex;

        data.hp = hp;
        data.stamina = stamina;

        data.m_ATK = m_ATK;
        data.r_ATK = r_ATK;

        data.current_XP = current_XP;
        data.lvl = lvl;
    }
}