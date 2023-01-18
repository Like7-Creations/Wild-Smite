using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using Ultimate.AI;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.InputSystem.XR;
using UnityEditor.XR;

public class PlayerActions : MonoBehaviour
{
    PlayerMovement playerController;
    PlayerVFX VFX;
    public float health;
    [Header("Melee Settings")]
    public HitArea[] HitAreas;
    public int combo;
    Animator animator;
    [SerializeField]bool isAttacking;

    //[SerializeField] UltimateAI ClosestEnemy;
    
    [Space(5)]
    [Header("Range Settings")]
    public GameObject ProjectileOrigin;
    public GameObject bullet;
    Vector2 rotation; // For Controller
    Vector3 mousePos;
    Vector3 Aim;
    float timer;
    [SerializeField] float FireRate;
    [SerializeField] float bulletSpeed;
    bool fired;

    [Space(5)]
    [Header("Dash & Sprint Settings")]
    public float DashSpeed;
    public float DashTime;
    public float SprintSpeed;
    [HideInInspector] public Vector3 refer;
    [HideInInspector] public Vector3 Dashdir;
    float OriginalSpeed;

    List<UltimateAI> enemiesInDot;

    void Start()
    {
        playerController = GetComponent<PlayerMovement>();
        VFX = GetComponent<PlayerVFX>();
        animator = GetComponent<Animator>();
        OriginalSpeed = playerController.playerSpeed;
    }

    
    void Update()
    {
        #region AOE Stuff
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AOE();
        }
        #endregion

        #region Find Enemies With CheckSphere Then Check If Inside Dot Product
        if(enemiesInDot!= null)enemiesInDot = enemiesInDot.Distinct().ToList(); //Keeping it From Duplicates.
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<UltimateAI>() != null)
            {
                UltimateAI enemy = c.GetComponent<UltimateAI>();
                Vector3 player = transform.position;
                Vector3 toEnemy = enemy.gameObject.transform.position - player;
                toEnemy.y = 0;

                for (int i = 0; i < HitAreas.Length; i++)
                {
                    if (toEnemy.magnitude <= HitAreas[i].Radius)
                    {
                        if (Vector3.Dot(toEnemy.normalized, transform.forward) > Mathf.Cos(HitAreas[i].Angle * 0.5f * Mathf.Deg2Rad))
                        {
                            HitAreas[i].enemyFound = true;
                            enemiesInDot.Add(enemy);
                            Debug.Log("In Dot Product");
                        }
                        else HitAreas[i].enemyFound = false;
                    }
                    else HitAreas[i].enemyFound = false;
                }
            }
        }
        #endregion


        #region Range System
        mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        
        if (Input.GetButton("Fire1") & !fired)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag != "Player")
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
        #endregion
    }

    public void Attack(/*InputAction.CallbackContext context*/)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("" + combo);
            Debug.Log(combo);
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
        isAttacking = false;
        combo = 0;
    }

    public void EnableCollider()
    {
        if(enemiesInDot != null) 
        { 
            for (int i = 0; i < enemiesInDot.Count; i++)
            {
                enemiesInDot[i].TakeDamage(10/*what ever the player dmg is*/);
            }
        }
        VFX.MeleeVFX.emitting = true;
    }

    public void DisableCollider()
    {
        animator.applyRootMotion = true;
        VFX.MeleeVFX.emitting = false;
        playerController.enabled = true;
    }

    public void AOE()
    {
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<DummyEnemy>() != null)
            {
                DummyEnemy enemy = c.GetComponent<DummyEnemy>();
                enemy.health -= 100;
            }
        }
    }

    public void Dash()
    {
        VFX.DashVFX.emitting = true;
        StartCoroutine(Dashing());
    }

    public IEnumerator Dashing()
    {
        float startTime = Time.time;

        while (Time.time < startTime + DashTime)
        {
            playerController.controller.Move(Dashdir * DashSpeed * Time.deltaTime);
            yield return null;
        }
        VFX.DashVFX.emitting = false;
        Debug.Log("Dashhinggg");
    }

    public void Sprinting()
    {
        playerController.playerSpeed = SprintSpeed;
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerController.playerSpeed = OriginalSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);

        Gizmos.color = Color.blue;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < HitAreas.Length; i++)
        {
            if (HitAreas[i].enemyFound)
            {
                Color c = new Color(0f, 0, 1, 0.4f);
                UnityEditor.Handles.color = c;
            }
            else
            {
                Color c = new Color(0.8f, 0, 0, 0.4f);
                UnityEditor.Handles.color = c;
            }
            Vector3 rotatedForward = Quaternion.Euler(0,
             -HitAreas[i].Direction * 0.5f,
             0) * transform.forward;
          
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, HitAreas[i].Angle, HitAreas[i].Radius);
        }
    }
    public void RangeAttack(/*Vector2 input*/)
    {
        ProjectileOrigin.transform.LookAt(Aim);
        Rigidbody bullets = Instantiate(bullet, ProjectileOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        bullets.AddForce(ProjectileOrigin.transform.forward * bulletSpeed, ForceMode.Impulse);
        fired = true;
    }

    public void Rotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    } // will probably remove
}

[System.Serializable]
public class HitArea
{
    public float Direction;
    public float Angle;
    public float Radius;
    public bool enemyFound;
}
/*[CustomEditor(typeof(CombatManager))]
public class MyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var myscript = target as CombatManager;

        myscript.enemyFound = GUILayout.Toggle(myscript.enemyFound, "enemyFound");

        if(myscript.enemyFound )
        {
            myscript.enemycloser = GUILayout.Toggle(myscript.enemycloser ,"EnemyCloser");
        }
    }
}*/
