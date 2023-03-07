using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamTrackerMove : MonoBehaviour
{
    #region Target Settings
    public GameObject playerTarget;

    [SerializeField] Vector3 targetOffset;
    Vector3 targetPos;
    #endregion

    #region SplitScreen Settings
    public Dynamic_SplitScreen dSplitScreen;
    public Vector3 splitOffset;
    public bool splitScreen;
    #endregion

    #region PlayerDesignation
    bool isP1;
    bool isP2;
    #endregion

    #region Arrival Behavior Settings
    public float trackerMass = 15;
    public float tracker_MaxVel = 3;
    public float tracker_MaxForce = 15;
    public float slowRadius;

    Vector3 velocity;
    #endregion

    Vector3 Arrival()
    {
        Vector3 desiredVel = targetPos - transform.position;
        desiredVel = desiredVel.normalized * tracker_MaxVel;

        float dist = Vector3.Distance(targetPos, transform.position);

        if(dist<slowRadius)
        {
            desiredVel = desiredVel.normalized * tracker_MaxVel * (dist / slowRadius);

        }
        else
        {
            desiredVel = desiredVel.normalized * tracker_MaxVel;
        }

        Vector3 arrival = desiredVel - velocity;
        arrival = Vector3.ClampMagnitude(arrival, tracker_MaxVel);
        arrival /= trackerMass;

        velocity = Vector3.ClampMagnitude(velocity + arrival, tracker_MaxVel);

        return velocity * Time.deltaTime;
    }

    #region Standard Functions
    void Start()
    {
        velocity = Vector3.zero;

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

        targetPos = playerTarget.transform.position - (-splitOffset + targetOffset);

        transform.position += Arrival();

        #region Failed Attempts
        /*if(Vector3.Distance(transform.position, target.transform.position) > maxDistFromTarget)
        {
            PlayerStats stats = target.GetComponent<PlayerStats>();

            trackerAcceleration = 
        }

        transform.position = Vector3.Lerp(transform.position, 
                                        target.transform.position - (-splitOffset + targetOffset),
                                        baseTrackerSpeed * Time.deltaTime);*/
        #endregion
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1f);
    }
    #endregion
}