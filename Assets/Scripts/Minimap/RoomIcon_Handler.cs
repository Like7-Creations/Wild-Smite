using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelGenerator.Scripts;
using System;
//using Unity.PlasticSCM.Editor.WebApi;


//DEPRECATED [DELETE LATER]
public class RoomIcon_Handler : MonoBehaviour
{
    #region Current Room Stuff
    [SerializeField] SpriteRenderer roomIcon;
    [SerializeField] BoxCollider iconCollider;

    //Current Room Colors
    [SerializeField] Color currentColor;

    public bool currentRoom;
    #endregion

    #region Standard Settings
    //Keeps track of all adjacent rooms that have been explored.
    public List<RoomIcon_Handler> exploredRooms;

    public enum RoomStatus { Current, Adjacent, None }
    public RoomStatus roomStatus;

    public bool exploredRoom = false;
    #endregion


    #region Adjacent Room Stuff
    //Keeps track of all adjacent rooms.
    public List<RoomIcon_Handler> adjacentRooms;

    //Room Colors
    [SerializeField] Color unexploredColor;
    [SerializeField] Color exploredColor;

    public bool adjacentRoom;
    #endregion

    #region Raycast stuff
    Ray ray;

    [SerializeField] float rayMagnitude = 29.78f;
    [SerializeField] LayerMask minimapLayer;

    [SerializeField] bool roomsChecked = false;
    #endregion

    void Start()
    {
        name = transform.parent.transform.parent.name;

        roomIcon = transform.parent.GetComponent<SpriteRenderer>();
        iconCollider = GetComponent<BoxCollider>();

        adjacentRooms = new List<RoomIcon_Handler>(4);
        exploredRooms = new List<RoomIcon_Handler>();

        /*if (transform.GetSiblingIndex() == 0)
        {
            roomStatus = RoomStatus.Current;
        }
        else
        {
            roomStatus = RoomStatus.None;
        }*/

        if (!exploredRoom)
        {
            for (int i = 0; i < adjacentRooms.Count; i++)
            {
                if (adjacentRooms[i].roomStatus == RoomStatus.Current)
                    roomIcon.enabled = true;
            }
        }

        if (!roomsChecked)
        {
            DetectAdjacentRooms();

            foreach (RoomIcon_Handler room in adjacentRooms)
            {
                room.roomStatus = RoomStatus.Adjacent;
            }

            roomsChecked = true;
        }
    }

    void Update()
    {
        UpdateRoomStatus();
    }

    public void UpdateRoomStatus()
    {
        switch (roomStatus)
        {
            case RoomStatus.Current:
                {
                    SetCurrentRoomState();

                    break;
                }

            case RoomStatus.Adjacent:
                {
                    SetAdjacentRoomState();

                    break;
                }

            case RoomStatus.None:       //Unexplored
                {
                    //Check if any of the rooms in the AdjacentRooms list is set as a Current Room.
                    if (adjacentRooms != null)
                    {
                        RoomIcon_Handler temp;

                        for (int i = 0; i < adjacentRooms.Count; i++)
                        {
                            if (adjacentRooms[i].roomStatus == RoomStatus.Current)
                            {
                                temp = adjacentRooms[i];

                                //If there is one, change this back to Adjacent.
                                roomStatus = RoomStatus.Adjacent;
                                return;
                            }

                            //If there aren't any, check if this room has been explored.
                            else
                            {
                                //If true, exit the function.
                                if (exploredRoom)
                                    return;

                                //If false, disable the icon.
                                else if (!exploredRoom)
                                    roomIcon.enabled = false;
                            }
                        }
                    }

                    break;
                }
        }
    }

    void SetCurrentRoomState()
    {
        if (roomStatus == RoomStatus.Current)
        {
            //Mark this room as explored.
            exploredRoom = true;

            //Change icon's color to currentColor
            roomIcon.color = currentColor;

            //Enable the icon if its disabled
            if (!roomIcon.enabled)
                roomIcon.enabled = true;

            //Check if the AdjacentRooms list is empty or not
            if (adjacentRooms != null || exploredRooms != null)
            {
                //If not empty, access their version of this script, and set them to Adjacent Room.
                foreach (RoomIcon_Handler room in adjacentRooms)
                {
                    if (room.roomStatus != RoomStatus.Adjacent)
                        room.roomStatus = RoomStatus.Adjacent;
                }
            }
            else if (adjacentRooms == null && exploredRooms == null)
            {
                //If empty, locate all adjacent rooms, access their version of this script, and set them to Adjacent Room.
                DetectAdjacentRooms();

                foreach (RoomIcon_Handler room in adjacentRooms)
                {
                    if (room.roomStatus != RoomStatus.Adjacent)
                        room.roomStatus = RoomStatus.Adjacent;
                }
            }
        }
    }


