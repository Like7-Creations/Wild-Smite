using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelGenerator.Scripts;

public class RoomIcon_Handler : MonoBehaviour
{
    //Keeps track of all adjacent rooms.
    [SerializeField] Section[] adjacentRooms;

    //Keeps track of all adjacent rooms that have been explored.
    [SerializeField] List<Section> exploredRooms;


    //Raycast stuff
    Ray ray;

    [SerializeField] float rayMagnitude = 29.78f;
    [SerializeField] LayerMask minimapLayer;

    bool roomsChecked = false;

    void Start()
    {
        adjacentRooms = new Section[4];
        exploredRooms = new List<Section>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!roomsChecked)
        {
            DetectAdjacentRooms();
            roomsChecked = true;
        }   
    }

    void DetectAdjacentRooms()
    {
        //ray = new Ray(transform.position, new Vector3(0, 0, 29.78f));

        RaycastHit raycastHit;

        for (int i = 0; i < adjacentRooms.Length; i++)
        {
            if (i == 0)
            {
                ray = new Ray(transform.position, Vector3.forward);
            }
            else if (i == 1)
            {
                ray = new Ray(transform.position, Vector3.right);

            }
            else if (i == 2)
            {
                ray = new Ray(transform.position, new Vector3(0, 0, -1));

            }
            else if (i == 3)
            {
                ray = new Ray(transform.position, Vector3.left);

            }

            if(Physics.Raycast(ray, out raycastHit, rayMagnitude, minimapLayer))
            {
                adjacentRooms[i] = raycastHit.transform.parent.transform.parent.GetComponent<Section>();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(ray.origin, new Vector3(0, 0, rayMagnitude));

        //Gizmos.DrawCube(transform.position + new Vector3(29.78f, 0, 0), new Vector3(5, 5, 5));
    }
}
