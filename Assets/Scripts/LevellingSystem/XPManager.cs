using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public CharacterStats cS;
    public TextMeshProUGUI currentXpText, requiredXpText, levelText;
    public float currentXp, requiredXp, level;
    public float xpMultiplier = 1.25f;

    public float[] levelCount;

    private void Awake()
    {
        if(cS.PlayerLevel == 1)
        {
            cS.PlayerRequiredXp = 100;
            cS.PlayerXP = 0;
        }
    }

    private void Start()
    {
        currentXpText.text = currentXp.ToString("0");

        requiredXpText.text = requiredXp.ToString("0");

        levelText.text = cS.PlayerLevel.ToString("0");

        level = cS.PlayerLevel;
        currentXp = cS.PlayerXP;
        //requiredXp = cS.PlayerRequiredXp;

    }

    public void AddXp(int xp)
    {
        currentXp += xp;

        while(currentXp >= requiredXp) 
        {
            currentXp = currentXp - requiredXp;
            cS.PlayerLevel++;

            //Formula Round to Nearest 10th
            requiredXp = (requiredXp + 1) * xpMultiplier;
            requiredXp = requiredXp / 10;
            requiredXp = Mathf.Round(requiredXp);
            requiredXp = requiredXp * 10;
        

            cS.PlayerRequiredXp = requiredXp;
            levelText.text = cS.PlayerLevel.ToString("0");
            requiredXpText.text = requiredXp.ToString("0");

        }

        cS.PlayerXP = currentXp;
        currentXpText.text = currentXp.ToString("0");

    }

}
