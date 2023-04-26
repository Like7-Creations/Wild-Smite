using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Cam : MonoBehaviour
{
    Camera miniCam;

    InitialiseLevel lvlInitializer;
    LevelGenerator.Scripts.LevelGenerator lvlGenerator;

    Bounds lvlBounds;

    public bool showFullMap = false;

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

    void Update()
    {
        //Check if the level has spawned.
        if (lvlInitializer.levelSpawned)        //Add a null check to prevent it from running repeatedly
        {
            if (lvlGenerator == null)
            {
                lvlGenerator = lvlInitializer.GetComponentInChildren<LevelGenerator.Scripts.LevelGenerator>();
                //Debug.Log("Level Generator Located");
            }
        }
        else
        {
            Debug.Log("Generator Missing!");
        }

        if (showFullMap)
        {
            //Create a new bounds obj.
            lvlBounds = new Bounds(lvlInitializer.transform.position, Vector3.one);

            //Locate and store an array of all the rooms in a level.
            LevelGenerator.Scripts.Section[] rooms = lvlGenerator.GetComponentsInChildren<LevelGenerator.Scripts.Section>();

            //If the list of rooms isn't empty, loop through each of them and encapsulate them within the Bounds obj.
            if (rooms.Length > 0)
            {
                foreach (LevelGenerator.Scripts.Section room in rooms)
                {
                    lvlBounds.Encapsulate(room.transform.position);
                }
            }

            //Set the miniCam's orthoSize to be equal to the z value of the Bounds.extent with an additional + 67f.
            miniCam.orthographicSize = (lvlBounds.extents.z + 67f) /* 2.5f*/;

            //Set the camera's position to the center of the bounds obj
            transform.position = new Vector3(lvlBounds.center.x, 125, lvlBounds.center.z);
        }
        else
        {
            //Locate all existing players.
            PlayerActions[] players = FindObjectsOfType<PlayerActions>();

            //Create a new Bounds obj.
            lvlBounds = new Bounds();

            if (players.Length == 1)
            {
                lvlBounds.center = players[0].transform.position;
            }
            else if (players.Length == 2)
            {
                foreach (PlayerActions player in players)
                {
                    lvlBounds.Encapsulate(player.transform.position);
                }
            }

            //Set the miniCam's orthoSize to be equal to the z value of the Bounds.extent with an additional + 67f.
            miniCam.orthographicSize = (lvlBounds.extents.z + 67f) /* 2.5f*/;

            //Set the camera's position to the center of the bounds obj
            transform.position = new Vector3(lvlBounds.center.x, 125, lvlBounds.center.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(lvlBounds.center, (lvlBounds.extents * 4) + new Vector3(36.5f, 0, 36.5f));
    }
}
