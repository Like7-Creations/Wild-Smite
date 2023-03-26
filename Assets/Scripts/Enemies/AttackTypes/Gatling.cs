using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gatling : Attack
{
    public float shootingDuration;
    public float delayBetweenBullets;
    public float durationTimer;
    public float delayTimer;
    bool abilityActivated;

    public GameObject origin;

    public GameObject Bullet;

    public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        abilityActivated = true;
    }

    public override void Update()
    {
        if (abilityActivated)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer <= shootingDuration)
            {
                delayTimer += Time.deltaTime;
                if(delayTimer >= delayBetweenBullets) 
                {
                    GameObject rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity);
                    rb.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
                    rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
                    if(vfx.isEnabled)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().GatlingVFX();
                    }
                    if (sfx.isEnabled)
                    {
                        var obj = GetComponent<Boss_SFXHandler>();
                        var clip = obj.gatlingSFX[Random.Range(0, obj.gatlingSFX.Length)];
                        audioSource.PlayOneShot(clip);
                    }
                    if (vfx.isEnabled)
                    {
                        vfx.GetComponent<Boss_VFXHandler>().GatlingVFX();
                    }
                    delayTimer = 0;
                }
            }
            else
            {
                abilityActivated = false;
                durationTimer = 0;
            }
        }
    }
}
