using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject origin;
    [SerializeField] ParticleSystem attackIndication;

    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator AttackType()
    {
        // Attack indication for ranged and melee are implemented in the state machine, therefore they dont have to be here
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.singleShotSFX[Random.Range(0, obj.singleShotSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        //anim.SetTrigger("Trishot");
        Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
    }
}
