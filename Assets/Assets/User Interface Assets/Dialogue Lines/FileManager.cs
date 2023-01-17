using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class FileManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI txt_alllines, txt_randomdialogues;

    string[] linesArray;
    string myFilePath, fileName;
    public TextAsset file;
    StreamReader Reader;
    string Speaker;
    string Dialogue = "";

    void Start()
    {
        string Text = file.text;
        Reader = new FileInfo(file.text).OpenText();
        Debug.Log("textstarted");
        
        /*fileName = "DialogueContext.txt"; // Use this line when developing
        myFilePath = Application.dataPath + "/" + fileName; // myFilePath = fileName; // Use this line when building the project
      //  myFilePath = fileName;*/
    }

    void DisplayAllLines()
    {

        if (Dialogue != null)
        {
            Dialogue = Reader.ReadLine();
            Debug.Log("DialogueRan " + Dialogue);
        }


        // System.Array.Sort(linesArray);
        /*foreach (string line in linesArray)
        {
            txt_alllines.text += line + "\n";
        }*/
    }

    public void ReadFromTheFile()
    {
        linesArray = File.ReadAllLines(myFilePath);
        DisplayAllLines();
        /*
        System.Array.Sort(linesArray);
        foreach (string line in linesArray)
        {
            print(line);
        }*/

        int randomNumber = Random.Range(0, linesArray.Length);
        print(linesArray[randomNumber]);
    }
}
