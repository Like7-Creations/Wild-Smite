using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{

    public LevelCompletion LevelCompleteUI;

    [SerializeField] PauseMenuController pauseMenu;

    private void Awake()
    {
        //pauseMenu = FindObjectOfType<PauseMenuController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pauseMenu = FindObjectOfType<PauseMenuController>();
            pauseMenu.gameObject.SetActive(false);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            LevelCompleteUI.gameObject.SetActive(true);
            LevelCompleteUI.ShowSummary();

        }
    }
}
