using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomVariant : MonoBehaviour
{

    public GameObject[] variants;

    [Range(0, 100)]
    public float SpawnChance;
    public GameObject[] SpawnLayouts;

    public GameObject itemLayout;

    private void Awake()
    {
        int index = Random.Range(0, variants.Length);
        Instantiate(variants[index], transform);

        if (SpawnLayouts.Length > 0)
        { 
            //int chance = Random.Range(0, 100);
            int layout = Random.Range(0, SpawnLayouts.Length);
            Transform spawnerRoot;
            if (FindObjectOfType<Spawner>() != null)
                spawnerRoot = FindObjectOfType<Spawner>().GetComponent<Transform>();
            else
                spawnerRoot = transform;

            GameObject obj = Instantiate(SpawnLayouts[layout], transform);
            //obj.transform.SetParent(spawnerRoot);
        }

        GameObject itemobj = Instantiate(itemLayout, transform);
    }
}
