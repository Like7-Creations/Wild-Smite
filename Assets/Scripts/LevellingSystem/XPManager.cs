using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public CharacterStats cS;
    public TextMeshProUGUI currentXpText, requiredXpText, levelText;
    public float currentXp, requiredXp, level;
    DisplayStats stat;

    private void Awake()
    {
        if(cS.PlayerLevel == 1)
        {
            cS.PlayerRequiredXp = 100;
            cS.PlayerXP = 0;
        }
    }

    private void Start()
    {
        stat = GetComponent<DisplayStats>();
        currentXpText.text = currentXp.ToString("0");

        requiredXp = cS.PlayerRequiredXp;
        requiredXpText.text = requiredXp.ToString("0");

        levelText.text = cS.PlayerLevel.ToString("0");

        level = cS.PlayerLevel;
        currentXp = cS.PlayerXP;
    }

    public void AddXp(int xp)
    {
        currentXp += xp;

        while(currentXp >= requiredXp) 
        {
            currentXp = currentXp - requiredXp;
            cS.PlayerLevel++;

            //Formula Round to Nearest 10th
            requiredXp = (requiredXp + 1) * cS.xpMultiplier;
            requiredXp = requiredXp / 10;
            requiredXp = Mathf.Round(requiredXp);
            requiredXp = requiredXp * 10;

            stat.currentHealth += 10;
            stat.currentStamina += 10;
            stat.meleeAtk += 4;
            stat.rangedAtk  += 4;

            cS.PlayerHealth = stat.currentHealth;
            cS.PlayerStamina = stat.currentStamina;
            cS.PlayerMeleeAtk = stat.meleeAtk;
            cS.PlayerRangedAtk =  stat.rangedAtk;

            cS.PlayerRequiredXp = requiredXp;

            stat.health.text = stat.currentHealth.ToString("0");
            stat.stamina.text = stat.currentStamina.ToString("0");

            levelText.text = cS.PlayerLevel.ToString("0");
            requiredXpText.text = requiredXp.ToString("0");

        }

        cS.PlayerXP = currentXp;
        currentXpText.text = currentXp.ToString("0");
    }
}
