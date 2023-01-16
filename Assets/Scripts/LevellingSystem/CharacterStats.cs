using UnityEngine;

[CreateAssetMenu(fileName = "New Stat File", menuName = "Stat/New Player Stat or Enemy Stat")]
public class CharacterStats : ScriptableObject
{
    
    [TextArea(5, 20)] public new string name;
    [TextArea(10, 20)] public string description;
    #region Values And Bools

    #region Player Stats
    [Header("[Player Stats]")]
    [Space(5)]
  
    public float PlayerHealth = 100;
 
    public float PlayerStamina = 100;
  
    public int PlayerMeleeAtk = 10;
    
    public int PlayerRangedAtk = 10;
   
    public float  PlayerChargeAttack = 0;

    public float PlayerLevel = 1;

    public float PlayerXP;

    public float PlayerRequiredXp;

    public float PlayerStatPoint;


    #endregion
    #region Stat Point Variables - Increase
    [Header("[Stat Point Variables - Increase]")]
    [Space(5)]

    public float healthStatPointInc;

    public float staminaStatPointInc;

    public int meleeAtkStatPointInc;

    public int rangedAtkStatPointInc;


    #endregion
    #region Stat Point Consumption Variables
    [Header("[Stat Point Consumption Variables]")]
    [Space(5)]

    public float healthStatPointConsumption;

    public float staminaStatPointConsumption;

    public int meleeAtkStatPointConsumption;

    public int rangedAtkStatPointConsumption;


    #endregion

    #region AoE Variables
    [Header("[AoE Variables]")]
    [Space(5)]

    public float maxChargeAttackMultiplier = 20;

    public float maxPlayerChargeAttackTime = 20;

    #endregion

    #region Recovery Rates
    [Header("[Recovery Rates]")]
    [Space(5)]
    [Range(0, 100)]
    public float staminaUpRate;
    #endregion

    #region Multiplier
    [Header("[Multipliers]")]
    [Space(5)]

    public float xpMultiplier = 1.25f;

    #endregion

    #region Stamina Consumption
    [Header("[Stamina Consumption]")]
    [Space(5)]
    [Range(0, 100)]
    public float sprintStamina;
    [Range(0, 100)]
    public float dashStamina;
    [Range(0, 100)]
    public float chargeStamina;

    #endregion

    #region Bools
    public bool death;
    #endregion

    #endregion

}