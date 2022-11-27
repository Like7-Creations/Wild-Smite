using UnityEngine;

[CreateAssetMenu(fileName = "New Stat File", menuName = "Stat/New Player Stat or Enemy Stat")]
public class CharacterStats : ScriptableObject
{
    [TextArea(5, 20)] public new string name;
    [TextArea(10, 20)] public string description;
    #region Values And Bools

    #region Current Values
    [Header("[Current Values]")]
    [Space(5)]
  
    public float currentHealth;

    public float currentStamina;
    
    public int currentMelee;
   
    public int currentRanged;

    #endregion


    #region Player Stats
    [Header("[Player Stats]")]
    [Space(5)]
  
    public float PlayerHealth = 100;
 
    public float PlayerStamina = 100;
  
    public int PlayerMeleeAtk = 10;
    
    public int PlayerRangedAtk = 10;
   
    //public float PlayerAoeDamage = 20;
   
    //public float PlayerAoeRanged = 20;
   
    public float  PlayerChargeAttack = 0;

    #endregion

    #region Max Values
    [Header("[Max Values]")]
    [Space(5)]

    public float maxChargeAttackMultiplier = 20;

    public float maxPlayerChargeAttackTime = 20;
  

    #endregion

    #region Increase Values
    [Header("[+Increase Values]")]
    [Space(5)]
    [Range(0, 100)]
    public float staminaUpRate;
    [Range(0, 100)]
    public float healthUpRate;
    [Range(0, 100)]
    public float attackMultiplierUpRate;
    #endregion

    #region Decrease Values
    [Header("[-Decrease Values]")]
    [Space(5)]
    [Range(0, -100)]
    public float healthDownRate;
    [Range(0, -100)]
    public float staminaDownRate;
    [Range(0, 100)]
    public float attackMultiplierDownRate;

    #endregion

    #region Bools
    public bool death;
    #endregion

    #endregion

}