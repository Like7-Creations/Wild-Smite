using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public PlayerStat_Data player;

    public int minLevels, maxLevels;
    public GameObject optionPrefab;

    public Vector2 MaxPosValues;
    public Vector2 MinPosValues;

    public Canvas canvas;
    public GameObject panel;
    public Vector2 InstancePaddingX;
    public Vector2 InstancePaddingY;

    bool OptionsCreated;
    List<Vector3> placedPositions;

    int loopCountExceded;

    // Start is called before the first frame update
    void Start()
    {
        placedPositions = new List<Vector3>();
        loopCountExceded = 0;

        if (!OptionsCreated)
            GenerateLevelOptions();

        panel.SetActive(false);
    }

    public void GenerateLevelOptions()
    {
        int amount = Random.Range(minLevels, player.lvl);

        for (int i = 0; i < amount; i++)
        {
            GameObject option = Instantiate(optionPrefab);
            option.transform.SetParent(transform, false);
            Debug.Log("Spawning Option " + (i + 1));

            RectTransform rt = option.GetComponent<RectTransform>();
            rt.anchorMax = Vector2.zero;
            rt.anchorMin = Vector2.zero;
            rt.anchoredPosition3D = GeneratePosition();
            //rt.localScale = Vector3.one;
        }

        OptionsCreated = true;
    }

    public Vector3 GeneratePosition()
    {
        int run = 0;
        Vector3 position = new Vector3(Random.Range(MinPosValues.x, MaxPosValues.x), Random.Range(MinPosValues.y, MaxPosValues.y), 0);
        Debug.Log($"First Generated Position is: " + position);

        if (placedPositions.Count > 0 && placedPositions != null)
        {
            bool validPosition = false;
            int loopCount = 0;

            while (!validPosition)
            {
                
                loopCount++;
                bool tooCloseX = false;
                bool tooCloseY = false;
                int validCount = 0;

                for (int i = 0; i < placedPositions.Count; i++)
                {
                    Debug.Log("Comparing Generated Position to Option " + (i + 1));

                    if ((position.x < placedPositions[i].x && position.x > placedPositions[i].x - InstancePaddingX.x) || (position.x > placedPositions[i].x && position.x < placedPositions[i].x + InstancePaddingX.y))
                        tooCloseX = true;
                    if ((position.y < placedPositions[i].y && position.y > placedPositions[i].y - InstancePaddingY.x) || (position.y > placedPositions[i].y && position.y < placedPositions[i].y + InstancePaddingY.y))
                        tooCloseY = true;

                    if (!tooCloseX && !tooCloseY)
                    {
                        //validPosition = true;
                        validCount++;
                        Debug.Log($"Position is Valid {validCount}/{placedPositions.Count}");
                    }
                }

                Debug.Log($"Final Valid count is {validCount}/{placedPositions.Count}");

                if (validCount == placedPositions.Count)
                {
                    validPosition = true;
                    break;
                }
                else
                {
                    position = new Vector3(Random.Range(MinPosValues.x, MaxPosValues.x), Random.Range(MinPosValues.y, MaxPosValues.y), 0);
                    Debug.Log("Position is not Valid");
                    Debug.Log($"New Generated Position {run} is: " + position);
                }

                if (loopCount > 100)
                {
                    Debug.Log("Loop Count Exceeded, breaking Loop");
                    loopCountExceded++;
                    break;
                }                
            }

            placedPositions.Add(position);
            return position;
        }
        else
        {
            placedPositions.Add(position);
            return position;
        }
    }
}
