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

    private void Start()
    {
        plStats = GetComponent<PlayerStats>();
        plMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            useItem();
        }
    }

    void useItem()
    {
        if (heldItem != null)
        {
            heldItem.Effect(plStats);
            itemDuration = heldItem.duration;
        }
    }
}
