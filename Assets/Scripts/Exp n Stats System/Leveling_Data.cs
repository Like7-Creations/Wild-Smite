using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/Leveling_Data", order = 1)]
public class Leveling_Data : ScriptableObject
{
    //Increase stat values by these amounts everytime the player levels up.
    [Header("Stat Increase")]
    public int newHP;
    public int newSTAM;     
    public int newM_ATK;
    public int newR_ATK;

    [Header("Stat Point Reward")]
    public int pointsPerLvl;

    //Multiply allocated points to a stat by a specific value when adding to said stat
    [Header("Points Conversion")]
    public int hp_Conversion;
    public int stamina_Conversion;
    public int mATK_Conversion;
    public int rATK_Conversion;
}
