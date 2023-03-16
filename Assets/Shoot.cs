using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    [SerializeField] float interval;
    [SerializeField] GameObject Bullet;
    public override IEnumerator AttackType()
    {
        // triggert animation
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);

        Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        for (int i = 0; i < 3; i++)
        {
            if (vfx.isEnabled)
            {
                vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
            }
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            ultimateAI.anim.SetTrigger("Shoot");
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
