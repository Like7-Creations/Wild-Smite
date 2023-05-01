using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    [SerializeField] float interval;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject origin;

    public override IEnumerator AttackType()
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

       /* anim.SetTrigger("ShootPrep");
        AnimationClip animClip = getAnimationClip(anim, "ShootPrep");
        float time = animClip.length;
        yield return new WaitForSeconds(time);*/

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
    }
}
