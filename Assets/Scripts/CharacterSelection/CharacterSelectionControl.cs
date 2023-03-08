using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionControl : MonoBehaviour
{

    public MainMenu menu;

    public void ReturnToMainMenu()
    {
        menu.ResetManager(gameObject);
    }
}
