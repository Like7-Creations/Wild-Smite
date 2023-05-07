using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseUIControl : MonoBehaviour
{

    public AnimationCurve curve;
    public float pulseDuration;
    public float min, max;

    float timer;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.material.EnableKeyword("_EMISSION");
        timer = 0;


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SetEmission(curve.Evaluate(timer / pulseDuration));

        if (timer >= pulseDuration)
            timer = 0;
    }

    void SetEmission(float percent)
    {
        float emission = min + (max - min) * percent;
        Material mat = image.material;
        Color col = image.color * emission;
        mat.SetColor("_EmissionColor", col);
    }
}
