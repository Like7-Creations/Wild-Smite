using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Attack
{
    [SerializeField] float Radius;
    public override void Start()
    {
        base.Start();
    }

    public override void AttackType()
    {
        Debug.Log("Smash Attack");
        ultimateAI.anim.SetTrigger("Smash");
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, Radius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<PlayerActions>() != null)
            {
                PlayerActions player = c.GetComponent<PlayerActions>();
                player.health -= 10;
            }
        }
        //yield return new WaitForSeconds(ultimateAI.attackRate);
    }
}
