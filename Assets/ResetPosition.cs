using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{

    public Transform resetPos;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = resetPos.position;
    }
}
