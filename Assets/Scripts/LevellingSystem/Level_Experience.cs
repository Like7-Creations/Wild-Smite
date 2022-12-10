using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level File", menuName = "Stat/LevelStat")]
public class Level_Experience : ScriptableObject
{
    CharacterStats cS;

    #region Current Level and Experience Values
    [Header("[Level and Experience]")]
    [Space(5)]

   
    public int Level;
    public float currentLevel;

    public float currentXP;
    public float requiredXP;


    #endregion

    


}
