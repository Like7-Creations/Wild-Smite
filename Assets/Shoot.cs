using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    [SerializeField] float bulletInterval;
    [SerializeField] float flurryInterval;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject origin;

    [SerializeField] int bulletAmount;
    [SerializeField] float duration;

    public override void attackLogic()
    {
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        float endTime = Time.time + duration;
        while (endTime >= Time.time)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.GetComponent<Destroy>().damage = stats.RATK;
                Vector3 target = GetComponent<BossBehaviors>().chosenPlayer.transform.position - origin.transform.position;
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
            vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
        }
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Tank_SFXHandler>();
            var clip = obj.shootSFX[Random.Range(0, obj.shootSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Tank_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Enemy_VFXHandler>().attackIndicationVFX.Play();
        }
    }

    /*public override IEnumerator AttackType()
    {
        // triggert animation
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Tank_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }


        for (int i = 0; i < 3; i++)
        {
            Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.GetComponent<Destroy>().damage = stats.RATK;
            if (vfx.isEnabled)
            {
                vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
            }
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            //ultimateAI.anim.SetTrigger("Shoot");
            if (vfx.isEnabled)
            {
                vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
            }
            if (sfx.isEnabled)
            {
                var obj = GetComponent<Tank_SFXHandler>();
                var clip = obj.shootSFX[Random.Range(0, obj.shootSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(interval);
        }
    }*/
}
