using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueSystem : MonoBehaviour
{

    public TextMeshProUGUI textcomponent;
    public string[] dialoguelines;
    public float textSpeed;

    private int index;


    void Start()
    {
        textcomponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (textcomponent.text == dialoguelines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text = dialoguelines[index];
            }
        }
    } 

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        foreach (char c in dialoguelines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }    
    }

    void NextLine()
    {
        if (index < dialoguelines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
