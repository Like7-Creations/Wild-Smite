using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using Ultimate.AI;
using System.Linq;
using UnityEditor;
using UnityEngine.Rendering;
//using System.Diagnostics;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] bool isGamepad;
    //[SerializeField] bool isGamepad;

    PlayerMovement playerController;
    PlayerVFX VFX;
    public float health;
    [Header("Melee Settings")]
    public HitArea[] HitAreas;
    public int combo;
    Animator animator;
    [SerializeField]bool isAttacking;
    [SerializeField] float meleeDash;

    //[SerializeField] UltimateAI ClosestEnemy;
    [Space(5)]
    [Header("Range Settings")]
    public GameObject ProjectileOrigin;
    public GameObject bullet;
    float timer;
    [SerializeField] float FireRate;
    [SerializeField] float bulletSpeed;
    bool fired;
    [HideInInspector] public Vector2 aim;
    [HideInInspector] public bool shooting;
    float deadzone = 0.1f;

    [Space(5)]
    [Header("Dash & Sprint Settings")]
    public float DashSpeed;
    public float DashTime;
    public float DashCDN;
    public float SprintSpeed;
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public Vector3 refer;
    [HideInInspector] public Vector3 Dashdir;
    float OriginalSpeed;

    List<UltimateAI> enemiesInDot = new List<UltimateAI>();
    PlayerControl Pc;
    PlayerControls controls;

    void Awake()
    {
        Pc = GetComponent<PlayerControl>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

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
        enemiesInDot.Clear();
        if (enemiesInDot != null) { enemiesInDot = enemiesInDot.Distinct().ToList(); } //Keeping it From Duplicates.
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
                        }
                        else HitAreas[i].enemyFound = false;
                    }
                    else HitAreas[i].enemyFound = false;
                }
            }
        }
        #endregion

        #region Range System
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float raylength;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if(groundPlane.Raycast(cameraRay,out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            ProjectileOrigin.transform.LookAt(new Vector3(pointToLook.x, ProjectileOrigin.transform.position.y, pointToLook.z));
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

        //Rotation();
        aim = controls.Player.Rotation.ReadValue<Vector2>();
        if (shooting)
        {
            RangeAttack();
            //Rotation();
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
        if(enemiesInDot != null)
        {
            transform.LookAt(enemiesInDot[0].transform);
        }
        playerController.controller.Move(transform.forward * meleeDash);
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
        VFX.Melee();
    }

    public void DisableCollider()
    {
        //animator.applyRootMotion = true;
        VFX.Melee();
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
        StartCoroutine(Dashing());
    }

    public IEnumerator Dashing()
    {
        float startTime = Time.time;

        VFX.Dash();
        while (Time.time < startTime + DashTime)
        {
            playerController.controller.Move(Dashdir * DashSpeed * Time.deltaTime);
            yield return null;
        }
        VFX.Dash();
    }

    public void Sprint()
    {
        playerController.playerSpeed = SprintSpeed;
    }

    public void UnSprint()
    {
        playerController.playerSpeed = OriginalSpeed;
    }

    public void RangeAttack()
    {
        if (!fired)
        {
            Rigidbody bullets = Instantiate(bullet, ProjectileOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullets.AddForce(ProjectileOrigin.transform.forward * bulletSpeed, ForceMode.Impulse);
            fired = true;
        }
    }

    public void Rotation()
    {
        //Debug.Log(aim);
        if (Mathf.Abs(aim.x) > deadzone || Mathf.Abs(aim.y) > deadzone)
        {
            Vector3 playerDir = Vector3.right * aim.x + Vector3.forward * aim.y;
            if (playerDir.sqrMagnitude > 0.0f)
            {
                Quaternion newrotation = Quaternion.LookRotation(playerDir, Vector3.up);
                ProjectileOrigin.transform.rotation = Quaternion.RotateTowards(ProjectileOrigin.transform.rotation, newrotation, 1000f * Time.deltaTime);
            }
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
