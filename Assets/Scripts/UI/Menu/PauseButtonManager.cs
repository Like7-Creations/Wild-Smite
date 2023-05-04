using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonManager : MonoBehaviour
{
    PauseGame pauseMenu;

    void Start()
    {
        pauseMenu = GetComponentInParent<PauseGame>();
    }

    public void Resume()
    {
        pauseMenu.GameResume();
    }

    public void Forfeit()
    {
        pauseMenu.GameForfeit();
    }
}
