using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{

    public List<Transform> points;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        points.Add(transform.GetChild(i).GetComponent<Transform>());
    }
}
