using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

    public Image[] controlledSprites;

    public void SetSpriteColors(Color color)
    {
        for (int i = 0; i < controlledSprites.Length; i++)
            controlledSprites[i].color = color;
    }
}
