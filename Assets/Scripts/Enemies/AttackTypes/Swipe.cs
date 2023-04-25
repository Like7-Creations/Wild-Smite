using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Swipe : Attack
{
    List<PlayerActions> playersInArea;
    public HitArea Hitarea;
    public float knockBackStr;
    public float knockBacktime;

    public override void Start()
    {
        base.Start();
        playersInArea = new List<PlayerActions>();
    }

    public override IEnumerator AttackType()
    {
        //Debug.Log("Swipe Attack");
        if (vfx.isEnabled)
        {
            //vfx
            vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<BaseEnemy_SFXHandler>();
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

        anim.SetTrigger("SwipePrep");
        AnimationClip animClip = getAnimationClip(anim, "SwipePrep");
        float time = animClip.length;
        yield return new WaitForSeconds(time);

        if (sfx.isEnabled)
        {
            // sfx 
            var obj = GetComponent<BaseEnemy_SFXHandler>();
            if (obj.GetComponent<Tank_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Tank_SFXHandler>();
                var clip = clipObj.swipeSFX[Random.Range(0, clipObj.swipeSFX.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (obj.GetComponent<Boss_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Boss_SFXHandler>();
                var clip = clipObj.swipeSFX[Random.Range(0, clipObj.swipeSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
        //ultimateAI.anim.SetTrigger("Swipe");
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, Hitarea.Radius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<PlayerActions>() != null)
            {
                PlayerActions player = c.GetComponent<PlayerActions>();
                Vector3 enemy = transform.position;
                Vector3 toPlayer = player.gameObject.transform.position - enemy;
                toPlayer.y = 0;

                if (toPlayer.magnitude <= Hitarea.Radius)
                {
                    if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(Hitarea.Angle * 0.5f * Mathf.Deg2Rad))
                    {
                        Hitarea.enemyFound = true;
                        playersInArea.Add(player);
                    }
                    else Hitarea.enemyFound = false;
                }
                else Hitarea.enemyFound = false;
                
            }
        }

        if(playersInArea.Count > 0) 
        {
            playersInArea = playersInArea.Distinct().ToList();
            for (int i = 0; i < playersInArea.Count; i++)
            {
                // Debug.Log($"{playersInArea[i]} Got Hit");
                if (vfx.isEnabled)
                {
                    if (vfx.GetComponent<Tank_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Tank_VFXHandler>().SwipeVFX();
                    }
                    if (vfx.GetComponent<Boss_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().SwipeVFX();
                    }//vfx
                }
                playersInArea[i].TakeDamage(stats.MATK);
                StartCoroutine(playersInArea[i].Mover(knockBackStr, knockBacktime, transform.forward));
                if (vfx.isEnabled)
                {
                    if (vfx.GetComponent<Tank_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Tank_VFXHandler>().SummonVFX();
                    }
                    if (vfx.GetComponent<Boss_VFXHandler>() != null)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().SummonVFX();
                    }//vfx
                }
                //playersInArea[i].health -= 10;
            }
        }
        
        //yield return new WaitForSeconds(ultimateAI.attackRate);
    }
    private void OnDrawGizmosSelected()
    {
        
        if (Hitarea.enemyFound)
        {
            Color c = new Color(0f, 0, 1, 0.4f);
           // UnityEditor.Handles.color = c;
        }
        else
        {
            Color c = new Color(0.8f, 0, 0, 0.4f);
           // UnityEditor.Handles.color = c;
        }
        Vector3 rotatedForward = Quaternion.Euler(0,
         -Hitarea.Direction * 0.5f,
         0) * transform.forward;

       // UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, Hitarea.Angle, Hitarea.Radius);
    }
}

