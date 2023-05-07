using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Flurry : Attack
{
    public GameObject Bullet;
    public GameObject[] origins;
    public float spinDuration;
    public float speed;
    public float fireRate;
    public float bulletsPerArm;

    float loops;
    public float loopLimit;

    public float bulletspeed;

    public override void attackLogic()
    {
        /*float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        float time = 0;

        while (t < spinDuration)
        {
            time += Time.deltaTime;
            if (time > fireRate)
            {
                vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
                Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(origin.transform.forward * 10f, ForceMode.Impulse);
                time = 0;
            }
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / spinDuration) % 360.0f;
            origin.transform.eulerAngles = new Vector3(origin.transform.eulerAngles.x, yRotation, origin.transform.eulerAngles.z);

            //yield return null;
        }*/
        loops++;

        if(loops == loopLimit)
        {
            anim.SetBool("FlurryLoop", false);
            loops = 0;
        }
        else
        {
            for (int i = 0; i < origins.Length; i++)
            {
                StartCoroutine(Shoot(origins[i]));
            }
        }

        GetComponent<BossBehaviors>().currentAttack = false;
        //StartCoroutine(OriginsShoot());
    }

    IEnumerator OriginsShoot()
    {
        for (int i = 0; i < origins.Length; i++)
        {
            StartCoroutine(Shoot(origins[i]));
        }
            yield return null;
    }

    IEnumerator Shoot(GameObject origin)
    {
        for (int i = 0; i < bulletsPerArm; i++)
        {
            Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(origin.transform.forward * bulletspeed, ForceMode.Impulse);
            yield return new WaitForSeconds(fireRate);
        }
       // yield return null;
    }

    public override void attackVFX()
    {
        vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.flurrySFX[Random.Range(0, obj.flurrySFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Boss_VFXHandler>().attackIndicationVFX.Play();
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
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        float time = 0;

        while (t < spinDuration)
        {
            time += Time.deltaTime;
            if (time > fireRate)
            {
                vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
                Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(origin.transform.forward * 10f, ForceMode.Impulse);
                if (sfx.isEnabled)
                {
                    var obj = GetComponent<Boss_SFXHandler>();
                    var clip = obj.flurrySFX[Random.Range(0, obj.flurrySFX.Length)];
                    audioSource.PlayOneShot(clip);
                    vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
                }
                time = 0;
            }
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / spinDuration) % 360.0f;
            origin.transform.eulerAngles = new Vector3(origin.transform.eulerAngles.x, yRotation, origin.transform.eulerAngles.z);

            yield return null;
        }
    }*/
}
