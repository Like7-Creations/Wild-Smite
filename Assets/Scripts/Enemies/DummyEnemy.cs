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
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        fD = FindObjectOfType<FlashDamage>();
        controller = GetComponent<CharacterController>();
        displayStats = FindObjectOfType<DisplayStats>();

        hitText.enabled = false;
       // smr = GetComponent<SkinnedMeshRenderer>();

        // whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        //defaultMat = smr.material;
    }

    // Update is called once per frame
    void Update()
    {
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
            health -= displayStats.stat.currentRanged;
        }
    }

    IEnumerator DisplayHitPoint(float hitpoint, float hitPointDelay)
    {
        hitText.text = hitpoint.ToString();
        hitText.enabled = true;
        yield return new WaitForSeconds(hitPointDelay);
        hitText.enabled = false;
    }



}
