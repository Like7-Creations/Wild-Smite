using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicBar_Slider : MonoBehaviour
{
    [Header("Bar Settings")]
    int midBarCount = 1;

    public GameObject midBars;
    public GameObject barPrefab;
    RectTransform midRT;
    public float barWidth;
    public float barHeight;

    float currentWidth;
    float desiredWidth;

    [Header("Bar Values")]
    [Range(0, 500)]
    public float maxValue;
    public float reduceSpeed = 2;
    float currentValue, desiredValue;
    public float barMaxValue;

    [Header("Sliders")]
    float sliderInterval;
    public Slider startBar;
    public Slider endBar;
    public Slider[] midBarSliders;

    // Start is called before the first frame update
    void Start()
    {
        midRT = midBars.GetComponent<RectTransform>();
        currentWidth = midRT.rect.width;

        int barCount = Mathf.CeilToInt(maxValue / barMaxValue);
        midBarCount = barCount - 2;

        sliderInterval = maxValue / barCount;
        startBar.minValue = 0;
        startBar.maxValue = sliderInterval;

        endBar.minValue = maxValue - sliderInterval;
        endBar.maxValue = maxValue;

        currentValue = maxValue;
        desiredValue = maxValue;

        SetMidBars();
        UpdateSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
        SetMidBars();

        if (currentValue > desiredValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, desiredValue, reduceSpeed * Time.deltaTime);
            UpdateSliderValues();
        }
        else if (currentValue < desiredValue)
        {
            currentValue = desiredValue;            
        }

        UpdateSliderValues();
    }

    void SetMidBars()
    {
        desiredWidth = barWidth * midBarCount;

        if (currentWidth != desiredWidth)
        {
            midRT.sizeDelta = new Vector2(desiredWidth, barHeight);
            currentWidth = midRT.rect.width;
            Debug.Log("Adjusting width: " + currentWidth + " to : " + desiredWidth);
        }

        if (midBars.transform.childCount > midBarCount)
        {
            Transform bar = midBars.transform.GetChild(midBars.transform.childCount - 1);
            Destroy(bar.gameObject);
        }
        else if (midBars.transform.childCount < midBarCount)
        {
            for (int i = 0; i < (midBarCount - midBars.transform.childCount); i++)
            {
                GameObject bar = Instantiate(barPrefab, midBars.transform);
            }
        }

        CollectMidSliders();
        SetMidBarValues();
    }

    void CollectMidSliders()
    {
        midBarSliders = midBars.GetComponentsInChildren<Slider>(true);
    }

    void SetMidBarValues()
    {
        float minValue = sliderInterval;
        for (int i = 0; i < midBarSliders.Length; i++)
        {
            midBarSliders[i].minValue = minValue;

            minValue += sliderInterval;

            midBarSliders[i].maxValue = minValue;
        }
    }

    void UpdateSliderValues()
    {
        if (currentValue > startBar.maxValue)
            startBar.value = startBar.maxValue;
        else if (currentValue < startBar.minValue)
            startBar.value = startBar.minValue;
        else
            startBar.value = currentValue;

        if (currentValue > endBar.maxValue)
            endBar.value = endBar.maxValue;
        else if (currentValue < endBar.minValue)
            endBar.value = endBar.minValue;
        else
            endBar.value = currentValue;

        for (int i = 0; i < midBarSliders.Length; i++)
        {
            if (currentValue > midBarSliders[i].maxValue)
                midBarSliders[i].value = midBarSliders[i].maxValue;
            else if (currentValue < midBarSliders[i].minValue)
                midBarSliders[i].value = midBarSliders[i].minValue;
            else
                midBarSliders[i].value = currentValue;
        }
    }

    public void TestReduceValue(float amount)
    {
        desiredValue -= amount;
    }

    public void FullValue()
    {
        desiredValue = maxValue;
    }
}
