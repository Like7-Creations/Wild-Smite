using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        Debug.Log("Loading scene");
        SceneManager.LoadScene(sceneToLoad);
    }
}
