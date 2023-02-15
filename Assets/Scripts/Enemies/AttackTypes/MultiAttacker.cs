using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttacker : MonoBehaviour
{
    public Attack[] attacksList;
    public void AttackPlayer()
    {
        //StartCoroutine(attacksList[Random.Range(0, attacksList.Length)].AttackType());
        attacksList[Random.Range(0, attacksList.Length)].AttackType();
    }
}
