using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class CamTrackerMove : MonoBehaviour
{
    #region Target Settings
    public GameObject playerTarget;
    [SerializeField] PlayerActions pActions;

    [SerializeField] Vector3 targetOffset;
    Vector3 targetPos;

    public List<Transform> enemyPositions;
    [SerializeField] float enemyMinDist;
    #endregion

    #region Camera Settings
    public CamHandler camHandler;
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

    Vector3 IdentifyTargetPos()
    {
        //Grab a list of enemies from the PlayerActions script that are within range of the player.
        foreach (EnemyStats enemy in pActions.enemiesInDot)
        {
            float targetDist = Vector3.Distance(enemy.transform.position, playerTarget.transform.position);
            if (targetDist <= enemyMinDist)
            {
                if (enemyPositions.Count <= 2)
                    //Select the two closest enemies from the player and add them to a new list.
                    enemyPositions.Add(enemy.transform);
                else
                    break;
            }
        }

        //Idenitfy the midpoint between the two enemies and the targetPlayer.
        Vector3 midpoint = new Vector3((playerTarget.transform.position.x + enemyPositions[0].position.x + enemyPositions[1].position.x) / 3,
            (playerTarget.transform.position.y + enemyPositions[0].position.y + enemyPositions[1].position.y) / 3,
            (playerTarget.transform.position.z + enemyPositions[0].position.z + enemyPositions[1].position.z) / 3);

        //Return the resulting Vector3 after subtracting the difference between the splitOffset and targetOffset from it.
        return midpoint - (-splitOffset);
    }

    Vector3 Arrival()
    {
        Vector3 desiredVel = targetPos - transform.position;
        desiredVel = desiredVel.normalized * tracker_MaxVel;
        float dist = Vector3.Distance(targetPos, transform.position);
        if (dist < slowRadius)
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
    
    Vector3 LocatePlayerMidpoint()
    {
        Vector3 midpoint = new Vector3((playerTarget.transform.position.x + dSplitScreen.player2.position.x) / 2, 
            (playerTarget.transform.position.y + dSplitScreen.player2.position.y) / 2, 
            (playerTarget.transform.position.z + dSplitScreen.player2.position.z) / 2);

        return midpoint;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1f);
    }
    
    #region Standard Functions
    void Start()
    {
        velocity = Vector3.zero;

        camHandler = GetComponent<CamHandler>();
        dSplitScreen = FindObjectOfType<Dynamic_SplitScreen>();


        if (CompareTag("Player1"))
        {
            isP1 = true;
            isP2 = false;
        }
        else if (CompareTag("Player2"))
        {
            isP2 = true;
            isP1 = false;
        }
        else
        {
            isP1 = false;
            isP2 = false;
        }
    }

    void Update()
    {
        if (playerTarget != null)
        {
            pActions = playerTarget.GetComponent<PlayerActions>();
        }

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

        //Comment out for now
        #region Cam Switching Logic     
        /*//Check if there are any enemies within range
        if (pActions.enemiesInDot.Count >= 1)
        {
            //if True

            targetPos = IdentifyTargetPos();
            camHandler.SwitchVirtualCamera();
        }
        else
        {
            //Return the Vector3 after subtracting the difference between the splitOffset and targetOffset from the targetPlayer's Pos.
            targetPos = playerTarget.transform.position - (-splitOffset + targetOffset);
            camHandler.SwitchVirtualCamera();
        }*/
        #endregion

        //if (dSplitScreen.isSplit)
        //{
        //targetPos = playerTarget.transform.position - (-splitOffset + targetOffset);
        //}
        //else if (!dSplitScreen.isSplit)
        //{
        //    targetPos = LocatePlayerMidpoint();
        //}
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
    #endregion
}