using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Attack
{
    [SerializeField] bool begin;
    [SerializeField] float radius;
    [SerializeField] float radiusEnd;
    [SerializeField] float expandSpeed;
    [SerializeField] float knockBackStr;
    [SerializeField] float knockBacktime;

    public override void AttackType()
    {
        Debug.Log("Shockwave happened!");
        reset();
        begin = true;
    }

    public override void Update()
    {
        if (begin)
        {
            radius += Time.deltaTime * expandSpeed;
            
            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, radius);
            foreach(Collider c in hits)
            {
                if (c.GetComponent<PlayerActions>() != null)
                {
                    PlayerActions player = c.GetComponent<PlayerActions>();
                    StartCoroutine(player.Mover(knockBackStr, knockBacktime, transform.forward));
                }
            }
            if(radius >= radiusEnd)
            {
                begin = false;
            }

        }
    }

    private void reset()
    {
        radius = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
