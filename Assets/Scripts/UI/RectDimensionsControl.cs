using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectDimensionsControl : MonoBehaviour
{

    public AnimationCurve curve;
    public float speed = 1;
    RectTransform rectTransform;
    public Vector2 from;
    public Vector2 to;

    bool trigger;
    float timer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = from;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && timer <= 1)
        {
            timer += Time.deltaTime * speed;
            rectTransform.sizeDelta = Vector2.Lerp(from, to, curve.Evaluate(timer));
        }
    }

    public void Tween()
    {
        rectTransform.sizeDelta = from;
        trigger = true;
        timer = 0;
    }
}
