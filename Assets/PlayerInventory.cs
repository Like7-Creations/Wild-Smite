using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    PlayerStats plStats;
    PlayerMovement plMovement;
    public Item heldItem;

    public GameObject rangeBuffUI;
    public GameObject meleeBuffUI;
    public GameObject speedBuffUI;

    public int healPacks;
    public float itemDuration;

    bool isHappening;

    public float timer;

    private void Start()
    {
        plStats = GetComponent<PlayerStats>();
        plMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if(heldItem!= null)
        {
            if(heldItem.timer <= 0)
            {
                heldItem = null;
            }
        }
    }

    public void useItem()
    {
        if (heldItem != null)
        {
            heldItem.Effect(plStats);
            itemDuration = heldItem.duration;
            isHappening = true;
        }
    }
}
