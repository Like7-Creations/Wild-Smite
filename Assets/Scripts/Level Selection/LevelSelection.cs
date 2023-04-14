using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button closeButton;
    public List<LevelOption> options;
    int currentOption;

    // Start is called before the first frame update
    void Start()
    {
        options = new List<LevelOption>();
        placedPositions = new List<Vector3>();
        loopCountExceded = 0;

        if (!OptionsCreated)
            GenerateLevelOptions();

        panel.SetActive(false);

    }

    public void SelectFirstOption()
    {
        Debug.Log("Selecting fisrt option");
        options[0].CollapsedButton.Select();
        currentOption = 0;
    }

    public void GenerateLevelOptions()
    {
        int amount = Random.Range(minLevels, maxLevels);
        Debug.Log($"Want to spawn {amount} options");
        for (int i = 0; i < amount; i++)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            //option.transform.SetParent(transform, false);
            Debug.Log("Spawning Option " + (i + 1));

            RectTransform rt = option.GetComponent<RectTransform>();
            rt.anchorMax = Vector2.zero;
            rt.anchorMin = Vector2.zero;
            Vector3 pos = GeneratePosition();
            option.GetComponent<LevelOption>().optionPosition = pos;
            options.Add(option.GetComponent<LevelOption>());
            rt.anchoredPosition3D = pos;
            //rt.localScale = Vector3.one;
        }

        options.Sort((o1, o2) => o1.optionPosition.x.CompareTo(o2.optionPosition.x));
        for (int i = 0; i < options.Count; i++)
        {
            Navigation nav = options[i].CollapsedButton.navigation;
            nav.mode = Navigation.Mode.Explicit;

            if (i + 1 < options.Count)
                nav.selectOnRight = options[i + 1].CollapsedButton;
            else
                nav.selectOnRight = options[0].CollapsedButton;



            if (i == 0)
                nav.selectOnLeft = options[options.Count - 1].CollapsedButton;
            else
                nav.selectOnLeft = options[i - 1].CollapsedButton;

            nav.selectOnUp = closeButton;

            options[i].CollapsedButton.navigation = nav;
            options[i].ExpandedButton.navigation = nav;

            options[i].CreateOption();
        }

        Navigation cNav = closeButton.navigation;
        cNav.mode = Navigation.Mode.Explicit;
        cNav.selectOnDown = options[0].CollapsedButton;
        closeButton.navigation = cNav;

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
