using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] ParticleSystem attackIndication;

    public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.singleShotSFX[Random.Range(0, obj.singleShotSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        ultimateAI.anim.SetTrigger("SingleShot");
        Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
    }
}
