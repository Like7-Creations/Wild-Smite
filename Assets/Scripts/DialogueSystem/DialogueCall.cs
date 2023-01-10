using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCall : MonoBehaviour
{

    DialogueSystem DialogueSystem;
    public GameObject DialoguePanelStatus;

    private void OnTriggerEnter(Collider Player)
    {
        DialoguePanelStatus.SetActive(true);
    }

    private void OnTriggerExit(Collider Player)
    {
        Destroy(DialoguePanelStatus);
    }
}
