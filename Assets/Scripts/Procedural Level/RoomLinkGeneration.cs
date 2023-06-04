using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLinkGeneration : MonoBehaviour
{
    [Header("Link Area")]
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject blockoff;
    [Range(0f, 1f)] public float linkChance;
    public float offset;

    [Header("GenerateRoom")]
    public bool RoomLink;
    //public bool Generate;
    [SerializeField] GameObject[] roomPrefabs;

    BuildingRandomSelection[] buildings;
    public bool incrementGenerate;
    public float interval;

    // Start is called before the first frame update
    void Awake()
    {
        if (RoomLink)
            GenerateRoom();
    }

    public void GenerateLink()
    {
        Vector3 pos = transform.position + (transform.forward * offset);
        if (Random.value > (1 - linkChance))
        {            
            GameObject link = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform, false);
            buildings = GetComponentsInChildren<BuildingRandomSelection>();
            link.transform.position = pos;
            
            if (incrementGenerate)
            {
                StartCoroutine(IncrementalGenerateLink());
            }
            else
            {
                for (int i = 0; i < buildings.Length; i++)
                {
                    buildings[i].SpawnBuilding();
                }
            }
        }
        else
        {
            GameObject block = Instantiate(blockoff, transform, false);
            block.transform.position = pos;
            block.GetComponentInChildren<BuildingRandomSelection>().SpawnBuilding();
        }
    }

    public void GenerateRoom()
    {
        GameObject roomObject = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], transform, false);
        RoomGeneration room = roomObject.GetComponent<RoomGeneration>();
        room.AdjustRoomOrientation(transform.position);
    }

    IEnumerator IncrementalGenerateLink()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].SpawnBuilding();
            yield return new WaitForSeconds(interval);
        }
        yield return null;
    }
}
