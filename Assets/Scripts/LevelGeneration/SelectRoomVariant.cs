using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomVariant : MonoBehaviour
{

    public GameObject[] variants;

    private void Awake()
    {
        int index = Random.Range(0, variants.Length);
        Instantiate(variants[index], transform);
    }
}
