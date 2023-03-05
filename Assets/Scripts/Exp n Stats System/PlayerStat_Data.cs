using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/PlayerStat_Data", order = 1)]
public class PlayerStat_Data : ScriptableObject
{
    [SerializeField] ExperienceData expData;
    [SerializeField] Leveling_Data lvlData;

    [SerializeField] string playerName;

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

    public PlayerStat_Data(string name, ExperienceData xpData, Leveling_Data lvlingData)
    {
        playerName = name;
        expData = xpData;
        lvlData = lvlingData;

        hp = lvlData.base_HP;
        stamina = lvlData.base_STAM;
        m_ATK = lvlData.base_MATK;
        r_ATK = lvlData.base_RATK;
    }

    //Increase player level based on Leveling Data class.
    void LevelUp()
    {
        hp += lvlData.HP_Increment;
        stamina += lvlData.STAM_Increment;
        m_ATK += lvlData.M_ATK_Increment;
        r_ATK += lvlData.R_ATK_Increment;
    }

    //Apply gained experience to the player's current level.
    void XPGained(int gainedXP)
    {
        //Check if the current XP has hit a certain milestone from Experience Data class.
        int currentLvl = lvl;
        int possibleLvl = expData.CheckLevel(gainedXP);

        if(possibleLvl > currentLvl)
        {
            for (int i = 0; i > possibleLvl - currentLvl; i++)
            {
                LevelUp();
            }

            lvl = possibleLvl;
        }
        //If true, run the LevelUp() function.
    }

    //Increase Stats based on the stat points allocated by the player
    void AllocateStatPoints(int[] pointAllocations)
    {
        //Repeat for all stats
        hp += pointAllocations[0] * lvlData.hp_Conversion;
        stamina += pointAllocations[0] * lvlData.hp_Conversion;

        m_ATK += pointAllocations[0] * lvlData.hp_Conversion;
        r_ATK += pointAllocations[0] * lvlData.hp_Conversion;
    }
}