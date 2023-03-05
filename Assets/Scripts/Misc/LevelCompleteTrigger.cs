using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{

    public LevelCompletion LevelCompleteUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelCompleteUI.gameObject.SetActive(true);
            LevelCompleteUI.ShowSummary();
        }
    }
}