    //Comment out. 
    void SetAdjacentRoomState()
    {
        if (roomStatus == RoomStatus.Adjacent)
        {
            //Check if the room has been explored or not
            if (!exploredRoom)
            {
                //If unexplored, change icon's color to unexplored color
                roomIcon.color = unexploredColor;
                //Enable the icon.
                roomIcon.enabled = true;
            }
            else if (exploredRoom)
            {
                if (currentRoom)
                    currentRoom = false;

                //If explored, change icon's color to explored color
                roomIcon.color = exploredColor;

                //Ensure that the icon is enabled at all times.
                if (!roomIcon.enabled)
                    roomIcon.enabled = true;

                //If the AdjacentRooms list has rooms in it, access their version of this script, and add this room to the ExploredRooms list after removing it from their AdjacentRooms list.
                if (adjacentRooms != null)
                {
                    foreach (RoomIcon_Handler room in adjacentRooms)
                    {
                        if (!room.exploredRooms.Contains(this))
                            room.exploredRooms.Add(this);
                        room.adjacentRooms.Remove(this);
                    }
                }
            }
        }
    }

    void DetectAdjacentRooms()
    {
        RaycastHit raycastHit;

        for (int i = 0; i < 4; i++)
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

            if (Physics.Raycast(ray, out raycastHit, rayMagnitude, minimapLayer))
            {
                RoomIcon_Handler temp = raycastHit.collider.gameObject.GetComponent<RoomIcon_Handler>();

                if (!temp.exploredRoom)
                    adjacentRooms.Add(temp);
                else if (temp.exploredRoom)
                    exploredRooms.Add(temp);


                //.transform.parent.transform.parent.GetComponent<Section>()
                //Add back if changes dont work
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (roomStatus == RoomStatus.Adjacent)
            if (other.CompareTag("Player"))
                roomStatus = RoomStatus.Current;
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            if (roomStatus != RoomStatus.Current)
            {
                roomStatus = RoomStatus.Current;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roomStatus == RoomStatus.Current)
            if (other.CompareTag("Player"))
                roomStatus = RoomStatus.Adjacent;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, new Vector3(0, 0, rayMagnitude));

        //Gizmos.DrawCube(transform.position + new Vector3(29.78f, 0, 0), new Vector3(5, 5, 5));


    }

    #region Failed Attempt 1
    //Call in one of the three Update functions, so that the adjacent and explored room lists are always upto date.
    void UpdateRooms1()
    {
        if (adjacentRoom)
        {
            for (int i = 0; i < adjacentRooms.Count; i++)
            {
                if (adjacentRooms[i].currentRoom)
                {
                    roomIcon.enabled = true;
                    break;
                }
                else if (!adjacentRooms[i].currentRoom)
                {
                    roomIcon.enabled = false;
                }
            }
        }
    }

    //Call when player collider hits RoomCollider
    public void SetCurrentRoom()
    {
        //Marks the room as the current one and as an explored room.
        currentRoom = true;
        exploredRoom = true;
        adjacentRoom = false;

        if (!roomIcon.enabled)
            //Ensures the room Icon is enabled.
            roomIcon.enabled = true;

        //Sets the icon's color to the currentColor.
        roomIcon.color = currentColor;

        //Loops through all adjacent rooms, checks if they're null before marking them as adjacent rooms and setting their color to unexploredColor.
        for (int i = 0; i < adjacentRooms.Count; i++)
        {
            if (adjacentRooms[i] != null)
            {
                adjacentRooms[i].roomIcon.color = unexploredColor;
                adjacentRooms[i].adjacentRoom = true;
            }
        }
    }

    //Call once player's collider exits the RoomCollider
    public void SetExploredRoom()
    {
        currentRoom = false;
        //adjacentRoom = true;
        //Loops through all its adjacent rooms, so that it can access their adjacentRoom lists.
        for (int i = 0; i < adjacentRooms.Count; i++)
        {
            //Finds itself in each of their lists and removes itself from it.
            adjacentRooms[i].adjacentRooms.Remove(this);
            //Adds itself to their exploredRooms lists.
            adjacentRooms[i].exploredRooms.Add(this);
        }

        roomIcon.color = exploredColor;
    }
    #endregion
}
