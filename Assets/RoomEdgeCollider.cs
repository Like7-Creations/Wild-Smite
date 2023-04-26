using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEdgeCollider : MonoBehaviour
{

    bool colliding;
    BoxCollider col;
    public List<Collider> neighbors;

    public enum direction
    {
        Xpos,
        Xneg,
        Zpos,
        Zneg,
    }

    Vector3 origin;
    Vector3 dir;
    public bool showDir;

    public direction rayDir;
    public LayerMask ignorLayers;
    public float raycastDistance = 1.0f; // the maximum distance to check for the floor

    private void Awake()
    {
        //col = GetComponent<BoxCollider>();
        //if (neighbors.Contains(col))
        //    neighbors.Remove(col);

        //origin = new Vector3(0, 1, 0);
        //dir = Vector3.zero;
        //switch (rayDir)
        //{
        //    case direction.Xpos:
        //        dir = Quaternion.Euler(0f, 0, 60) * -transform.up;
        //        break;
        //    case direction.Zpos:
        //        dir = Quaternion.Euler(-60, 0, 0) * -transform.up;
        //        break;
        //    case direction.Xneg:
        //        dir = Quaternion.Euler(0, 0, -60) * -transform.up;
        //        break;
        //    case direction.Zneg:
        //        dir = Quaternion.Euler(60, 0, 0) * -transform.up;
        //        break;
        //}
    }

    public void Touching()
    {
        colliding = true;
    }

    private void Update()
    {
        //Vector3 raycastDirection = Quaternion.Euler(45.0f, 0.0f, 0.0f) * -transform.up;

        // Perform the raycast to check for the floor
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + origin, dir, out hit, raycastDistance, ~ignorLayers))
        //{
        //    // A floor was detected
        //    Debug.Log("Floor detected!");
        //    colliding = true;
        //    HideCollider();
        //}
        //else
        //{
        //    // No floor was detected
        //    Debug.Log("No floor detected.");
        //    colliding = false;
        //}
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("BarrierCollision");
    //    if (!neighbors.Contains(collision.collider))
    //    {
    //        Destroy(collision.gameObject);
    //        HideCollider();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("BarrierCollision");
    //    if (!neighbors.Contains(other))
    //    {
    //        Destroy(other.gameObject);
    //        HideCollider();
    //    }
    //}

    void HideCollider()
    {
        Destroy(gameObject);
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    if (colliding)
    //        Gizmos.color = Color.red;

    //    Gizmos.matrix = this.transform.localToWorldMatrix;

    //    if (col != null)
    //    {
    //        Gizmos.DrawWireCube(Vector3.zero, col.size);
    //    }


    //}
}
