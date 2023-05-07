using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentControls : MonoBehaviour
{

    public RoomEdgeCollider[] RoomColliders;

    public void RunForwardChecks()
    {
        for (int i = 0; i < RoomColliders.Length; i++)
            RoomColliders[i].RunForwardChecks();
    }

    public void RunLeftChecks()
    {
        for (int i = 0; i < RoomColliders.Length; i++)
            RoomColliders[i].RunLeftChecks();
    }

    public void RunRightChecks()
    {
        for (int i = 0; i < RoomColliders.Length; i++)
            RoomColliders[i].RunRightChecks();
    }

    public void CheckToHide()
    {
        for (int i = 0; i < RoomColliders.Length; i++)
            RoomColliders[i].HideCollider();
    }
}
