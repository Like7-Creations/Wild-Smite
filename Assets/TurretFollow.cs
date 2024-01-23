using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float speed;

    void Start()
    {
        StartCoroutine(UnParent());
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position, speed * Time.deltaTime);
    }

    IEnumerator UnParent() 
    {
        yield return new WaitForSeconds(0.1f);
        transform.parent = null;
    }
}
