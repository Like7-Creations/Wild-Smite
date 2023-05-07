using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gatling : Attack
{
    public float flurryInterval;
    public float bulletInterval;
    public float duration;
    public float bulletAmount;
    public float bulletSpeed;
    bool abilityActivated;

    public GameObject[] origins;

    public GameObject Bullet;

    public override void attackLogic()
    {
        //abilityActivated = true;
        for (int i = 0; i < origins.Length; i++)
        {
            StartCoroutine(shoot(i));
        }
    }

    IEnumerator shoot(int g)
    {
        float endTime = Time.time + duration;
        while (endTime >= Time.time)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Rigidbody rb = Instantiate(Bullet, origins[g].transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.GetComponent<Destroy>().damage = stats.RATK;
                Vector3 target = GetComponent<BossBehaviors>().chosenPlayer.transform.position - origins[g].transform.position;
                rb.AddForce(target.normalized * bulletSpeed, ForceMode.Impulse);
                yield return new WaitForSeconds(bulletInterval);
            }
            yield return new WaitForSeconds(flurryInterval);
        }
        GetComponent<BossBehaviors>().currentAttack = false;
    }

    public override void attackVFX()
    {
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Boss_VFXHandler>().GatlingVFX();
        }
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.gatlingSFX[Random.Range(0, obj.gatlingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    /*public override IEnumerator AttackType()
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
    }*/

    public override void Update()
    {
        /*if (abilityActivated)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer <= flurryInterval)
            {
                delayTimer += Time.deltaTime;
                if(delayTimer >= bulletInterval) 
                {
                    GameObject rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity);
                    rb.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
                    rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
                    delayTimer = 0;
                }
            }
            else
            {
                abilityActivated = false;
                durationTimer = 0;
                GetComponent<BossBehaviors>().currentAttack = false;
            }
        }*/
    }
}
