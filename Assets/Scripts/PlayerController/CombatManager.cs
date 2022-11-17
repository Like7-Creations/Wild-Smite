using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]bool isAttacking;
    Animator animator;
    public int combo;
    public GameObject Sword;
    bool test;


    void Start()
    {
        animator = GetComponent<Animator>();
        Sword.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        /*if (test)
        {
            Collider[] hits;
            Debug.Log("Hitting");
            hits = Physics.OverlapSphere(Sword.transform.position, 2);
            foreach (Collider c in hits)
            {
                if (c.GetComponent<DummyEnemy>() != null)
                {
                    DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                    enemy.health -= 10;
                    enemy.hitted = true;
                    Debug.Log("test");
                }
            }
        }*/
    }


    public void Attack()
    {
        if(Input.GetButtonDown("Fire1") & !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("" + combo);
        }
    }

    public void startCombo()
    {
        isAttacking = false;
        if(combo < 3)
        {
            combo++;
        }
    }
    public void FinishAni()
    {
        //Debug.Log("hello?");
        isAttacking = false;
        combo = 0;
    }

    public void EnableCollider()
    {
        Sword.GetComponent<BoxCollider>().enabled = true;
        test = true;
        Collider[] hits;
        Debug.Log("Hitting");
        hits = Physics.OverlapSphere(Sword.transform.position, 1);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                enemy.health -= 10;
                enemy.hitted = true;
                Debug.Log("test");
            }
        }
    }


    public void DisableCollider()
    {
        Sword.GetComponent<BoxCollider>().enabled = false;
        test = false;
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
