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

    public override IEnumerator AttackType()
    {
        // Debug.Log("Smash Attack");
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Tank_SFXHandler>();
            var clip = obj.smashSFX[Random.Range(0, obj.smashSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
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
                GetComponent<Animator>().SetTrigger("Smash");
                if (vfx.isEnabled)
                {
                    vfx.GetComponent<Tank_VFXHandler>().SmashVFX();//vfx
                }
                player.TakeDamage(10, transform.forward);
                //player.GetComponent<PlayerMovement>().knockUp();
            }
        }
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Tank_VFXHandler>().SmashVFX();//vfx
        }

    }

    public void Update()
    {
        float dist = Vector3.Distance(transform.position, ultimateAI.players[0].transform.position);

        if(dist <= GetComponent<Swipe>().Hitarea.Radius)
        {
             smashPos.transform.position = Vector3.Lerp(smashPos.position, ultimateAI.players[0].transform.position, 5f * Time.deltaTime);
        }
        else 
        { 
            smashPos.transform.position = Vector3.Lerp(smashPos.position, ultimateAI.players[0].transform.position, 5f * Time.deltaTime);
            Vector3 pos = smashPos.localPosition;
            pos.x = Mathf.Clamp(smashPos.localPosition.x, -4f, 5f);
            pos.z = Mathf.Clamp(smashPos.localPosition.z, 0f, 5f);
            smashPos.localPosition = pos;
            vfx.GetComponent<Tank_VFXHandler>().smash_VFX.transform.position = smashPos.localPosition;
        }
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
