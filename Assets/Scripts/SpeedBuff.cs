using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuff : Item
{
    PlayerMovement plMovement;
    PlayerActions pActions;

    float originalSprintSpeed;
    float originalSpeed;

    public override void Effect(PlayerStats stats)
    {
        originalSpeed = pActions.OriginalSpeed;
        originalSprintSpeed = pActions.SprintSpeed;
        plMovement.playerSpeed += buffAmount;
        pActions.SprintSpeed = plMovement.playerSpeed;
        pActions.OriginalSpeed = plMovement.playerSpeed;
        originalAmount = (int)plMovement.playerSpeed;
        //plStats.ori
        timer = duration;
        //temUI.GetComponentInChildren<Image>().fillAmount = 1;
        useItem = true;

        Play_UseItemSFX();
    }

    public override void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (useItem)
        {
            // itemUI.SetActive(true);
            float thisint = timer / duration;
            // itemUI.GetComponentInChildren<Image>().fillAmount = thisint;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //     itemUI.SetActive(false);

                Play_CollectItemSFX();

                pActions.OriginalSpeed = plMovement.playerSpeed - buffAmount;
                plMovement.playerSpeed = pActions.OriginalSpeed;
                pActions.SprintSpeed = originalSprintSpeed;
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
                pActions = pl.GetComponent<PlayerActions>();
                plMovement = pl.GetComponent<PlayerMovement>();
                transform.parent = other.transform;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }

        }
    }
}
