using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[ExecuteAlways]
public class TextConverter : MonoBehaviour
{

    public TextHolders[] textElements;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < textElements.Length; i++)
            if (textElements[i].tmp.text != textElements[i].text.text)
                textElements[i].tmp.text = textElements[i].text.text;
    }
}

[System.Serializable]
public class TextHolders
{
    public Text text;
    public TMP_Text tmp;
}
