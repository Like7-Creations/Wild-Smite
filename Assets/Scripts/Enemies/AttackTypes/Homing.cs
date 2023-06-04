using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Homing : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject origin;
    [SerializeField] float speed;
    [SerializeField] LineRenderer lr;

    public override void Start()
    {
        base.Start();
    }

    public override void attackLogic()
    {
        Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
    }

    public override void attackVFX()
    {

    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.homingSFX[Random.Range(0, obj.homingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Range_VFXHandler>().attackIndicationVFX.Play();
        }
    }

    /*public override IEnumerator AttackType()
    {
        // Attack indication for ranged and melee are implemented in the state machine, therefore they dont have to be here
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.homingSFX[Random.Range(0, obj.homingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        //anim.SetTrigger("Homing");
        Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
        yield return new WaitForSeconds(5);
    }*/
}
