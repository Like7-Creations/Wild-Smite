using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item[] items;

    [SerializeField] Transform origin;

    void Start()
    {
        Instantiate(items[Random.Range(0, items.Length)], origin.position, Quaternion.identity);
    }
}
