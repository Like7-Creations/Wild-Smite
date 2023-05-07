using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> linkPoints;
    public float roomoffset;
    public bool GenerateLinks;

    BuildingRandomSelection[] buildings;
    public bool incrementGenerate;
    public float interval;

    private void Awake()
    {
        buildings = GetComponentsInChildren<BuildingRandomSelection>();
        
        if (GenerateLinks)
        {            
            GenerateLinkAssets();
        }
    }

    public void GenerateLinkAssets()
    {
        if (incrementGenerate)
            StartCoroutine(IncrementalGenerate());
        else
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                buildings[i].SpawnBuilding();
            }

            for (int i = 0; i < linkPoints.Count; i++)
                linkPoints[i].GetComponent<RoomLinkGeneration>().GenerateLink();
        }
    }

    public Vector3 FindClosestLink(Vector3 point)
    {
        GameObject closestLink = linkPoints[0];
        for (int i = 1; i < linkPoints.Count; i++)
        {
            if (Vector3.Distance(point, linkPoints[i].transform.position) < Vector3.Distance(point, closestLink.transform.position))
            {
                closestLink = linkPoints[i];
            }
        }
        linkPoints.Remove(closestLink);
        return closestLink.transform.position;
    }

    public void AdjustRoomOrientation(Vector3 point)
    {
        Vector3 linkPoint = FindClosestLink(point);
        Vector3 directionToPoint = linkPoint - transform.position;

        transform.localPosition += new Vector3(0, 0, roomoffset);
        Quaternion rot = Quaternion.LookRotation(directionToPoint);
        transform.rotation = rot;
        transform.Rotate(new Vector3(0, 180, 0));

        if (incrementGenerate)
            StartCoroutine(IncrementalGenerate());
        else
            GenerateLinkAssets();
    }

    IEnumerator IncrementalGenerate()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].SpawnBuilding();
            yield return new WaitForSeconds(interval);
        }
        for (int i = 0; i < linkPoints.Count; i++)
        {
            linkPoints[i].GetComponent<RoomLinkGeneration>().GenerateLink();
            yield return new WaitForSeconds(interval);
        }
        yield return null;
    }
}
