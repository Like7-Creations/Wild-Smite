using System.Collections;
using System.Collections.Generic;
using Ultimate.AI.Utils;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class TriShot : Attack
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float interval;
    [SerializeField] float offsetAngle;
    public override void AttackType()
    {
       // Debug.Log("Trishot");
        // set animatioon trigger here
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
