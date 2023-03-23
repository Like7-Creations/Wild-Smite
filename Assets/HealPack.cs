using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : Item
{
    public int healthToAdd;
    // Playerstats player;

    public override void Effect(PlayerStats stats)
    {
        GetComponent<PlayerStats>().hp += healthToAdd;
    }

    public override void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.GetComponent<PlayerStats>() != null)
        {
            PlayerStats pl = other.gameObject.GetComponent<PlayerStats>();
            pl.hp += healthToAdd;
        }
    }
}
