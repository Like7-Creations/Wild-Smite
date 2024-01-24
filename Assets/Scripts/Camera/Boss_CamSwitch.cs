using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss_CamSwitch : MonoBehaviour
{
    bool arenaCam = false;

    void Awake()
    {
        //bossCamAnimator = GetComponent<Animator>();
    }

    public void SwitchBossCamState()
    {
        if (!arenaCam)
        {
            Transform hallCam = gameObject.transform.GetChild(0);
            hallCam.gameObject.SetActive(false);
        }

        arenaCam = true;
    }

    void OnTriggerEnter(Collider other)
    {
        SwitchBossCamState();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
