using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomVariant : MonoBehaviour
{

    public SectionAdjacents sectionAdjacents;
    public AdjacentControls adjacentControls;

    public GameObject[] variants;

    [Range(0, 100)]
    public float SpawnChance;
    public GameObject[] SpawnLayouts;

    public GameObject itemLayout;

    private void Awake()
    {
        int index = Random.Range(0, variants.Length);
        GameObject obj = Instantiate(variants[index], transform);
        adjacentControls = obj.GetComponentInChildren<AdjacentControls>();
        if (adjacentControls != null)
            sectionAdjacents.controls = adjacentControls;

        if (SpawnLayouts.Length > 0)
        {
            //int chance = Random.Range(0, 100);
            int layout = Random.Range(0, SpawnLayouts.Length);
            Transform spawnerRoot;
            if (FindObjectOfType<Spawner>() != null)
                spawnerRoot = FindObjectOfType<Spawner>().GetComponent<Transform>();
            else
                spawnerRoot = transform;

            Instantiate(SpawnLayouts[layout], transform);

            //obj.transform.SetParent(spawnerRoot);
        }

        if (itemLayout != null)
            Instantiate(itemLayout, transform);
    }
}
