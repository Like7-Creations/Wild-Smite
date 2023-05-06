using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CamSwitch : MonoBehaviour
{
    public Animator bossCamAnimator;

    bool arenaCam = false;

    void Awake()
    {
        //bossCamAnimator = GetComponent<Animator>();
    }

    public void SwitchBossCamState()
    {
        if (!arenaCam)
        {
            bossCamAnimator.Play("Arena_DollyCam");
        }

        arenaCam = true;
    }

    void OnTriggerEnter(Collider other)
    {
        SwitchBossCamState();
        gameObject.SetActive(false);
    }
}
