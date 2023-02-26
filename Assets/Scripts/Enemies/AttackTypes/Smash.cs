using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Attack
{
    [SerializeField] float Radius;
    [SerializeField] Transform smashPos;
    Vector3 targetHit;


    [SerializeField] float test;
    
    public override void Start()
    {
        base.Start();
    }

    public override void AttackType()
    {
        Debug.Log("Smash Attack");
        ultimateAI.anim.SetTrigger("Smash");
        targetHit = smashPos.position;
        Collider[] hits;
        hits = Physics.OverlapSphere(targetHit, Radius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<PlayerActions>() != null)
            {
                PlayerActions player = c.GetComponent<PlayerActions>();
                //player.health -= 10;
                  player.TakeDamage(10, transform.forward);
                //player.GetComponent<PlayerMovement>().knockUp();
            }
        }
        //yield return new WaitForSeconds(ultimateAI.attackRate);
    }

    public void Update()
    {
        float dist = Vector3.Distance(transform.position, smashPos.position);

        /*if(dist > GetComponent<Swipe>().Hitarea.Radius)
        {
            smashPos.transform.position = Vector3.Lerp(smashPos.position, transform.position, test * Time.deltaTime);
        }*/
        /*Vector3 pos = smashPos.localPosition;
        pos.x = Mathf.Clamp(smashPos.transform.position.x, 0f, 5f);
        pos.z = Mathf.Clamp(smashPos.transform.position.z, 0f, 5f);
        smashPos.transform.position.x = pos.x;
        smashPos.transform.position.z = pos.z;
        smashPos.transform.localPosition = Vector3.Lerp(smashPos.position, ultimateAI.players[0].transform.position, 5 * Time.deltaTime);*/
       // targetHit = smashPos.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
       // Gizmos.DrawWireSphere(smashPos.position, Radius);

        Gizmos.DrawSphere(smashPos.position, 1);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(targetHit, 1);

    }
}
