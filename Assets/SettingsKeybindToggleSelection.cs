using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsKeybindToggleSelection : MonoBehaviour
{

    public Image frame;
    public TMP_Text text;

    public Color selectedColor;
    public Color deSelectedColor;

    public void ToggleColor(bool state)
    {
        if (state)
        {
            frame.color = selectedColor;
            text.color = selectedColor;
        }
        else
        {
            frame.color = deSelectedColor;
            text.color = deSelectedColor;
        }
    }
}
