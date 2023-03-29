using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionControl : MonoBehaviour
{

    public MainMenu menu;
    public GameObject cursor;

    public void ReturnToMainMenu()
    {
        menu.ResetManager(gameObject);
        cursor.SetActive(false);
    }
}
