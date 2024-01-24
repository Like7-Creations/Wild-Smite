using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeBuff : Item
{
    PlayerStats plStats;
    bool used;

    public override void Effect(PlayerStats stats)
    {
        if (!used)  // this if statement code was added 1 hour before submission..thats why its amatuer
        {
            originalAmount = plStats.r_ATK;
            plStats.r_ATK += buffAmount;
            //timer = duration;
            //itemUI.GetComponentInChildren<Image>().fillAmount = 1;
            useItem = true;

            used = true;

            Play_UseItemSFX();
        }
    }

    public override void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (useItem)
        {
            float thisint = timer / duration;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                plStats.r_ATK = originalAmount;
                useItem = false;
            }
        }
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
                timer = duration;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }
    }

}
