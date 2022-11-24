using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public CharacterStats stat;
    PlayerController pC;


    #region Text Holders
    [Header("[Health & Stamina Text Holders]")]
    [Space(5)]
    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;

    public int meleeAtk;
    public int rangedAtk;
    #endregion

    public void Start()
    {
        #region Stats Equal Max-Values

        stat.currentHealth = stat.maxHealth;
        stat.currentStamina = stat.maxStamina;
        meleeAtk = stat.currentMelee;
        rangedAtk = stat.currentRanged;

        #endregion

        #region Bool Equals-start

        stat.death = false;

        #endregion

        #region Bar Max Value-start

        health.text = stat.currentHealth.ToString("0");
        stamina.text = stat.currentStamina.ToString("0");

        #endregion
        UpdateUI();

    }

    public void Update()
    {
        #region If Running
        if (Input.GetKey("left shift") && Input.GetKey("w")|| (Input.GetKey("left shift") && Input.GetKey("a") || (Input.GetKey("left shift") && Input.GetKey("s") || (Input.GetKey("left shift") && Input.GetKey("d")))))
        {
            stat.currentStamina += stat.staminaDownRate * Time.deltaTime;
            GetComponent<Animator>().speed = 3;
        }
        else if (Input.GetKey("w") || (Input.GetKey("a") || (Input.GetKey("s") || (Input.GetKey("d")))))
        {
            stat.currentStamina += stat.staminaUpRate * Time.deltaTime;
            GetComponent<Animator>().speed = 1;
        }

        //if(stamina)
        #endregion


        #region If Health Is Less Or Equal To 0 
        if (stat.currentHealth <= 0)
        {
            Die();
        }
        #endregion


        UpdateUI();

    }

    private void UpdateUI()
    {

        #region Math Clamps
        stat.currentHealth = (int)Mathf.Clamp(stat.currentHealth, 0, 100f);
        stat.currentStamina = Mathf.Clamp(stat.currentStamina, 0, 100f);
        #endregion

        #region Health & Stamina Values

        health.text = stat.currentHealth.ToString("0");
        stamina.text = stat.currentStamina.ToString("0");

        #endregion
    }

    public void GetDamaged(int amount)
    {
        stat.currentHealth -= amount;
        UpdateUI();
    }


    public void Die()
    {
        stat.death = true;
        Debug.Log("you have died");
    }
}
