using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDithering : MonoBehaviour
{
    [SerializeField] Camera[] cams;

    [SerializeField] Material[] PrefabMats;
    MeshRenderer renderer;

    [SerializeField] float distFromCam;

    [SerializeField] float ditherDist;      //Default around 15 maybe

    public float ditherPercentage;

    private void Awake()
    {
        cams = FindObjectsOfType<Camera>();
    }

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        PrefabMats = renderer.materials;
    }

    public float GetPercentage(float value, float maxValue)
    {
        // Calculate the percentage using the formula: (1 - value / maxValue) * 100
        float percentage = 1 - (1 - value / maxValue);

        // Return the percentage
        return percentage;
    }

    float LocateClosestCam()
    {
        Camera closestCamera = null;
        float minDistance = Mathf.Infinity;

        // Loop through all cameras and calculate the distance to the target object
        foreach (Camera camera in cams)
        {
            float distance = Vector3.Distance(camera.transform.position, transform.position);

            // If this camera is closer than the current closest camera, update the closest camera and the minimum distance
            if (distance < minDistance)
            {
                closestCamera = camera;
                minDistance = distance;
            }
        }

        return minDistance;
    }

    // Update is called once per frame
    void Update()
    {

        distFromCam = LocateClosestCam();

        //distFromCam = Mathf.Clamp(distFromCam, 0f, 1f);

        ditherPercentage = 1;

        if (distFromCam < ditherDist)
        {
            ditherPercentage = GetPercentage(distFromCam, ditherDist);
        }

        foreach (var mat in PrefabMats)
        {
            mat.SetFloat("_DitherThreshold", ditherPercentage);
        }
    }
}
