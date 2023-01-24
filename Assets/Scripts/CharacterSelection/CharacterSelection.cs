using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{

    public GameObject[] characters;
    public GameObject CharacterUIOnSelect;
    // public GameObject CharacterChosenOnSelect;
    public ParticleSystem CharacterParticleOnChosen;
    public int selectedCharacter = 1;
    public AudioSource NavigateSelectSound;
    public AudioSource ChosenSelectSound;
    public float delayTime = 2f;

    public void NextCharacter()
    {
        NavigateSelectSound.Play();
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        NavigateSelectSound.Play();
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartCharacterSelectGame()
    {
        CharacterUIOnSelect.SetActive(false);
        CharacterParticleOnChosen.Play();
     // CharacterChosenOnSelect.SetActive(true);
        ChosenSelectSound.Play();
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        Invoke("DelayCharacterSelectGame", delayTime);
    }

                                    void DelayCharacterSelectGame()
                                    {
                                        SceneManager.LoadScene("Sami");
                                        Time.timeScale = 1f;
                                    }
}
