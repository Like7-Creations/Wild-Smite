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
        currentDamage = Random.Range(meleestats.min_meleeDMG, meleestats.max_meleeDMG);
        Debug.Log(currentDamage);
       // ScaleStats();
        Debug.Log(currentDamage);
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
            meleestats.min_Health = meleestats.min_Health * meleestats.hpMultiplier;
            meleestats.min_Health = Mathf.RoundToInt(meleestats.min_Health);
            //dmg
            meleestats.min_meleeDMG = meleestats.min_meleeDMG * meleestats.dmgMultiplier; // round to nearest 5
            meleestats.max_meleeDMG = meleestats.max_meleeDMG * meleestats.dmgMultiplier;// round to nearest 5
            meleestats.min_meleeDMG = Mathf.RoundToInt(meleestats.min_meleeDMG);
            meleestats.max_meleeDMG = Mathf.RoundToInt(meleestats.max_meleeDMG);
            //resistance
            meleestats.min_meleeDEF = meleestats.min_meleeDEF * meleestats.resMultiplier; // round to nearest 5
            meleestats.max_meleeDEF = meleestats.max_meleeDEF * meleestats.resMultiplier;// round to nearest 5
            meleestats.min_meleeDEF = Mathf.RoundToInt(meleestats.min_meleeDEF);
            meleestats.max_meleeDEF = Mathf.RoundToInt(meleestats.max_meleeDEF);
            currentDamage = Random.Range(meleestats.min_meleeDMG, meleestats.max_meleeDMG);
        }
        if (enemyTypelol == enemyType.range)
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
        }
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
