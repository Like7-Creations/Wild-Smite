using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int currHealth;
    public int maxHealth;

    public int currStamina;
    public int maxStamina;

    public int meleeAtk;
    public int rangedAtk;

    bool isDead = false;

    public virtual void CheckHealth()
    {
        if(currHealth >=maxHealth) 
        {
            currHealth = maxHealth;
        }
        if(currHealth <= 0) 
        {
            currHealth = 0;
            isDead= true;   
        }
    }

    public virtual void CheckStamina()
    {
        if (currStamina >= maxStamina)
        {
            currStamina = maxStamina;
        }
        if (currStamina <= 0)
        {
            currStamina = 0;
        }
    }

    public virtual void Die()
    {
        //override!
    }
}
