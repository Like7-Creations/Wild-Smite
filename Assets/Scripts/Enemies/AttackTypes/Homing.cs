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
        Debug.Log("Homing Attack Happened");
        StartCoroutine(thisAttack());
    }

    public override void Update()
    {
        
    }

    IEnumerator thisAttack()    
    {
        aiming = true;
        yield return new WaitForSeconds(AimTime);
        Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        aiming= false;
        yield return new WaitForSeconds(5);
    }
}
