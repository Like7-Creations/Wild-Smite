using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateCamTarget : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if(cam.LookAt == null)
        {
            Transform target = FindObjectOfType<PlayerActions>().gameObject.transform;
            cam.LookAt = target;
            cam.Follow = target;
        }
    }
}
