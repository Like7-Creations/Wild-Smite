using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class CombatManager : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;
    [Header("Melee Settings")]
    [SerializeField]bool isAttacking;
    public int combo;
    public GameObject Sword;
    [SerializeField] float hitDetectionDirection;
    [SerializeField] float hitDetectionDirectionMulti;
    [SerializeField] float hitDetectionAngle;
    [SerializeField] float hitDetectionAngleMulti;
    [SerializeField] float hitRadius;
    [SerializeField] float hitRadiusDivison;
    public GameObject trail;
    public GameObject trailTwo;
    
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
    [SerializeField] bool enemyFound;
    [SerializeField] bool enemycloser;


    DisplayStats cS;
    //PlayerStats playstat;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        Sword.GetComponent<BoxCollider>().enabled = false;
        cS = GetComponent<DisplayStats>();
    }

    
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

            if (Physics.Raycast(ray, out hit, 100))
            {
                Aim = hit.point;
                Aim.y = 1;
                // Debug.Log(hit.transform.name);
                RangeAttack();
            }
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.blue);
        }
        if (fired)
        {
            timer += Time.deltaTime;
            if (timer > FireRate)
            {
                timer = 0;
                fired = false;
            }
        }

        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (Collider c in hits)
        {

            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                Vector3 player = transform.position;
                Vector3 toEnemy = enemy.gameObject.transform.position - player;
                toEnemy.y = 0;

                if (toEnemy.magnitude <= hitRadius)
                {
                    if (Vector3.Dot(toEnemy.normalized, transform.forward) >
                        Mathf.Cos(hitDetectionAngle * 0.5f * Mathf.Deg2Rad))
                    {
                        enemyFound = true;
                        Debug.Log("In Dot Product");
                    }
                }

                if (toEnemy.magnitude <= hitRadius / hitRadiusDivison)
                {
                    if (Vector3.Dot(toEnemy.normalized, transform.forward) >
                        Mathf.Cos((hitDetectionAngle * hitDetectionAngleMulti) * 0.5f * Mathf.Deg2Rad))
                    {
                        enemycloser = true;
                        Debug.Log("Enemy Is Closer");
                    }
                }
            }
            else
            {
                enemyFound = false;
                enemycloser = false;
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
        DummyEnemy closestEnemy = null;
        bool Closer = false;
        List<DummyEnemy> enemiesInDot = new List<DummyEnemy>();
        Sword.GetComponent<BoxCollider>().enabled = true;
        trail.GetComponent<TrailRenderer>().emitting = true;
        playerController.playerSpeed = 0;
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                Vector3 player = transform.position;
                Vector3 toEnemy = enemy.gameObject.transform.position - player;
                toEnemy.y = 0;
                //enemy.health -= cS.stat.currentMelee;
                //Debug.Log(cS.stat.currentMelee);
                if (toEnemy.magnitude <= hitRadius)
                {
                    if (Vector3.Dot(toEnemy.normalized, transform.forward) >
                        Mathf.Cos(hitDetectionAngle * 0.5f * Mathf.Deg2Rad))
                    {
                        enemiesInDot.Add(enemy);
                        //transform.position = playerPos + thisobj;
                        //transform.LookAt(thisobj);
                        //enemy.hitted = true;
                        // enemy.TakeDamage();
                    }
                }
                else if (toEnemy.magnitude <= hitRadius / hitRadiusDivison)
                {
                    if (Vector3.Dot(toEnemy.normalized, transform.forward) >
                        Mathf.Cos((hitDetectionAngle * hitDetectionAngleMulti) * 0.5f * Mathf.Deg2Rad))
                    {
                        enemiesInDot.Add(enemy);
                        Closer = true;
                        //transform.LookAt(thisobj);
                        //enemy.hitted = true;
                        //enemy.TakeDamage();
                    }
                }

            }
        }
        closestEnemy = GetClosestEnemy(enemiesInDot);
        Debug.Log($"closest enemy is{closestEnemy.gameObject.name}");

        if (closestEnemy != null)
        {
            Vector3 thisobj = closestEnemy.transform.position;
            Vector3 playerPos = closestEnemy.transform.rotation * thisobj.normalized * 0.8f;
            float dist = Vector3.Distance(thisobj, transform.position);
            if (dist > 1)
            {
                playerController.enabled = false;
                trailTwo.GetComponent<TrailRenderer>().emitting = true;
                animator.applyRootMotion = false;
                transform.LookAt(thisobj);
                closestEnemy.hitted = true;
                if (!Closer) transform.position = playerPos + thisobj;
            }
        }
    }


    DummyEnemy GetClosestEnemy(List<DummyEnemy> enemiesInDot)
    {
        DummyEnemy tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (DummyEnemy t in enemiesInDot)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void DisableCollider()
    {
        playerController.enabled = true;
        animator.applyRootMotion = true;
        trail.GetComponent<TrailRenderer>().emitting = false;
        trailTwo.GetComponent<TrailRenderer>().emitting = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);

        Gizmos.color = Color.blue;
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyFound)
        {
            Color c = new Color(0f, 0, 1, 0.4f);
            UnityEditor.Handles.color = c;
        }
        if (enemycloser)
        {
            Color c = new Color(1f, 0, 1, 0.4f);
            UnityEditor.Handles.color = c;
        }
        if(!enemycloser & !enemyFound)
        {
            Color c = new Color(0.8f,0,0,0.4f);
            UnityEditor.Handles.color = c;
        }

        Vector3 rotatedForward = Quaternion.Euler(0,
            -hitDetectionDirection * 0.5f,
            0) * transform.forward;
        Vector3 rotatedForwardTwo = Quaternion.Euler(0,
            (-hitDetectionDirection * hitDetectionDirectionMulti) * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, hitDetectionAngle, hitRadius);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForwardTwo, hitDetectionAngle * hitDetectionAngleMulti, hitRadius / hitRadiusDivison);
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
