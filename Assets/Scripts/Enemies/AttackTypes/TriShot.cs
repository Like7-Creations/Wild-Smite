using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject origin;
    [SerializeField] float interval;
    [SerializeField] float offsetAngle;

    public override void Start()
    {
        base.Start();
    }

    public override void attackLogic()
    {
        for (int i = 0; i < 3; i++)
        {
            Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;

            if (i == 0)
            {
                rb.AddForce(origin.transform.right * offsetAngle, ForceMode.Impulse);
            }
            if (i == 2)
            {
                rb.AddForce(origin.transform.right * -offsetAngle, ForceMode.Impulse);
            }
        }
    }

    public override void attackVFX()
    {

    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.triShotSFX[Random.Range(0, obj.triShotSFX.Length)];
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
            var clip = obj.triShotSFX[Random.Range(0, obj.triShotSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        //anim.SetTrigger("Trishot");
        for (int i = 0; i < 3; i++)
        {
            Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;

            if (i == 0)
            {
                rb.AddForce(origin.transform.right * offsetAngle, ForceMode.Impulse);
            }
            if (i == 2)
            {
                rb.AddForce(origin.transform.right * -offsetAngle, ForceMode.Impulse);
            }
        }

    }*/
}
