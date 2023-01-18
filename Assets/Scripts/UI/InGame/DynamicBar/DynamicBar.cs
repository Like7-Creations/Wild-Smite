using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBar : MonoBehaviour
{
    [Header("Bar Settings")]
    [Range(1, 10)]
    public int numOfBars = 1;

    public GameObject midBars;
    public GameObject barPrefab;
    RectTransform midRT;
    public float barWidth;
    public float barHeight;

    float currentWidth;
    float desiredWidth;

    [Header("Bar Values")]
    public float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        midRT = midBars.GetComponent<RectTransform>();
        currentWidth = midRT.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        desiredWidth = barWidth * numOfBars;

        if (currentWidth != desiredWidth)
        {
            midRT.sizeDelta = new Vector2(desiredWidth, barHeight);
            currentWidth = midRT.rect.width;
            Debug.Log("Adjusting width: " + currentWidth + " to : " + desiredWidth);
        }
        
        if (midBars.transform.childCount > numOfBars)
        {
            Transform bar = midBars.transform.GetChild(midBars.transform.childCount - 1);
            Destroy(bar.gameObject);
        }
        else if (midBars.transform.childCount < numOfBars)
        {
            for (int i = 0; i < (numOfBars - midBars.transform.childCount); i++)
            {
                GameObject bar = Instantiate(barPrefab, midBars.transform);
            }
        }
    }
}
