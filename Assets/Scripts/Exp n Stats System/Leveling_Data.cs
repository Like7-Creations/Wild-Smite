using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/Leveling_Data", order = 1)]
public class Leveling_Data : ScriptableObject
{
    //Increase stat values by these amounts everytime the player levels up.
    //[Header("Stat Increase")]


    [field: SerializeField]
    public int HP_Increment { get; private set; }

    [field: SerializeField]
    public int STAM_Increment { get; private set; }

    [field: SerializeField]
    public int M_ATK_Increment { get; private set; }


    [field: SerializeField] 
    public int R_ATK_Increment { get; private set; }

    //[Header("Stat Point Reward")]

    [field: SerializeField]
    public int pointsPerLvl { get; private set; }

    //Multiply allocated points to a stat by a specific value when adding to said stat
    //[Header("Points Conversion")]
    
    [field: SerializeField]
    public int hp_Conversion { get; private set; }

    [field: SerializeField]
    public int stamina_Conversion { get; private set; }
    
    [field: SerializeField]
    public int mATK_Conversion { get; private set; }
    
    [field: SerializeField]
    public int rATK_Conversion { get; private set; }

    //BASE STATS
    [field: SerializeField]
    public int base_HP { get; private set; }

    [field: SerializeField]
    public int base_STAM { get; private set; }
    
    [field: SerializeField]
    public int base_MATK { get; private set; }

    [field: SerializeField]
    public int base_RATK { get; private set; }
}
