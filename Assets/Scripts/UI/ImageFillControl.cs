using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillControl : MonoBehaviour
{

    public AnimationCurve curve;
    public float speed = 1;
    public Image image;

    bool trigger;
    float timer;

    private void Awake()
    {
        image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && timer <= 1)
        {
            timer += Time.deltaTime * speed;
            image.fillAmount = curve.Evaluate(timer);
        }
    }

    public void Tween()
    {
        image.fillAmount = 0;
        trigger = true;
        timer = 0;
    }
}
