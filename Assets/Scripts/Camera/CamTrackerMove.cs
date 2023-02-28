using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamTrackerMove : MonoBehaviour
{
    public GameObject target;
    public Dynamic_SplitScreen dSplitScreen;

    public Vector3 splitOffset;

    public bool splitScreen;

    [SerializeField] Vector3 targetOffset;
    [SerializeField] float trackerSpeed;



    bool isP1;
    bool isP2;


    void Start()
    {
        dSplitScreen = FindObjectOfType<Dynamic_SplitScreen>();


        if (CompareTag("Player1"))
        {
            isP1= true;
            isP2 = false;
        }
        else if (CompareTag("Player2"))
        {
            isP2= true;
            isP1= false;
        }
        else
        {
            isP1= false;
            isP2= false;
        }
    }

    void Update()
    {
        if (isP1)
        {
            splitOffset = dSplitScreen.camTrack1;
        }
        else if (isP2)
        {
            splitOffset = dSplitScreen.camTrack2;
        }
        else
        {
            splitOffset = Vector3.zero;
        }

        transform.position = Vector3.Lerp(transform.position, 
                                        target.transform.position - (-splitOffset + targetOffset), 
                                        trackerSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}