using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Cam : MonoBehaviour
{
    Camera miniCam;

    InitialiseLevel lvlInitializer;
    LevelGenerator.Scripts.LevelGenerator lvlGenerator;

    Bounds lvlBounds;

    // Start is called before the first frame update
    void Start()
    {
        miniCam = GetComponent<Camera>();

        lvlInitializer = FindObjectOfType<InitialiseLevel>();

        if (lvlInitializer != null)
        {
            Debug.Log("Level Initializer Located");
        }
        else
            Debug.Log("Level Initializer Not Found");


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(lvlBounds.center, (lvlBounds.extents * 4) + new Vector3(36.5f, 0, 36.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlInitializer.levelSpawned)
        {
            lvlGenerator = lvlInitializer.GetComponentInChildren<LevelGenerator.Scripts.LevelGenerator>();
            Debug.Log("Level Generator Located");
        }
        else
        {
            Debug.Log("Generator Missing!");
        }

        lvlBounds = new Bounds(lvlInitializer.transform.position, Vector3.one);

        LevelGenerator.Scripts.Section[] rooms = lvlGenerator.GetComponentsInChildren<LevelGenerator.Scripts.Section>();

        if (rooms.Length > 0)
        {
            foreach (LevelGenerator.Scripts.Section room in rooms)
            {
                lvlBounds.Encapsulate(room.transform.position);
            }
        }

        miniCam.orthographicSize = (lvlBounds.extents.z + 67f) /* 2.5f*/;

        transform.position = new Vector3(lvlBounds.center.x, 125, lvlBounds.center.z);
    }
}
