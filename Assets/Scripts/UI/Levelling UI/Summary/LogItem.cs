using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogItem : MonoBehaviour
{

    public TMP_Text typeCountText;
    public TMP_Text givenXPText;

    public void SetItem(string typeText, string xpText)
    {
        typeCountText.text = typeText;
        givenXPText.text = xpText;
    }
}
