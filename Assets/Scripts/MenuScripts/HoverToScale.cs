using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverToScale : MonoBehaviour
{

    public AudioSource HoverSound;
    public GameObject DisplayPanelUI;
    
    public void PointToEnter()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        HoverSound.Play();
        DisplayPanelUI.SetActive(true);
    }

    public void PointerExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        DisplayPanelUI.SetActive(false);
    }
}

