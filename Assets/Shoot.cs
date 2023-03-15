using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack
{
    [SerializeField] float interval;
    [SerializeField] GameObject Bullet;
    public override void AttackType()
    {
        // triggert animation
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {

        Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        for (int i = 0; i < 3; i++)
        {
            vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            ultimateAI.anim.SetTrigger("Shoot");
            vfx.GetComponent<Tank_VFXHandler>().ShootVFX();
            yield return new WaitForSeconds(interval);
        }

        /*Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        ultimateAI.anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(interval);
        rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        ultimateAI.anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(interval);
        rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        ultimateAI.anim.SetTrigger("Shoot");*/

    }
}
