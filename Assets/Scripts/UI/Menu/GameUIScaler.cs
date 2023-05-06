using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIScaler : MonoBehaviour
{
    void Awake()
    {
        float newScale = PlayerPrefs.GetFloat("GameUIScale");

        transform.localScale += new Vector3(newScale, newScale, 0);

        /*List<GameObject> playerUI = new List<GameObject>()
        {
            GameObject.Find("Player_1_UI")
        };

        if(PlayerConfigManager.Instance.GetPlayerConfigs().Count == 2)
        {
            playerUI.Add(GameObject.Find("Player_2_UI"));
        }

        foreach(GameObject pUI in playerUI)
        {
            newScale = PlayerPrefs.GetFloat("GameUIScale");

            pUI.transform.localScale += new Vector3(newScale, newScale, 0);
        }*/
    }
}