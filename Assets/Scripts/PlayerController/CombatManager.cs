using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;
    [Header("Melee Settings")]
    [SerializeField]bool isAttacking;
    public int combo;
    public GameObject Sword;
    [SerializeField] float hitDetectionDirection;
    [SerializeField] float hitDetectionAngle;
    [SerializeField] float hitRadius;
    public GameObject trail;
    
    [Space(5)]
    [Header("Range Settings")]
    public GameObject Companion;
    public GameObject bullet;
    Vector2 rotation;
    Vector3 mousePos;
    Vector3 Aim;
    float timer;
    [SerializeField]float FireRate;
    [SerializeField] float bulletSpeed;
    bool fired;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    DisplayStats cS;
    //PlayerStats playstat;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        Sword.GetComponent<BoxCollider>().enabled = false;
        cS = GetComponent<DisplayStats>();
    }

    // Update is called once per frame
    void Update()
    {
       // Attack();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AOE();
        }

        /*mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);*/
        if (Input.GetButton("Fire1") & !fired)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                Aim = hit.point;
                Aim.y = 1;
                Debug.Log(hit.transform.name);
                RangeAttack();
            }
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.blue);
        }
        if (fired)
        {
            timer += Time.deltaTime;
            if(timer > FireRate)
            {
                timer = 0;
                fired = false;
            }
        }
    }


    public void Attack(InputAction.CallbackContext context)
    {
        // if(Input.GetButtonDown("Fire1") & !isAttacking)
        //{
        //playerController.playerSpeed = 0;
        isAttacking = true;
        animator.SetTrigger("" + combo);

        //}
    }

    public void startCombo()
    {
        //playerController.playerSpeed = playerController.originalSpeed;
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
        //Sword.GetComponent<BoxCollider>().enabled = true;
        Collider[] hits;
        trail.GetComponent<TrailRenderer>().emitting = true;
        //Debug.Log("Hitting");
        playerController.playerSpeed = 0;
        hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                /*enemy.health -= cS.stat.currentMelee;
                //Debug.Log(cS.stat.currentMelee);
                enemy.hitted = true;
                Debug.Log("test");*/
                Debug.Log("Enemy is in sphere");
                Vector3 player = transform.position;
                Vector3 toEnemy = enemy.gameObject.transform.position - player;
                toEnemy.y = 0;
                if(toEnemy.magnitude <= hitDetectionDirection)
                {
                    if(Vector3.Dot(toEnemy.normalized, transform.position) >
                        Mathf.Cos(hitDetectionAngle * 0.5f * Mathf.Deg2Rad))
                    {
                        enemy.hitted = true;
                        enemy.TakeDamage();
                    }
                }
            }
        }
    }
    public void DisableCollider()
    {
        trail.GetComponent<TrailRenderer>().emitting = false;
        Sword.GetComponent<BoxCollider>().enabled = false;
        playerController.playerSpeed = playerController.originalSpeed;
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
                //Debug.Log("test");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f,0,0,0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(0,
            -hitDetectionDirection * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, hitDetectionAngle, hitRadius);
    }
    public void RangeAttack(/*Vector2 input*/)
    {
        Companion.transform.LookAt(Aim);
        Rigidbody bullets = Instantiate(bullet, Companion.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        bullets.AddForce(Companion.transform.forward * bulletSpeed, ForceMode.Impulse);
        fired = true;
    }

    public void Rotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }
}
