using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinion_Detector : MonoBehaviour
{
    public BoxCollider col;

    public CinemachineSmoothPath dollyTrack;

    public int closestDollyNodeIndex; //Wont work. Store index of node instead and use that.

    CinemachineSmoothPath.Waypoint[] dollyNodes;

    void Awake()
    {
        col= GetComponent<BoxCollider>();

        dollyTrack = FindObjectOfType<CinemachineSmoothPath>();

        dollyNodes = dollyTrack.m_Waypoints;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //Move the closest dolly node to a predetermined location 
        }
    }

    void Update()
    {

    }
}
