using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverToScale : MonoBehaviour
{

    //public AudioSource HoverSound;
    public GameObject DisplayPanelUI;
    public TMP_Text uiText;
    Vector3 original;

    public void PointToEnter()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        uiText.color = Color.green;
        //HoverSound.Play();
        if (DisplayPanelUI != null)
        {
            DisplayPanelUI.SetActive(true);
        }
    }

    public void PointerExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        uiText.color = Color.black;
        if (DisplayPanelUI != null)
            DisplayPanelUI.SetActive(false);
    }

    public void ResetUI()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        uiText.color = Color.black;
    }

    //transform.localScale = new Vector3(1f, 1f, 1f);
    // uiText.color = Color.black;
}

