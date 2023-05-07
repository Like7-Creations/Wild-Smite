using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceSystem : MonoBehaviour
{

    public Choice[] choices;

    public void SelectChoice(int choice)
    {
        choices[choice].choiceResult.Invoke();
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Choice 
{
    public string name;
    public UnityEvent choiceResult;
}