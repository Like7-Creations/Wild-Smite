using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelectionPanel : MonoBehaviour
{

    public bool newGame;
    public bool solo;
    public LoadSlots[] slots;

    [Header("PlayerSelection")]
    public GameObject SelectionCanvas;
    public GameObject SelectionPanel;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (newGame)
                slots[i].SetMode(newGame);

            if (solo)
                slots[i].SetPlayers(1);
            else
                slots[i].SetPlayers(2);
        }
    }

    public void LoadCharacterSelection()
    {
        SelectionCanvas.SetActive(true);
    }

    public void SetMode(bool mode)
    {
        newGame = mode;

        for (int i = 0; i < slots.Length; i++)
        {
            if (newGame)
                slots[i].SetMode(newGame);
        }
    }

    public void SetPlayers(bool single)
    {
        solo = single;

        for (int i = 0; i < slots.Length; i++)
        {
            if (solo)
                slots[i].SetPlayers(1);
            else
                slots[i].SetPlayers(2);
        }
    }
}
