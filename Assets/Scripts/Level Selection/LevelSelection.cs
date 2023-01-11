using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public int minLevels, maxLevels;
    public GameObject optionPrefab;

    public Vector2 positionRange;

    public Canvas canvas;
    public GameObject panel;
    public Vector2 edgePadding;
    public Vector2 areaSize;

    bool OptionsCreated;
    List<Vector3> placedPositions;

    // Start is called before the first frame update
    void Start()
    {
        Rect rect = RectTransformUtility.PixelAdjustRect(transform.GetComponent<RectTransform>(), canvas);
        areaSize.x = rect.x - edgePadding.x;
        areaSize.y = rect.y - edgePadding.y;

        placedPositions = new List<Vector3>();

        if (!OptionsCreated)
            GenerateLevelOptions();

        panel.SetActive(false);
    }



    public void GenerateLevelOptions()
    {
        int amount = Random.Range(minLevels, maxLevels);

        for (int i = 0; i < amount; i++)
        {
            GameObject option = Instantiate(optionPrefab);
            option.transform.SetParent(transform, false);

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
        Vector3 position = new Vector3(Random.Range(edgePadding.x, positionRange.x - edgePadding.x), Random.Range(edgePadding.y, positionRange.y - edgePadding.y), 0);

        if (placedPositions.Count > 0 && placedPositions != null)
        {
            bool validPosition = false;

            while (!validPosition)
            {
                bool tooCloseX = false;
                bool tooCloseY = false;

                for (int i = 0; i < placedPositions.Count; i++)
                {
                    tooCloseX = position.x <= placedPositions[i].x + edgePadding.x && position.x >= placedPositions[i].x - edgePadding.x;
                    tooCloseY = position.y <= placedPositions[i].y + edgePadding.y && position.y >= placedPositions[i].y - edgePadding.y;

                    if (!tooCloseX && !tooCloseY)
                    {
                        validPosition = true;
                        break;
                    }
                    else
                    {
                        position = new Vector3(Random.Range(edgePadding.x, positionRange.x - edgePadding.x), Random.Range(edgePadding.y, positionRange.y - edgePadding.y), 0);
                        break;
                    }
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
