using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    PlayerController player;
    Animator animator;
    public bool hitted;
    [SerializeField] float dist;
    public float health;
    CharacterController controller;
    Vector3 movement;

   // private Material whiteMat;
   // private Material defaultMat;

    //SkinnedMeshRenderer smr;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
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
            //health = health - 1;
            //smr.material = whiteMat;
           // Invoke("ResetMaterial", 5f);
            animator.ResetTrigger("Hitted");
            hitted = false;
        }

        //call GetDamaged Func from DisplayStats script whereever the enemy deals damage//
    }

  /*  void ResetMaterial()
    {
        smr.material = defaultMat;
    }*/
}
