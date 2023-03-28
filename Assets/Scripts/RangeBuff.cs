using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeBuff : Item
{
    PlayerStats plStats;


    public override void Effect(PlayerStats stats)
    {
        originalAmount = plStats.r_ATK;
        plStats.r_ATK += buffAmount;
        //timer = duration;
        //itemUI.GetComponentInChildren<Image>().fillAmount = 1;
        useItem = true;
    }

    public override void Update()
    {
        if (useItem)
        {
           // itemUI.SetActive(true);
            float thisint = timer / duration;
            //itemUI.GetComponentInChildren<Image>().fillAmount = thisint;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
            //    itemUI.SetActive(false);
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
                plStats = pl.GetComponent<PlayerStats>();
                transform.parent = other.transform;
                timer = duration;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }
    }

}
