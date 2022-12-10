using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DummyEnemy>() != null)
        {
            other.GetComponent<DummyEnemy>().hitted = true;
            other.GetComponent<DummyEnemy>().TakeDamage();
        }
       
    }
}
