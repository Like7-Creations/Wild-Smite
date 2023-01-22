using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummyEnemy : MonoBehaviour
{
    PlayerMovement player;
    Animator animator;
    public bool hitted;
    public bool hitTest;
    [SerializeField] float dist;
    public float health;
    CharacterController controller;
    Vector3 movement;

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

    public FlashDamage fD;

    public GameObject testObject;


   // private Material whiteMat;
   // private Material defaultMat;

    //SkinnedMeshRenderer smr;


    void Start()
    {
        CurrentLevel = 1;
        //CurrentLevel = meleestats.Level;
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        fD = FindObjectOfType<FlashDamage>();
        controller = GetComponent<CharacterController>();
        displayStats = FindObjectOfType<DisplayStats>();
        hitText.enabled = false;
        //currentDamage = Random.Range(meleestats.min_meleeDMG, meleestats.max_meleeDMG);
        //Debug.Log(currentDamage);
       // ScaleStats();
        //Debug.Log(currentDamage);

        /*meleestats.cur_Health = meleestats.Health;
        meleestats.cur_meleeDMG = meleestats.meleeDMG;
        meleestats.cur_meleeDEF = meleestats.meleeDEF;
        meleestats.cur_meleeCD = meleestats.meleeCD;
        meleestats.cur_moveSpeed = meleestats.moveSpeed;*/
       // smr = GetComponent<SkinnedMeshRenderer>();

        // whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        //defaultMat = smr.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 thisobj = transform.position;
        Vector3 testobj = transform.rotation * thisobj.normalized * 3;
       /* Vector3 toPlayer = player.transform.position - transform.position;
        controller.Move(toPlayer.normalized * 3 * Time.deltaTime);*/
        ///testObject.transform.position = testobj + thisobj;
        /*if(CurrentLevel != meleestats.Level)
        {
            ScaleStats();
            CurrentLevel = meleestats.Level;
        }*/
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
            TakeDamage();
            hitTest = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Reaction Hit"))
        {
            //TakeDamage();
            animator.ResetTrigger("Hitted");
            hitTest = false;
            hitted = false;
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
            TakeDamage();
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
        //Debug.Log("I GOT HIT");

       //StartCoroutine(DisplayHitPoint(displayStats.meleeAtk, hitPointDelay));

        //change Material Temporarily

        fD.HitEnemy();

        //play hit effect of robot getting hit
        hiteffect.Play();
        //add sound effect of robot getting hit..

        //if ranged attack do this line
        StartCoroutine(DisplayHitPoint(displayStats.rangedAtk, hitPointDelay));
        /*Vector3 knockBack = transform.position - transform.forward * 0.05f;
        knockBack.y = 0;
        transform.position = knockBack;*/

        //health = health - 1;
        //smr.material = whiteMat;
        // Invoke("ResetMaterial", 5f);
        //hitTest = false;
       // animator.ResetTrigger("Hitted");
    }

}
