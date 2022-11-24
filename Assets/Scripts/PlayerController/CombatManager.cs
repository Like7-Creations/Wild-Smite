using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [SerializeField]bool isAttacking;
    Animator animator;
    public int combo;
    public GameObject Sword;
    bool test;
    bool shooting;
    public GameObject Companion;
    public GameObject bullet;
    Vector2 rotation;

    DisplayStats cS;
    //PlayerStats playstat;

    void Start()
    {
        animator = GetComponent<Animator>();
        Sword.GetComponent<BoxCollider>().enabled = false;
        cS = GetComponent<DisplayStats>();
    }

    // Update is called once per frame
    void Update()
    {
       // Attack();
        RangeAttack(rotation);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AOE();
        }
    }


    public void Attack(InputAction.CallbackContext context)
    {
       // if(Input.GetButtonDown("Fire1") & !isAttacking)
        //{
            isAttacking = true;
            animator.SetTrigger("" + combo);
        //}
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
                enemy.health -= cS.stat.currentMelee;
                //Debug.Log(cS.stat.currentMelee);
                enemy.hitted = true;
                Debug.Log("test");
            }
        }
    }

    public void AOE()
    {
        Collider[] hits;
        Debug.Log("Hitting");
        hits = Physics.OverlapSphere(Sword.transform.position, 5);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                //enemy.health -= cS.currentMelee;
                enemy.health -= 100;
                //Destroy(enemy.transform);
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

    public void RangeAttack(Vector2 input)
    {
        Vector3 playerDir = Vector3.right * input.x + Vector3.forward * input.y;
        if (playerDir.magnitude > 0f)
        {
            Quaternion newrotation = Quaternion.LookRotation(playerDir, Vector3.up);
            Companion.transform.rotation = Quaternion.RotateTowards(Companion.transform.rotation, newrotation, 1000 * Time.deltaTime);
            Rigidbody bullets = Instantiate(bullet, Companion.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullets.AddForce(Companion.transform.forward * 200, ForceMode.Impulse);

            //if bullets collide with Enemy Object then
            //do enemy.health -= cS.currentRanged;
        }
        //else ranged = false;
    }

    public void Rotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }
}
