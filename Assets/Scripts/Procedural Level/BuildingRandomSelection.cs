using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildingRandomSelection : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnBuilding();
    }

    public void SpawnBuilding()
    {
        if (!spawned)
        {
            spawned = true;
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        }
    }
}
