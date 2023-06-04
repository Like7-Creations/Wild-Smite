using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSimulation : MonoBehaviour
{

    public float duration;
    float time = 0;
    bool stop = false;

    public GameObject nextPart;

    [Header("UI")]
    public Slider slider;

    private void Awake()
    {
        slider.value = 0;
        slider.minValue = 0;
        slider.maxValue = duration;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            time += Time.deltaTime;
            slider.value = time;

            if (time > duration)
            {
                stop = true;
                if (nextPart != null)
                {
                    nextPart.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
