using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using Ultimate.AI;
using System.Linq;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;
using UnityEditor.UIElements;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
//using System.Diagnostics;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] bool isGamepad;

    public bool lockX, lockY, lockZ;
    private Vector3 startRotation;

    PlayerMovement playerController;
    PlayerVFX VFX;

    [Header("Stats stuff")]
    public PlayerStats pStats;
    public float health;



    [Space(5)]
    [Header("Melee Settings")]
    public HitArea[] HitAreas;
    public int combo;
    Animator animator;
    bool invincible;
    [SerializeField] bool isAttacking;
    [SerializeField] float meleeDash;
    [SerializeField] float meleeknockback;
    [SerializeField] float flashDamageTime;
    [SerializeField] GameObject flash;
    [SerializeField] SkinnedMeshRenderer hit;
    [SerializeField] Material red;
    [HideInInspector] public Vector3 knockBackDir;

    [Space(5)]
    [Header("Range Settings")]
    public GameObject ProjectileOrigin;
    public GameObject bullet;
    float timer;
    [SerializeField] float FireRate;
    [SerializeField] float bulletSpeed;
    bool fired;
    [HideInInspector] public Vector2 aim;
    [HideInInspector] public Vector2 playerLookDir;
    [HideInInspector] public bool shooting;
    [HideInInspector] public bool mouseShooting;
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

    [Header("AOE Settings")]
    public bool charging;
    public float startingRadius;
    public float chargingSpeed;
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;

    public List<EnemyStats> enemiesInDot = new List<EnemyStats>();
    PlayerControl Pc;
    PlayerControls controls;

    [Space(5)]
    [Header("VFX Events")]

    public UnityEvent trigger_walkVFX;
    public UnityEvent trigger_dashVFX;
    public UnityEvent trigger_sprintVFX;

    public UnityEvent trigger_attackVFX_left;
    public UnityEvent trigger_attackVFX_right;
    public UnityEvent trigger_dmgVFX;

    public UnityEvent trigger_aoeVFX;
    public UnityEvent trigger_aoeChargeVFX;


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
        pStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        OriginalSpeed = playerController.playerSpeed;
        startRotation = transform.rotation.eulerAngles;
    }

    public float currentCharge = 0;

    void Update()
    {
        #region Area Of Effect
        ChargeAOE();

        #endregion

        #region Find Enemies With CheckSphere Then Check If Inside Dot Product
        //if (enemiesInDot != null) { enemiesInDot = enemiesInDot.Distinct().ToList(); } //Keeping it From Duplicates.
        enemiesInDot.Clear();
        if (enemiesInDot.Count > 0)
        {
            for (int i = 0; i < enemiesInDot.Count; i++)
            {
                if (enemiesInDot[i] == null)
                {
                    enemiesInDot.RemoveAt(i);
                }
            }
        }
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemy = c.GetComponent<EnemyStats>();
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
                            if (!enemiesInDot.Contains(enemy))
                            {
                                enemiesInDot.Add(enemy);
                            }
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
        if (groundPlane.Raycast(cameraRay, out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);
            playerLookDir = pointToLook;
            //pointToLook.y = 1;
            //playerLookDir.y = 1;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            //float dist = Vector3.Distance(transform.position, pointToLook);
            //if(dist >= 1.5f)
            transform.LookAt(pointToLook);
            if (mouseShooting)
            {
                ProjectileOrigin.transform.LookAt(new Vector3(pointToLook.x, ProjectileOrigin.transform.position.y, pointToLook.z));
            }
        }
        //transform.LookAt(playerLookDir);

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

        //Farhan's  UwU Code-----
        //Check if sprinting, consume stamina by the specified amount.

        if (isSprinting)
        {
            pStats.UseSprint(pStats.sprint);
        }
        else if (!isSprinting)
        {
            //Debug.Log("recover stamina called");
            if (pStats.stamina < pStats.playerData.stamina)
            {
                if (!pStats.begin_STAMRecov)
                {
                    StartCoroutine(pStats.STAM_RecovDelay());
                }
            }

        }

        if (pStats.stam_recov)
            pStats.RecoverStamina(pStats.recovRate_STAMINA);

        //Implement HP Recov later.

        //Testing Saving

        if (Input.GetKeyDown(KeyCode.P))
        {
            // save the game anytime before loading a new scene
            DataPersistenceManager.instance.SaveGame();

            Debug.Log("Game is being Saved");


            // load the main menu scene
            SceneManager.LoadSceneAsync("_MVP_MainMenu");
        }

        //Farhan's Code-----


        if (lastrands.Count == 2)
        {
            int previos = lastrands[1];
            lastrands.Clear();
            lastrands.Add(previos);
        }
    }

    void LateUpdate()
    {
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            lockX ? startRotation.x : newRotation.x,
            lockY ? startRotation.y : newRotation.y,
            lockZ ? startRotation.z : newRotation.z
        );
    }

    #region Player Take Damage
    public void TakeDamage(float damage)
    {
        print("Take damage called");
        if (!invincible)
        {
            if (!isAttacking)
            {
                animator.SetTrigger("Hit");
            }
            Color origin = hit.material.color;
            flash.gameObject.SetActive(true);
            /* hit.materials[0].color = Color.red;
             for (int i = 0; i < hit.materials.Length; i++)
             {
             }*/

            //Farhan's Code-----
            pStats.LoseHealth((int)damage);      //Call Take Damage function, since the actual stat values have a private set.

            trigger_dmgVFX.Invoke();

            //Farhan's Code-----

            //StartCoroutine(Mover(10, 0.03f, knockbackdir));
            //playerController.controller.Move(transform.forward * meleeknockback * Time.deltaTime);
            //Vector3 knockBack = transform.position - transform.forward * knockbackStr;
            //knockBack.y = 0;
            //transform.position = knockBack;
            StartCoroutine(resetFlashDamage(origin));
            // hit.materials[0].color = origin;
        }
    }

    IEnumerator resetFlashDamage(Color origin)
    {
        yield return new WaitForSeconds(flashDamageTime);
        flash.gameObject.SetActive(false);
        // hit.materials[0].color = origin;
    }
    #endregion

    bool testCombat;
    [SerializeField] List<int> lastrands = new List<int>();

    public void Attack(/*InputAction.CallbackContext context*/)
    {
        if (!shooting)
        {
            if (!testCombat)
            {
                //transform.LookAt(playerLookDir);
                if (enemiesInDot.Count > 0) { transform.LookAt(GetClosestEnemy(enemiesInDot).transform); };
                //StartCoroutine(Mover(2, 0.1f, Dashdir));
                int randomNumber = UnityEngine.Random.Range(0, 2);
                int previous = randomNumber;



                if (!lastrands.Contains(randomNumber))
                {
                    animator.SetTrigger("Attack" + randomNumber.ToString());


                    lastrands.Add(randomNumber);

                }
                else
                {
                    Attack();
                }
            }
        }

    }

    EnemyStats GetClosestEnemy(List<EnemyStats> enemies)
    {
        EnemyStats tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (EnemyStats t in enemies)
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

    //new combat system Build one
    public void testTrue()
    {
        testCombat = true;
    }
    public void testfalse()
    {
        testCombat = false;
        animator.ResetTrigger("Attack" + 0);
        animator.ResetTrigger("Attack" + 1);
        playerController.playerSpeed = OriginalSpeed;
    }

    public void startCombo()
    {
        playerController.playerSpeed = 0f;
        isAttacking = false;
        if (combo < 3)
        {
            combo++;
        }
    }
    public void FinishAni()
    {
        isAttacking = false;
        combo = 0;
    }

    //Event Required
    public void EnableCollider()
    {
        if (enemiesInDot != null)
        {
            for (int i = 0; i < enemiesInDot.Count; i++)
            {
                Debug.Log("enable collider called");
                enemiesInDot[i].TakeDamage(pStats.m_ATK, pStats);
            }
        }
        //VFX.Melee();
    }

    //Event Required
    public void DisableCollider()
    {
        //trigger_attackVFX.Invoke();
        //animator.applyRootMotion = true;
        //VFX.Melee();
        playerController.enabled = true;
    }

    public void SwipeLeft()
    {
        //trigger_attackVFX_left.Invoke();
    }

    public void SwipeRight()
    {
        //trigger_attackVFX_right.Invoke();
    }

    #region Calculting Percentages
    public float CalculateStatPercentValue(float percentage, float statValue)
    {
        return (percentage / 100) * statValue;
    }

    public float ReturnValueFromPercentage(float minValue, float maxValue, float percentage)
    {
        float result = minValue + (maxValue - minValue) * percentage / 100f;

        return result;
        //return Mathf.Clamp(minValue, maxValue, result);
    }
    #endregion

    #region AOE Functions
    public float chargedSTAM = 0;
    public float chargedMELEE = 0;
    public float chargedRANGE = 0;
    IEnumerator BeginChargingAOE()
    {
        Debug.Log("Begin Powering Up AOE");

        trigger_aoeChargeVFX.Invoke();      //Enables Charging VFX

        currentCharge += pStats.aoe_ChargeRate * Time.deltaTime;

        yield return null;
    }

    public void ChargeAOE()
    {
        if (charging)
        {
            StartCoroutine(BeginChargingAOE());

            Debug.Log((int)currentCharge);

            if (currentCharge <= pStats.aoe_Hold)
            {
                #region Attempt #3 [Success]

                float chargePercentage = Mathf.InverseLerp(0, pStats.aoe_Hold, currentCharge) * 100;

                //Debug.Log($"Charge Percentage: {(float)chargePercentage}");

                //Melee
                float minMeleeMultiplier = CalculateStatPercentValue(30f, pStats.m_ATK);
                float maxMeleeMultiplier = CalculateStatPercentValue(100f, pStats.m_ATK);

                chargedMELEE = pStats.m_ATK + ReturnValueFromPercentage(minMeleeMultiplier, maxMeleeMultiplier, chargePercentage);
                Debug.Log($"Min Melee Multiplier: {minMeleeMultiplier}, Max Melee Multiplier: {maxMeleeMultiplier}");
                //Debug.Log("Charged Melee" + chargedMELEE);



                //Radius
                float minRadiusMultiplier = CalculateStatPercentValue(4f, pStats.r_ATK);
                float maxRadiusMultiplier = CalculateStatPercentValue(6f, pStats.r_ATK);

                chargedRANGE = ReturnValueFromPercentage(minRadiusMultiplier, maxRadiusMultiplier, chargePercentage);
                Debug.Log($"Min Radius Multiplier: {minRadiusMultiplier}, Max Radius Multiplier: {maxRadiusMultiplier}");
                //Debug.Log("Charged Radius" + chargedRANGE);

                #endregion

                #region Attempt #2 [Failed]
                /*float chargePercent = GetPercentage(currentCharge, pStats.aoe_Hold);

                //Melee
                chargedMELEE = AddValueFromCharge(30f, 100f, pStats.m_ATK, chargePercent);

                //Radius
                Func<float, float> percentValueFunction = GetPercentValueFunction(4f, 6f);
                float percentValue = percentValueFunction(chargePercent);*/

                #endregion

                #region Attempt #1 [Failed]
                //chargedRANGE = pStats.r_ATK * GetPercentageBetweenValues(minRadius, maxRadius, currentCharge);
                //chargedMELEE = pStats.m_ATK + (pStats.m_ATK * GetPercentageBetweenValues(30f, 100f, currentCharge));
                #endregion

                //Charge radius, damamge, stamina
                chargedSTAM = pStats.aoe_Tap * currentCharge;
                //chargedMELEE = pStats.m_ATK * currentCharge;
                //chargedRANGE = pStats.r_ATK * currentCharge;
            }
            else if (currentCharge >= pStats.aoe_Hold || chargedSTAM == pStats.stamina)
            {
                currentCharge = pStats.aoe_Hold;
                chargedSTAM = pStats.stamina;
            }
        }
        else if (!charging && currentCharge > 0.11f || chargedSTAM == pStats.stamina)
        {
            trigger_aoeChargeVFX.Invoke();      //Disables the charging VFX

            ReleaseAOE(chargedSTAM, chargedMELEE, chargedRANGE);

            Debug.Log("AOE charged release");
            Debug.Log($"Stamina: {chargedSTAM}, Melee: {chargedMELEE}, Radius: {chargedRANGE}");

            currentCharge = 0;
            chargedSTAM = 0;
            chargedMELEE = 0;
            chargedRANGE = 0;
        }
    }

    public void ReleaseAOE(float stamina, float melee, float radius)
    {
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, radius);        //Use Range Stat to define AOE Radius.
        foreach (Collider c in hits)
        {
            if (c.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemy = c.GetComponent<EnemyStats>();
                enemy.Health -= melee;        //Use Melee Stat here.
            }
        }

        //minRadius = radius;
        //UnityEngine.Debug.Log("AOE attack");

        trigger_aoeVFX.Invoke();        //Trigger AOE VFX
        pStats.UseDash((int)chargedSTAM);

        charging = false;
    }
    #endregion

    #region Sprint & Dash Functions
    public void Dash()
    {
        if (pStats.stamina > pStats.dash)
        {
            StartCoroutine(Mover(DashSpeed, DashTime, Dashdir));        //Dashing stuff


            pStats.UseDash((int)pStats.dash);
        }
    }

    //Event Required
    public IEnumerator Mover(float speed, float time, Vector3 dir)
    {
        float startTime = Time.time;

        invincible = true;

        //VFX.Dash();

        trigger_dashVFX.Invoke();

        while (Time.time < startTime + time)
        {
            playerController.controller.Move(dir * speed * Time.deltaTime);
            yield return null;
        }
        invincible = false;

        //VFX.Dash();
        trigger_dashVFX.Invoke();
    }

    //Event Required
    public void Sprint()
    {
        if (pStats.stamina >= pStats.sprint)
            playerController.playerSpeed = SprintSpeed;     //Sprinting stuff. Need to add logic to deplete stamina over time (in seconds)
        isSprinting = true;

        trigger_sprintVFX.Invoke();
    }

    //Event Required
    public void UnSprint()
    {
        playerController.playerSpeed = OriginalSpeed;   //Ensure that stamina is not being depleted anymore.
        isSprinting = false;

        trigger_sprintVFX.Invoke();
    }
    #endregion

    #region Ranged Attack Functions
    public void RangeAttack()
    {
        if (!fired)
        {
            if (!isAttacking)
            {
                //animator.SetLayerWeight(animator.GetLayerIndex("Shooting Layer"), 1);
                Rigidbody bullets = Instantiate(bullet, ProjectileOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                bullets.AddForce(ProjectileOrigin.transform.forward * bulletSpeed, ForceMode.Impulse);
                bullets.GetComponent<Destroy>().damage = pStats.r_ATK;
                bullets.GetComponent<Destroy>().playershot = true;
                fired = true;
            }
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
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < HitAreas.Length; i++)
        {
            if (HitAreas[i].enemyFound)
            {
                Color c = new Color(0f, 0, 1, 0.4f);
                //UnityEditor.Handles.color = c;
            }
            else
            {
                Color c = new Color(0.8f, 0, 0, 0.4f);
                //UnityEditor.Handles.color = c;
            }
            Vector3 rotatedForward = Quaternion.Euler(0,
             -HitAreas[i].Direction * 0.5f,
             0) * transform.forward;

            // UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, HitAreas[i].Angle, HitAreas[i].Radius);
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
