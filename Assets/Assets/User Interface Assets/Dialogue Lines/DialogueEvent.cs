using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;

public class DialogueEvent : MonoBehaviour
{

    [SerializeField] TMP_Text speakerText, dialogueText;

    public TextAsset file;
    List<string> speakerValues;
    List<string> dialogueValues;
    int currentIndex;

    void Awake()
    {
        speakerValues = new List<string>();
        dialogueValues = new List<string>();
        ParseTextAsset();

        currentIndex = 0;
        DisplayDialogue();
    }

    void ParseTextAsset()
    {
        string fs = file.text;
        string[] fLines = file.text.Split("\n"[0]);
        //string[] lines = Regex.Split(fs, "\n|\r|\r\n");

        for (int i = 0; i < fLines.Length; i++)
        {
            string line = fLines[i];
            string[] values = Regex.Split(line, ":");
            for (int j = 0; j < values.Length; j++)
            {
                Debug.Log($"Line value {j} is: {values[j]}");
            }

            speakerValues.Add(values[0]);
            dialogueValues.Add(values[1]);
        }
    }

    void DisplayDialogue()
    {
        speakerText.text = speakerValues[currentIndex];
        dialogueText.text = dialogueValues[currentIndex];

        currentIndex++;
    }

    public void NextLine()
    {
        if (currentIndex < speakerValues.Count)
            DisplayDialogue();
        else
            gameObject.SetActive(false);
    }
}
