using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Homing : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float AimTime;
    [SerializeField] float speed;
    [SerializeField] LineRenderer lr;
    [SerializeField] bool aiming;
    public override void AttackType()
    {
        //Debug.Log("Homing Attack Happened");
        ultimateAI.anim.SetTrigger("Homing");
        StartCoroutine(thisAttack());
    }

    public override void Update()
    {
        
    }

    IEnumerator thisAttack()    
    {
        aiming = true;
        vfx.attackIndicationVFX.Play();
        yield return new WaitForSeconds(AimTime);
        GameObject rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<GameObject>();
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
        aiming = false;
        yield return new WaitForSeconds(5);
    }
}
