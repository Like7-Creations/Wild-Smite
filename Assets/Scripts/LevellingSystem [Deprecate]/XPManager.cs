using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class XPManager : MonoBehaviour
{
    public CharacterStats cS;
    public TextMeshProUGUI currentXpText, requiredXpText, levelText, currentPlayerStatPointText;
    public float currentXp, requiredXp, level;
    public GameObject statPointPanel;

    public bool statHealth;


    DisplayStats stat;

    private void Awake()
    {
        if(cS.PlayerLevel == 1)
        {
            cS.PlayerRequiredXp = 100;
            cS.PlayerXP = 0;
            cS.PlayerStatPoint = 0;
            cS.PlayerHealth = 100;
            cS.PlayerStamina = 100;
            cS.PlayerMeleeAtk = 10;
            cS.PlayerRangedAtk = 10;
        }

        statHealth = false;
    }

    private void Start()
    {
        stat = GetComponent<DisplayStats>();
        currentXpText.text = currentXp.ToString("0");

        requiredXp = cS.PlayerRequiredXp;
        requiredXpText.text = requiredXp.ToString("0");

        stat.currentPlayerStatPoint = cS.PlayerStatPoint;
        currentPlayerStatPointText.text = stat.currentPlayerStatPoint.ToString("0");

        levelText.text = cS.PlayerLevel.ToString("0");

        level = cS.PlayerLevel;
        currentXp = cS.PlayerXP;

        statPointPanel.SetActive(false);

    }

    private void Update()
    {
        cS.PlayerHealth = stat.currentHealth;
        cS.PlayerStamina = stat.currentStamina;
        cS.PlayerMeleeAtk = stat.meleeAtk;
        cS.PlayerRangedAtk = stat.rangedAtk;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
    }

    private void FixedUpdate()
    {
        if (cS.PlayerStatPoint > 0)
        {
            statPointPanel.SetActive(true);
        }
        else
        {
            statPointPanel.SetActive(false);
        }
    }

    public void AddXp(int xp)
    {
        currentXp += xp;

        while(currentXp >= requiredXp) 
        {
            currentXp = currentXp - requiredXp;
            cS.PlayerLevel++;


            //increase player stat point
            stat.currentPlayerStatPoint += 5;
            cS.PlayerStatPoint = stat.currentPlayerStatPoint;

            //Formula Round to Nearest 10th
            requiredXp = (requiredXp + 1) * cS.xpMultiplier;
            requiredXp = requiredXp / 10;
            requiredXp = Mathf.Round(requiredXp);
            requiredXp = requiredXp * 10;

            stat.currentHealth += 10;
            stat.currentStamina += 10;
            stat.meleeAtk += 4;
            stat.rangedAtk  += 4;

            

            cS.PlayerRequiredXp = requiredXp;

            stat.health.text = stat.currentHealth.ToString("0");
            stat.stamina.text = stat.currentStamina.ToString("0");

            levelText.text = cS.PlayerLevel.ToString("0");
            requiredXpText.text = requiredXp.ToString("0");

            currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");

        }

        cS.PlayerXP = currentXp;
        currentXpText.text = currentXp.ToString("0");
    }


    public void InceaseStatHealth()
    {
        Debug.Log("dog");
        stat.currentPlayerStatPoint -= 1;
        stat.currentHealth += cS.healthStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


    public void InceaseStatStamina()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.currentStamina += cS.staminaStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


    public void InceaseStatMelee()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.meleeAtk += cS.meleeAtkStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        cS.PlayerMeleeAtk = stat.meleeAtk;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


    public void InceaseStatRanged()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.rangedAtk += cS.rangedAtkStatPointInc;
        cS.PlayerRangedAtk = stat.rangedAtk;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


}
