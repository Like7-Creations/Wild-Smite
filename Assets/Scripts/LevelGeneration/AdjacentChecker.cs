using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentChecker : MonoBehaviour
{

    BoxCollider boxCollider;
    bool touchingRoom;

    public LayerMask roomLayer;
    public LayerMask fillLayer;

    //public RoomEdgeCollider[] connectedColliders;
    float delayedTimer;
    bool hasChecked;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        delayedTimer = 0;
    }

    private void Update()
    {
        //if (!hasChecked)
        //{
        //    delayedTimer += Time.deltaTime;

        //    if (delayedTimer > 5)
        //    {
        //        touchingRoom = Physics.CheckBox(transform.TransformPoint(boxCollider.center), boxCollider.size / 2f, transform.rotation);

        //        //if (touchingRoom)
        //        //    for (int i = 0; i < connectedColliders.Length; i++)
        //        //        connectedColliders[i].HideCollider();
        //        //else
        //        //    for (int i = 0; i < connectedColliders.Length; i++)
        //        //        connectedColliders[i].NoAdjacentRoom();

        //        //Destroy(gameObject);
        //        hasChecked = true;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        touchingRoom = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!touchingRoom)
            touchingRoom = true;
    }

    public bool RoomCheck()
    {
        return Physics.CheckBox(transform.TransformPoint(boxCollider.center), boxCollider.size / 2f, transform.rotation, roomLayer);
    }

    public bool PreSpawnCheck()
    {
        return (!Physics.CheckBox(transform.TransformPoint(boxCollider.center), boxCollider.size / 2f, transform.rotation, fillLayer) && !RoomCheck());
    }

    public void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            bool isTouchingRoom = Physics.CheckBox(transform.TransformPoint(boxCollider.center), boxCollider.size / 2f, transform.rotation, roomLayer);
            bool isTouchingFill = Physics.CheckBox(transform.TransformPoint(boxCollider.center), boxCollider.size / 2f, transform.rotation, fillLayer);

            if (isTouchingRoom)
                Gizmos.color = Color.red;
            else if (isTouchingFill)
                Gizmos.color = Color.yellow;
            else
                Gizmos.color = Color.green;


            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}
