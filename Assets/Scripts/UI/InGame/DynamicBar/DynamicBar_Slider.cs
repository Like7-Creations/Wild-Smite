using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicBar_Slider : MonoBehaviour
{
    public bool noPlayer;

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
    public List<Slider> midBarSliders = new List<Slider>();

    [Header("ColorSettings")]
    public Color baseColor;
    public Gradient ColorGradient;
    public bool useGradient;
    public bool pulse;
    public AnimationCurve pulseCurve;
    public AnimationCurve speedCurve;
    public float pulseDuration;
    float pulseTimer;

    bool obtainedValue;

    private void Awake()
    {
        if (noPlayer)
            SetSliderValues(maxValue);

        startBar.GetComponent<Bar>().SetSpriteColors(baseColor);
        endBar.GetComponent<Bar>().SetSpriteColors(baseColor);
        for (int i = 0; i < midBarSliders.Count; i++)
            midBarSliders[i].GetComponent<Bar>().SetSpriteColors(baseColor);
    }

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

        if (useGradient)
        {
            if (pulse)
            {
                pulseTimer += Time.deltaTime;
                pulseDuration = speedCurve.Evaluate(currentValue / maxValue);

                float factor = pulseCurve.Evaluate(pulseTimer / pulseDuration);

                Color col = Color.Lerp(baseColor, ColorGradient.Evaluate(currentValue / maxValue), factor);
                startBar.GetComponent<Bar>().SetSpriteColors(col);
                endBar.GetComponent<Bar>().SetSpriteColors(col);
                for (int i = 0; i < midBarSliders.Count; i++)
                    midBarSliders[i].GetComponent<Bar>().SetSpriteColors(col);

                if (pulseTimer >= pulseDuration)
                    pulseTimer = 0;
            }
            else
            {
                Color col = ColorGradient.Evaluate(currentValue / maxValue);

                startBar.GetComponent<Bar>().SetSpriteColors(col);
                endBar.GetComponent<Bar>().SetSpriteColors(col);
                for (int i = 0; i < midBarSliders.Count; i++)
                    midBarSliders[i].GetComponent<Bar>().SetSpriteColors(col);
            }
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
            //Debug.Log("Adjusting width: " + currentWidth + " to : " + desiredWidth);
        }

        if (midBarSliders.Count > midBarCount)
        {
            Debug.Log($"Extra bars found current bars are {midBarSliders.Count} | desired count is {midBarCount}");
            Transform bar = midBars.transform.GetChild(midBars.transform.childCount - 1);
            Destroy(bar.gameObject);
        }
        else if (midBarSliders.Count < midBarCount)
        {
            for (int i = midBars.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(midBars.transform.GetChild(i));
                midBarSliders.Clear();
            }

            for (int i = 0; i < midBarCount; i++)
            {
                GameObject bar = Instantiate(barPrefab, midBars.transform);
                midBarSliders.Add(bar.GetComponent<Slider>());
            }
        }

        //CollectMidSliders();
        SetMidBarValues();
    }

    void CollectMidSliders()
    {
        //midBarSliders = midBars.GetComponentsInChildren<Slider>(true);
    }

    void SetMidBarValues()
    {
        float minValue = sliderInterval;
        for (int i = 0; i < midBarSliders.Count; i++)
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

        for (int i = 0; i < midBarSliders.Count; i++)
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

    public void DeductValue(float value)
    {
        desiredValue -= value;
    }

    public void FullValue()
    {
        desiredValue = maxValue;
    }
}
