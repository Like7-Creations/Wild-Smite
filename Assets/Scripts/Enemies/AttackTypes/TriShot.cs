using System.Collections;
using System.Collections.Generic;
using Ultimate.AI.Utils;
using UnityEngine;

public class TriShot : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float interval;
    [SerializeField] float offsetAngle;
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
            var clip = obj.triShotSFX[Random.Range(0, obj.triShotSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        ultimateAI.anim.SetTrigger("Trishot");
        for (int i = 0; i < 3; i++)
        {
            Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;

            if (i == 0)
            {
                rb.AddForce(ultimateAI.shooter.transform.right * offsetAngle, ForceMode.Impulse);
            }
            if (i == 2)
            {
                rb.AddForce(ultimateAI.shooter.transform.right * -offsetAngle, ForceMode.Impulse);
            }
        }

    }
}
