using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] string profileID = "";

    [Header("UI Content")]
    [SerializeField] GameObject noDataContent;
    [SerializeField] GameObject hasDataContent;

    [SerializeField] TextMeshProUGUI pLvl_Txt;
    [SerializeField] TextMeshProUGUI pCount_Txt;

    public void SetData(GameData data)
    {
        //If there is no data for this profileID
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        //If there is data for this profileID
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            //pLvl_Txt.text = data.level.ToString();    //Show player's current level
            //set player count here.
        }
    }

    public string GetProfileID()
    {
        return profileID;
    }



}