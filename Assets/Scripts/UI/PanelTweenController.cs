using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTweenController : MonoBehaviour
{

    [Header("Tween Info")]
    public Jun_TweenRuntime firstButtonTween;
    public RectDimensionsControl[] buttonsRects;

    public void TweenButtons()
    {
        firstButtonTween.Play();
    }

    public void ResetButtonTweens()
    {
        for (int i = 0; i < buttonsRects.Length; i++)
        {
            buttonsRects[i].ResetTween();
        }
    }
}
