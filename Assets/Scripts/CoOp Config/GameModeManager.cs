using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public GameMode gamemode;
    [SerializeField] GameObject playerTwoChars;
    void Start()
    {
        setSettings();   
    }
    public void SinglePlayerMode()
    {
        gamemode.GM = GameMode.Gamemode.SinglePlayer;
        playerTwoChars.SetActive(false);
    }

    public void CoopMode()
    {
        gamemode.GM = GameMode.Gamemode.CoopPlayer;
        playerTwoChars.SetActive(true);
    }

    public void setSettings()
    {
        //spawnplayer(s)
    }

    public void setPlayerOneChar()
    {

    }

    public void setPlayerTwoChar()
    {

    }
}
