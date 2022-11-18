using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    PlayerUI playerUI;

    void Start()
    {
        playerUI = GetComponent<PlayerUI>();

        maxHealth = 100;
        currHealth = maxHealth;

        maxStamina = 100;
        currStamina = maxStamina;

        meleeAtk = 10;
        rangedAtk = 10;

        SetStats();

    }

    void SetStats()
    {
        playerUI.healthAmount.text = currHealth.ToString();
        playerUI.staminaAmount.text = currStamina.ToString("0");
    }

    public override void Die()
    {
        Debug.Log("You Died!");
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        SetStats();
    }

    public override void CheckStamina()
    {
        base.CheckStamina();
        SetStats();
    }
}
