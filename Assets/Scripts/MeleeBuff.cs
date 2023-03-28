using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MeleeBuff : Item
{
    PlayerStats plStats;
    public Slider itemImage;

    public override void Effect(PlayerStats stats)
    {
        originalAmount = plStats.m_ATK;
        plStats.m_ATK += buffAmount;
        timer = duration;
        //itemUI.GetComponentInChildren<Slider>().fillAmount = 1;
        useItem = true;
    }

    public override void Update()
    {
        if(useItem)
        {
            //itemUI.SetActive(true);
            float thisint = timer / duration;
            //itemUI.GetComponentInChildren<Slider>().fillAmount = thisint;
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
              //  itemUI.SetActive(false);
                plStats.m_ATK = originalAmount;
                useItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInventory>() != null) 
        {
            PlayerInventory pl = other.GetComponent<PlayerInventory>();
            if(pl.heldItem == null)
            {
                pl.heldItem = this;
                plStats = pl.GetComponent<PlayerStats>();
                transform.parent = other.transform;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }        
    }
}
