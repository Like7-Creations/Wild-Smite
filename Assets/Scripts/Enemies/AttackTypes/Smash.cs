using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Attack
{
    [SerializeField] float Radius;
    [SerializeField] Transform smashPos;
    BossBehaviors bossbe;
    Vector3 targetHit;


    [SerializeField] float test;
    
    public override void Start()
    {
        base.Start();
        bossbe = GetComponent<BossBehaviors>();
    }

    public override void attackLogic()
    {
        targetHit = smashPos.position;
        Collider[] hits;
        hits = Physics.OverlapSphere(targetHit, Radius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<PlayerActions>() != null)
            {
                PlayerActions player = c.GetComponent<PlayerActions>();
                //player.health -= 10;
                if (vfx.isEnabled)
                {
                    if (vfx.GetComponent<Tank_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Tank_VFXHandler>().SmashVFX();
                    }
                    if (vfx.GetComponent<Boss_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().SmashVFX();
                    }//vfx;//vfx
                }
                player.TakeDamage(stats.MATK);
            }
        }
        GetComponent<BossBehaviors>().currentAttack = false;
    }

    public override void attackVFX()
    {
        if (vfx.isEnabled)
        {
            if (vfx.GetComponent<Tank_VFXHandler>() != null)
            {
                vfx.GetComponent<Tank_VFXHandler>().SmashVFX();
            }
            if (vfx.GetComponent<Boss_VFXHandler>() != null)
            {
                vfx.GetComponent<Boss_VFXHandler>().SmashVFX();
            }//vfx;//vfx
        }
    }

    public override void attackSFX()
    {
        var obj = GetComponent<Enemy_SFXHandler>();
        if (obj.GetComponent<Tank_SFXHandler>() != null)
        {
            var clipObj = obj.GetComponent<Tank_SFXHandler>();
            var clip = clipObj.smashSFX[Random.Range(0, clipObj.smashSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (obj.GetComponent<Boss_SFXHandler>() != null)
        {
            var clipObj = obj.GetComponent<Boss_SFXHandler>();
            var clip = clipObj.smashSFX[Random.Range(0, clipObj.smashSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Enemy_SFXHandler>();
            if (obj.GetComponent<Tank_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Tank_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (obj.GetComponent<Boss_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Boss_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Enemy_VFXHandler>().attackIndicationVFX.Play();
        }
    }

    /*public override IEnumerator AttackType()
    {
        // Debug.Log("Smash Attack");
        if (vfx.isEnabled)
        {
            //vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Enemy_SFXHandler>();
            if (obj.GetComponent<Tank_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Tank_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (obj.GetComponent<Boss_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Boss_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
        yield return new WaitForSeconds(0);

        if (sfx.isEnabled)
        {
            var obj = GetComponent<Enemy_SFXHandler>();
            if (obj.GetComponent<Tank_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Tank_SFXHandler>();
                var clip = clipObj.smashSFX[Random.Range(0, clipObj.smashSFX.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (obj.GetComponent<Boss_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Boss_SFXHandler>();
                var clip = clipObj.smashSFX[Random.Range(0, clipObj.smashSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
        //ultimateAI.anim.SetTrigger("Smash");
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
                    if (vfx.GetComponent<Tank_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Tank_VFXHandler>().SmashVFX();
                    }
                    if (vfx.GetComponent<Boss_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().SmashVFX();
                    }//vfx;//vfx
                }
                player.TakeDamage(stats.MATK);
                //player.GetComponent<PlayerMovement>().knockUp();
            }
        }
        if (vfx.isEnabled)
        {
            if (vfx.GetComponent<Tank_VFXHandler>() != null)
            {
                vfx.GetComponent<Tank_VFXHandler>().SmashVFX();
            }
            if (vfx.GetComponent<Boss_VFXHandler>() != null)
            {
                vfx.GetComponent<Boss_VFXHandler>().SmashVFX();
            }//vfx;//vfx
        }

    }*/

    public override void Update()
    {
        float dist = Vector3.Distance(transform.position, bossbe.chosenPlayer.transform.position);

        if(dist <= GetComponent<Swipe>().Hitarea.Radius)
        {
            smashPos.transform.position = Vector3.Lerp(smashPos.position, bossbe.chosenPlayer.transform.position, 5f * Time.deltaTime);
            Vector3 pos = smashPos.localPosition;
            pos.x = Mathf.Clamp(smashPos.localPosition.x, -4f, 5f);
            pos.z = Mathf.Clamp(smashPos.localPosition.z, 0f, 5f);
            if (vfx.GetComponent<Tank_VFXHandler>() != null)
            {
                vfx.GetComponent<Tank_VFXHandler>().smash_VFX.transform.position = smashPos.localPosition;
            }
            if (vfx.GetComponent<Boss_VFXHandler>() != null)
            {
                vfx.GetComponent<Boss_VFXHandler>().smash_VFX.transform.position = smashPos.localPosition;
            }//vfx;
        }
        /*else 
        { 
            smashPos.transform.position = Vector3.Lerp(smashPos.position, bossbe.chosenPlayer.transform.position, 5f * Time.deltaTime);
            Vector3 pos = smashPos.localPosition;
            pos.x = Mathf.Clamp(smashPos.localPosition.x, -4f, 5f);
            pos.z = Mathf.Clamp(smashPos.localPosition.z, 0f, 5f);
            smashPos.localPosition = pos;
            if(vfx.isEnabled)
            {
                if (vfx.GetComponent<Tank_VFXHandler>() != null)
                {
                    vfx.GetComponent<Tank_VFXHandler>().smash_VFX.transform.position = smashPos.localPosition;
                }
                if (vfx.GetComponent<Boss_VFXHandler>() != null)
                {
                    vfx.GetComponent<Boss_VFXHandler>().smash_VFX.transform.position = smashPos.localPosition;
                }//vfx;
            }
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(smashPos.position, Radius);

        Gizmos.DrawSphere(smashPos.position, 1);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(targetHit, 1);

    }
}
