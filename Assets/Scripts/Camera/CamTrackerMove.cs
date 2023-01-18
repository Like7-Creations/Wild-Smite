using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamTrackerMove : MonoBehaviour
{
    public GameObject target;

    [SerializeField] Vector3 targetOffset;
    [SerializeField] float trackerSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                        target.transform.position - targetOffset, 
                                        trackerSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
