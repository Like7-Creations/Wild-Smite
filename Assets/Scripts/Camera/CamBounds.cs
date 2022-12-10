using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]
public class CamBounds : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera currentCam;
    [SerializeField] Vector3 colSize;

    BoxCollider boxCol;
    Rigidbody rb;

    void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.isTrigger = true;
        boxCol.size = colSize;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            if (CameraSwitcher.ActiveCam != currentCam)
                CameraSwitcher.SwitchCam(currentCam);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, colSize);
    }
}
