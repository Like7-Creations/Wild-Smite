using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/PlayerStat_Data", order = 1)]
public class PlayerStat_Data : ScriptableObject
{
    [SerializeField] ExperienceData expData;
    [SerializeField] Leveling_Data lvlData;

    [SerializeField] string playerName;

    [Header("Stats")]
    public int hp;
    public int stamina;
    public int m_ATK;
    public int r_ATK;
    public int current_XP;
    public int lvl;

    [Header("Recovery Rates")]
    [SerializeField] float recovRate_HP;
    [SerializeField] float recovRate_STAMINA;

    [Header("Consumption Rates")]
    [SerializeField] float dash;             //Fixed amount
    [SerializeField] float sprint;           //Amount per second
    [SerializeField] float aoe_TAP;          //Fixed amount
    [SerializeField] float aoe_HOLD;         //Amount per second

    //Increase player level based on Leveling Data class.
    void LevelUp()
    {
        hp = lvlData.newHP;
        stamina = lvlData.newSTAM;
        m_ATK = lvlData.newM_ATK;
        r_ATK = lvlData.newR_ATK;

        lvl++;
    }

    //Apply gained experience to the player's current level.
    void XPGained()
    {
        //Check if the current XP has hit a certain milestone from Experience Data class.
        if (expData.CheckLevel(current_XP) > lvl)
            LevelUp();
        //If true, run the LevelUp() function.
    }

    //Increase Stats based on the stat points allocated by the player
    void AllocateStats()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}