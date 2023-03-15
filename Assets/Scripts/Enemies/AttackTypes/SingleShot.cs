using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Attack
{
    [SerializeField] GameObject Bullet;
    public override void AttackType()
    {
        //Debug.Log("SingleShot");
        ultimateAI.anim.SetTrigger("SingleShot");
        Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
    }
}
