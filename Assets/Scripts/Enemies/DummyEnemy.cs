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
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
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
            animator.ResetTrigger("Hitted");
            hitted = false;
        }
    }
}
