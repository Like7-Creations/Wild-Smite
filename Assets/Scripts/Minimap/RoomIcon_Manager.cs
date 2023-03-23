using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIcon_Manager : MonoBehaviour
{
    [SerializeField] SpriteRenderer roomIcon;
    [SerializeField] Color currentColor;

    [SerializeField] Minimap_DetectionPoint local_detectionPoint;

    public enum RoomStatus { Current, Hidden, Adjacent }
    public RoomStatus roomStatus;

    public bool exploredRoom = false;

    [SerializeField] Color unexploredColor;
    [SerializeField] Color exploredColor;
    [SerializeField] Color hiddenColor;

    [SerializeField] LayerMask minimapLayer;
    [SerializeField] float rayMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        name = transform.parent.transform.parent.name;

        roomIcon = transform.parent.GetComponent<SpriteRenderer>();
        
        roomStatus = RoomStatus.Hidden;
        UpdateRoomState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateRoomState()
    {
        switch (roomStatus)
        {
            case RoomStatus.Current:

                roomIcon.color = currentColor;
                exploredRoom = true;
                CheckAdjacentRooms();

                break;


            case RoomStatus.Hidden:

                if (exploredRoom)
                    roomIcon.color = exploredColor;
                else
                    roomIcon.color = hiddenColor;

                break;
        }
    }

    public void CheckAdjacentRooms()
    {
        //Perform Raycast in All 4 directions
        Ray ray = new Ray();

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

            if (Physics.Raycast(ray, out raycastHit, minimapLayer))
            {
                if (raycastHit.collider.CompareTag("Minimap Detection"))
                {
                    Minimap_DetectionPoint tempPoint = raycastHit.collider.GetComponent<Minimap_DetectionPoint>();

                    if (tempPoint != local_detectionPoint)
                    {
                        tempPoint.room.SetAdjacentState();
                        tempPoint.detectedRoom = this;
                    }
                }
            }

        }
        //If it hits any detection objects, based on tag.
        //It will grab that DetectionPoint script, and accesses the "room" variable and call the AdjacentState function.
    }

    public void SetAdjacentState()
    {
        roomStatus = RoomStatus.Adjacent;
        roomIcon.color = unexploredColor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            if (roomStatus != RoomStatus.Current)
            {
                roomStatus = RoomStatus.Current;
                UpdateRoomState();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roomStatus == RoomStatus.Current)
            if (other.CompareTag("Player"))
            {
                roomStatus = RoomStatus.Hidden;
                UpdateRoomState();
            }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, -1));
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left);
    }
}
