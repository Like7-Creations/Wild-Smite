using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectionPanel : MonoBehaviour
{

    public bool newGame;
    public bool solo;
    public LoadSlots[] slots;

    [Header("PlayerSelection")]
    public GameObject SelectionCanvas;
    public GameObject SelectionPanel;

    [Header("FirstSelectedButtons")]
    public Button modeSelect;

    [Header("Tween Settings")]
    public float interval;

    public void LoadCharacterSelection()
    {
        SelectionCanvas.SetActive(true);
    }

    public void SelectMode()
    {
        modeSelect.Select();
    }

    public void SelectSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetMode(newGame);

            if (solo)
                slots[i].SetPlayers(1);
            else
                slots[i].SetPlayers(2);

            slots[i].LoadSaveInfo();
        }

        slots[0].GetComponent<Button>().Select();
        StartCoroutine(SlotSequencialTween());
    }

    public void SetMode(bool mode)
    {
        newGame = mode;

        for (int i = 0; i < slots.Length; i++)
        {
            if (newGame)
                slots[i].SetMode(newGame);

            //slots[i].LoadSaveInfo();
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

    public void ResetSlotTweens()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ResetTweens();
        }
    }

    IEnumerator SlotSequencialTween()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].tweenControl.Play();
            yield return new WaitForSeconds(interval);
        }
    }
}
