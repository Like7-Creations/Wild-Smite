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
    [Range(0, 100)]
    public int currentHealth;
    [Range(0, 100)]
    public float currentStamina;
    [Range(0, 100)]
    public int currentMelee;
    [Range(0, 100)]
    public int currentRanged;

    #endregion

    #region Maximum Values
    [Header("[Maximum Values]")]
    [Space(5)]
    [Range(0, 100)]
    public int maxHealth;
    [Range(0, 100)]
    public float maxStamina;
    [Range(0, 100)]
    public int maxMelee;
    [Range(0, 100)]
    public int maxRanged;

    #endregion

    #region Increase Values
    [Header("[+Increase Values]")]
    [Space(5)]
    [Range(0, 100)]
    public float staminaUpRate;
    [Range(0, 100)]
    public int healthUpRate;
    #endregion

    #region Decrease Values
    [Header("[-Decrease Values]")]
    [Space(5)]
    [Range(0, -100)]
    public int healthDownRate;
    [Range(0, -100)]
    public float staminaDownRate;

    #endregion

    #region Bools
    public bool death;
    #endregion

    #endregion

}