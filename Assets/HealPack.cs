using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : Item
{
    public int healthToAdd;

    PlayerStats plStats;
    // Playerstats player;

    public override void Effect(PlayerStats stats)
    {
        plStats.hp += healthToAdd;
        plStats.GetComponent<PlayerInventory>().heldItem= null;

        Play_UseItemSFX();
    }

    public override void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    { 

        if (other.GetComponent<PlayerInventory>() != null)
        {
            PlayerInventory pl = other.GetComponent<PlayerInventory>();
            if (pl.heldItem == null)
            {
                pl.heldItem = this;

                Play_CollectItemSFX();

                plStats = pl.GetComponent<PlayerStats>();
                transform.parent = other.transform;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }
    }
}
