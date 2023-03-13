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

    public override void AttackType()
    {
        StartCoroutine(gatling());
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

    IEnumerator gatling()
    {
        vfx.attackIndicationVFX.Play();
        yield return new WaitForSeconds(0.5f);
        abilityActivated = true;
    }
}
