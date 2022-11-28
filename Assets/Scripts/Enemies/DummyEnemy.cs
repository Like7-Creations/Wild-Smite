using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummyEnemy : MonoBehaviour
{
    PlayerController player;
    Animator animator;
    public bool hitted;
    [SerializeField] float dist;
    public float health;
    CharacterController controller;
    Vector3 movement;

    public MeleeEnemy meleestats;
    public RangeEnemy rangestats;
    [SerializeField] float currentDamage;
    [SerializeField] float level;
    [SerializeField] float CurrentLevel;

    public enum enemyType
    {
        range, melee
    }

    public enemyType enemyTypelol;

    public TextMeshProUGUI hitText;
    DisplayStats displayStats;

    public float hitPointDelay;

    public ParticleSystem hiteffect = null;

    FlashDamage fD;


   // private Material whiteMat;
   // private Material defaultMat;

    //SkinnedMeshRenderer smr;


    void Start()
    {
        CurrentLevel = 1;
        CurrentLevel = meleestats.Level;
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        fD = FindObjectOfType<FlashDamage>();
        controller = GetComponent<CharacterController>();
        displayStats = FindObjectOfType<DisplayStats>();
        hitText.enabled = false;
        //currentDamage = Random.Range(meleestats.min_meleeDMG, meleestats.max_meleeDMG);
        Debug.Log(currentDamage);
       // ScaleStats();
        Debug.Log(currentDamage);

        meleestats.cur_Health = meleestats.Health;
        meleestats.cur_meleeDMG = meleestats.meleeDMG;
        meleestats.cur_meleeDEF = meleestats.meleeDEF;
        meleestats.cur_meleeCD = meleestats.meleeCD;
        meleestats.cur_moveSpeed = meleestats.moveSpeed;
       // smr = GetComponent<SkinnedMeshRenderer>();

        // whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        //defaultMat = smr.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentLevel != meleestats.Level)
        {
            ScaleStats();
            CurrentLevel = meleestats.Level;
        }
        if(health <= 0)
        {
            Destroy(transform.gameObject);
        }
        transform.LookAt(player.transform);
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (controller.isGrounded)
        {
            movement.y -= 9.81f;
        }
        controller.Move(movement * Time.deltaTime);
        if(dist < 2)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }

        if (hitted)
        {
            animator.SetTrigger("Hitted");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Reaction Hit"))
        {
            TakeDamage();
        }

        //call GetDamaged Func from DisplayStats script whereever the enemy deals damage//
    }

     void LateUpdate() // to make Hitpoint text UI look at camera at all times, runs in late update so it happens after everything else is done.
     {
        hitText.transform.LookAt(hitText.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
     }

    /*  void ResetMaterial()
      {
          smr.material = defaultMat;
      }*/
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            health -= displayStats.rangedAtk; 
        }
    }

    void ScaleStats()
    {
        if(enemyTypelol == enemyType.melee)
        {
            //health
            meleestats.cur_Health.x = meleestats.cur_Health.x * meleestats.hpMultiplier;
            meleestats.cur_Health.y = meleestats.cur_Health.y * meleestats.hpMultiplier;
            meleestats.cur_Health.x = Mathf.RoundToInt(meleestats.cur_Health.x);
            meleestats.cur_Health.y = Mathf.RoundToInt(meleestats.cur_Health.y);
            //speed
            meleestats.cur_moveSpeed.x = meleestats.cur_moveSpeed.x * meleestats.hpMultiplier;
            meleestats.cur_moveSpeed.y = meleestats.cur_moveSpeed.y * meleestats.hpMultiplier;
            meleestats.cur_moveSpeed.x = Mathf.RoundToInt(meleestats.cur_moveSpeed.x);
            meleestats.cur_moveSpeed.y = Mathf.RoundToInt(meleestats.cur_moveSpeed.y);
            //dmg
            meleestats.cur_meleeDMG.x = meleestats.cur_meleeDMG.x * meleestats.dmgMultiplier; // round to nearest 5
            meleestats.cur_meleeDMG.y = meleestats.cur_meleeDMG.y * meleestats.dmgMultiplier;// round to nearest 5
            meleestats.cur_meleeDMG.x = Mathf.RoundToInt(meleestats.cur_meleeDMG.x);
            meleestats.cur_meleeDMG.y = Mathf.RoundToInt(meleestats.cur_meleeDMG.y);
            //resistance
            meleestats.cur_meleeDEF.x = meleestats.cur_meleeDEF.x * meleestats.resMultiplier; // round to nearest 5
            meleestats.cur_meleeDEF.y = meleestats.cur_meleeDEF.y * meleestats.resMultiplier;// round to nearest 5
            meleestats.cur_meleeDEF.x = Mathf.RoundToInt(meleestats.cur_meleeDEF.x);
            meleestats.cur_meleeDEF.y = Mathf.RoundToInt(meleestats.cur_meleeDEF.y);
            CurrentLevel = meleestats.Level;
            //currentDamage = Random.Range(meleestats.cur_meleeDMG.x, meleestats.cur_meleeDMG.y);
        }
        /*if (enemyTypelol == enemyType.range)
        {
            //health
            rangestats.min_Health = meleestats.min_Health * meleestats.hpMultiplier;
            rangestats.min_Health = Mathf.RoundToInt(meleestats.min_Health);
            //dmg
            rangestats.min_rangeDMG = rangestats.min_rangeDMG * rangestats.dmgMultiplier; // round to nearest 5
           // rangestats.max_rangeDMG = rangestats.min_rangeDMG * rangestats.dmgMultiplier;// round to nearest 5
            //rangestats.min_rangeDMG = Mathf.RoundToInt(rangestats.min_rangeDMG);
            rangestats.max_rangeDMG = Mathf.RoundToInt(rangestats.max_rangeDMG);
            //resis
            rangestats.min_rangeDEF = rangestats.min_rangeDEF * rangestats.resMultiplier; // round to nearest 5
            rangestats.max_rangeDEF = rangestats.max_rangeDEF * rangestats.resMultiplier;// round to nearest 5
            rangestats.min_rangeDEF = Mathf.RoundToInt(rangestats.min_rangeDEF);
            rangestats.max_rangeDEF = Mathf.RoundToInt(rangestats.max_rangeDEF);
            //currentDamage = Random.Range(rangestats.min_rangeDMG, rangestats.max_rangeDMG);
        }*/
    }
    IEnumerator DisplayHitPoint(float hitpoint, float hitPointDelay)
    {
        hitText.text = hitpoint.ToString();
        hitText.enabled = true;
        yield return new WaitForSeconds(hitPointDelay);
        hitText.enabled = false;
    }

    public void TakeDamage()
    {
        Debug.Log("I GOT HIT");

        StartCoroutine(DisplayHitPoint(displayStats.meleeAtk, hitPointDelay));

        //change Material Temporarily
        fD.HitEnemy();

        //play hit effect of robot getting hit
        hiteffect.Play();

        //add sound effect of robot getting hit..

        //if ranged attack do this line
        //StartCoroutine(DisplayHitPoint(displayStats.rangedAtk, hitPointDelay));

        //health = health - 1;
        //smr.material = whiteMat;
        // Invoke("ResetMaterial", 5f);
        animator.ResetTrigger("Hitted");
        hitted = false;
    }

}
