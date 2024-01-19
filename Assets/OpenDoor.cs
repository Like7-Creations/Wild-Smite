using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.Play("door_1_open");
        }
    }
}
