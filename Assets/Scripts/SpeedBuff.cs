using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuff : Item
{
    PlayerMovement plStats;

    public override void Effect(PlayerStats stats)
    {
        originalAmount = (int)plStats.playerSpeed;
        plStats.playerSpeed += buffAmount;
        timer = duration;
        itemUI.GetComponentInChildren<Image>().fillAmount = 1;
        useItem = true;
    }

    public override void Update()
    {
        if (useItem)
        {
            itemUI.SetActive(true);
            float thisint = timer / duration;
            itemUI.GetComponentInChildren<Image>().fillAmount = thisint;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                itemUI.SetActive(false);
                plStats.playerSpeed = originalAmount;
                Destroy(gameObject);
                useItem = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            PlayerInventory pl = other.GetComponent<PlayerInventory>();
            if (pl.heldItem == null)
            {
                pl.heldItem = this;
                plStats = pl.GetComponent<PlayerMovement>();
                transform.parent = other.transform;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }
    }
}
