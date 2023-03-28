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

    public override IEnumerator AttackType()
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
        //anim.SetTrigger("Trishot");
        Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
        yield return new WaitForSeconds(5);
    }
}
