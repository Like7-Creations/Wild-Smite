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
    public float maxValue;
    public float reduceSpeed = 2;
    public float reduceDelay = 0;
    bool runDelay;
    float delayTimer;
    float currentValue, desiredValue, prevValue;
    public float barMaxValue;

    [Header("Sliders")]
    float sliderInterval;
    public Slider startBar;
    public Slider endBar;
    public Slider[] midBarSliders;

    bool obtainedValue;

    // Update is called once per frame
    void Update()
    {
        if (obtainedValue)
        {
            //SetMidBars();

            if (desiredValue != prevValue && !runDelay)
            {
                Debug.Log($"{gameObject.name} Value Has Changed");
                runDelay = true;
            }

            if (runDelay && reduceDelay > 0)
            {
                Debug.Log($"{gameObject.name} running Delay for {reduceDelay} seconds");
                delayTimer += Time.deltaTime;
                if (delayTimer >= reduceDelay)
                {
                    Debug.Log($"{gameObject.name} Delay Finished, Updating Values");
                    ValuesUpdateLogic();
                    prevValue = desiredValue;
                    delayTimer = 0;
                    runDelay = false;
                }
            }
            else
            {
                //Debug.Log($"{gameObject.name} No Delay, Updating Values");
                ValuesUpdateLogic();
                if (prevValue != desiredValue)
                    prevValue = desiredValue;
            }
        }
    }

    void ValuesUpdateLogic()
    {
        if (currentValue > desiredValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, desiredValue, reduceSpeed * Time.deltaTime);
            UpdateSliderValues();
        }
        else if (currentValue < desiredValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, desiredValue, reduceSpeed * Time.deltaTime);
            UpdateSliderValues();
        }

        //UpdateSliderValues();
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

    public void SetSliderValues(float fullValue)
    {
        midRT = midBars.GetComponent<RectTransform>();
        currentWidth = midRT.rect.width;

        int barCount = Mathf.CeilToInt(fullValue / barMaxValue);
        if ((barCount - 2) <= 0)
            midBarCount = 0;
        else
            midBarCount = barCount - 2;


        sliderInterval = fullValue / barCount;
        startBar.minValue = 0;
        startBar.maxValue = sliderInterval;

        endBar.minValue = fullValue - sliderInterval;
        endBar.maxValue = fullValue;

        currentValue = fullValue;
        desiredValue = fullValue;
        prevValue = desiredValue;

        obtainedValue = true;

        SetMidBars();
        UpdateSliderValues();
    }

    public void ChangeValue(float amount)
    {
        desiredValue = amount;
    }

    public void FullValue()
    {
        desiredValue = maxValue;
    }
}
