using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public CharacterStats stat;
    PlayerController pC;

    [SerializeField] bool beginDelay;
    float delay;


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
        pC = GetComponent<PlayerController>();
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
        if (stat.currentStamina > 0 && Input.GetKey("left shift") & pC.refer != Vector3.zero)
        {
            if (!beginDelay)
            {
                stat.currentStamina += stat.staminaDownRate * Time.deltaTime;
                Debug.Log("IM SPEEEDINGNGGNGGNG");
                GetComponent<Animator>().speed = 3;
            }
        }
        else if (stat.currentStamina <= 100 || pC.refer != Vector3.zero && !beginDelay)
        {
                Debug.Log("regeneration");
                GetComponent<Animator>().speed = 1;
                stat.currentStamina += stat.staminaUpRate * Time.deltaTime;
        }
        if(stat.currentStamina <= 0.5f)
        {
            beginDelay = true;
        }

        if(beginDelay)
        {
            delay += Time.deltaTime;
            if(delay >= 1)
            {
                beginDelay = false;
                delay = 0;
            }
        }
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
