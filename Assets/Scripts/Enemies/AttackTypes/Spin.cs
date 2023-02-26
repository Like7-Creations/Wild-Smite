using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Attack
{
    public float noOfAttacks;
    public float TimeBetweenAttacks;

    public override void AttackType()
    {
        Debug.Log("Spin attack");
        ultimateAI.attackRange = 2;
        fov.viewAngle = 360;
        // set animation
        StartCoroutine(SpinAttack());
    }

    IEnumerator SpinAttack()
    {
        for (int i = 0; i < noOfAttacks; i++)
        {
            ultimateAI.playerTakeDamage();
            yield return new WaitForSeconds(TimeBetweenAttacks); // We either use this or we just use the normal void and call the melee as event at attack time
        }
    }
}
