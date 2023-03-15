using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Homing : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float speed;
    [SerializeField] LineRenderer lr;
    public override IEnumerator AttackType()
    {
        //Debug.Log("Homing Attack Happened");
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Range_SFXHandler>();
            var clip = obj.homingSFX[Random.Range(0, obj.homingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        ultimateAI.anim.SetTrigger("Homing");
        GameObject rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<GameObject>();
        rb.GetComponent<Destroy>().damage = GetComponent<EnemyStats>().RATK;
        yield return new WaitForSeconds(5);
    }
}
