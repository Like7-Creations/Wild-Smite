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

    public GameObject Bullet;

    public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
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
                    Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
                    rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
                    if (sfx.isEnabled)
                    {
                        var obj = GetComponent<Boss_SFXHandler>();
                        var clip = obj.gatlingSFX[Random.Range(0, obj.gatlingSFX.Length)];
                        audioSource.PlayOneShot(clip);
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
