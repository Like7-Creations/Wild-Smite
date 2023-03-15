using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Vector3 pos { get; private set; }

    private void Awake()
    {
        pos = transform.position;
    }
}
