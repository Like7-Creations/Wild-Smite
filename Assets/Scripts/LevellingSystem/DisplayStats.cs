using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DisplayStats : MonoBehaviour
{
    public CharacterStats stat;
    PlayerController pC;

    [SerializeField] bool beginDelay;
    float delay;
    public float staminaFromChargeAttack;
    [SerializeField] bool stillCharging;

    [SerializeField] bool reachedMax;




    #region Text Holders
    [Header("[Health & Stamina Text Holders]")]
    [Space(5)]

    public float currentHealth;
    public float currentStamina;


    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI chargeAttack;

    public float currentAttackMultiplier;

    public float currentChargeAttackCount;
    public int meleeAtk;
    public int rangedAtk;


    public GameObject releasePanel;



    #endregion
    private void Awake()
    {
        currentChargeAttackCount = stat.PlayerChargeAttack;
        currentHealth = stat.PlayerHealth;
        currentStamina = stat.PlayerStamina;
        meleeAtk = stat.PlayerMeleeAtk;
        rangedAtk = stat.PlayerRangedAtk;

    }
    public void Start()
    {
        #region Stats Equal Max-Values
        pC = GetComponent<PlayerController>();


        reachedMax = false;
        stat.attackMultiplierUpRate = 2;
        currentChargeAttackCount = 0;   
        currentStamina = 100;
        currentHealth = 100;
        releasePanel.gameObject.SetActive(false);


        #endregion

        #region Bool Equals-start

        stat.death = false;

        #endregion

        #region Bar Max Value-start

        health.text = currentHealth.ToString("0");
        stamina.text = currentHealth.ToString("0");
        chargeAttack.text = currentChargeAttackCount.ToString("0");

        #endregion
        UpdateUI();

    }

    public void Update()
    {
        #region If Running
        if (currentStamina > 0 && Input.GetKey("left shift") & pC.refer != Vector3.zero)
        {
            if (!beginDelay)
            {
                currentStamina += stat.staminaDownRate * Time.deltaTime;
                //SDebug.Log("IM SPEEEDINGNGGNGGNG");
                GetComponent<Animator>().speed = 3;
            }
        }
        else if (currentStamina <= 100 || pC.refer != Vector3.zero && !beginDelay)
        {
                //Debug.Log("regeneration");
                GetComponent<Animator>().speed = 1;
                currentStamina += stat.staminaUpRate * Time.deltaTime;
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
        if (currentHealth <= 0)
        {
            Die();
        }
        #endregion

        #region AoE Calculations and charging

        if (Input.GetKey("p") && currentStamina >= 50) //check condition
        {
            Debug.Log("increase chargeatk variable");
            stillCharging = true;

            currentChargeAttackCount += stat.attackMultiplierUpRate * Time.deltaTime;
            currentAttackMultiplier += stat.attackMultiplierUpRate * Time.deltaTime;
            

            if ((int)currentChargeAttackCount >= (int)stat.maxPlayerChargeAttackTime)
            {
                reachedMax = true;
                releasePanel.gameObject.SetActive(true);
                stat.attackMultiplierUpRate = 0;
            }
        }
        else if(Input.GetKeyUp("p") && currentStamina >= 50)
        {
            //play AOE Animaton
            stillCharging = false;
            currentStamina -= staminaFromChargeAttack;
            releasePanel.gameObject.SetActive(false);

            //m = t / T * M
            currentAttackMultiplier = currentChargeAttackCount / stat.maxPlayerChargeAttackTime * stat.maxChargeAttackMultiplier;
            meleeAtk = stat.PlayerMeleeAtk + (stat.PlayerMeleeAtk * (int)currentAttackMultiplier);
            rangedAtk = stat.PlayerRangedAtk + (stat.PlayerRangedAtk * (int)currentAttackMultiplier);
            pC.CheckForEnemies();
       
            stat.attackMultiplierUpRate = 2;
            currentChargeAttackCount = 0;

        }
        meleeAtk = stat.PlayerMeleeAtk;
        rangedAtk = stat.PlayerRangedAtk;
        reachedMax = false;




        #endregion
        UpdateUI();

    }

    private void UpdateUI()
    {

        #region Math Clamps
        currentHealth = Mathf.Clamp(currentHealth, 0, 100f);
        currentStamina = Mathf.Clamp(currentStamina, 0, 100f);
        currentChargeAttackCount = Mathf.Clamp(currentChargeAttackCount, 0, 100f);
        #endregion

        #region Health & Stamina Values

        health.text = currentHealth.ToString("0");
        stamina.text = currentStamina.ToString("0");
        chargeAttack.text= currentChargeAttackCount.ToString("0");

        #endregion
    }

    public void GetDamaged(int amount)
    {
        currentHealth -= amount;
        UpdateUI();
    }


    public void Die()
    {
        stat.death = true;
        Debug.Log("you have died");
    }
}
