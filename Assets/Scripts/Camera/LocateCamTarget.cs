using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateCamTarget : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineTargetGroup targetGroup;

    public bool isArenaCam;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }

    void Update()
    {
        if (isArenaCam)
        {
            if (targetGroup.m_Targets[1].target == null)
            {
                targetGroup.m_Targets[1].target = FindObjectOfType<PlayerActions>().gameObject.transform;

                targetGroup.m_Targets[1].weight = 2f;
                targetGroup.m_Targets[1].radius = 1f;
            }
        }
    }
}
