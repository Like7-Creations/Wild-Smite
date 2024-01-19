using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEdgeCollider : MonoBehaviour
{
    public BoxCollider col;

    public bool spawnFillRoom;
    public Transform forwardFillSpawn;
    public Transform leftFillSpawn;
    public Transform rightFillSpawn;

    public bool fillRoad;
    public GameObject fillPrefab;
    public GameObject fillRoadPrefab;

    public AdjacentChecker forwardCheck;
    public AdjacentChecker CornerLeftCheck;
    public AdjacentChecker CornerRightCheck;

    bool checkedForward;
    bool checkedRight;
    bool checkedLeft;

    bool colliderHidden;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();

        if (forwardCheck.RoomCheck() && !spawnFillRoom)
            HideCollider();
    }

    public void HideCollider()
    {
        //Destroy(gameObject);
        //col.enabled = false;
        if (forwardCheck.RoomCheck())
        {
            col.enabled = false;
            colliderHidden = true;
        }
    }

    public void RunForwardChecks()
    {
        if (spawnFillRoom)
        {
            if (forwardCheck.PreSpawnCheck())
            {
                //if (col.enabled)
                //    col.enabled = false;

                if (fillRoad)
                    Instantiate(fillRoadPrefab, forwardFillSpawn);
                else
                    Instantiate(fillPrefab, forwardFillSpawn);
            }
        }
        checkedForward = true;

    }

    public void RunLeftChecks()
    {
        if (spawnFillRoom)
        {
            if (CornerLeftCheck.PreSpawnCheck())
                Instantiate(fillPrefab, leftFillSpawn);
        }
        checkedLeft = true;
    }

    public void RunRightChecks()
    {
        if (spawnFillRoom)
        {
            if (CornerRightCheck.PreSpawnCheck())
                Instantiate(fillPrefab, rightFillSpawn);
        }
        checkedRight = true;
    }

    public void OnDrawGizmos()
    {
        if (col != null)
        {
            Gizmos.color = Color.green;
            if (!col.enabled)
                Gizmos.color = Color.red;

            if (col.enabled)
                Gizmos.DrawWireCube(transform.position + col.center, col.size);
        }
    }
}
