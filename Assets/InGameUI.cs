using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [Header("Player 1")]
    public GameObject P1_Panel;
    public DynamicBar_Slider[] player1_HealthBars;
    public DynamicBar_Slider[] player1_StaminaBars;
    float p1_HP, p1_STAM;
    
    [Header("Player 2")]
    public GameObject P2_Panel;
    public DynamicBar_Slider[] player2_HealthBars;
    public DynamicBar_Slider[] player2_StaminaBars;
    float p2_HP, p2_STAM;

    PlayerStats player1;
    PlayerStats player2;

    bool solo;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerConfigManager.Instance.GetPlayerConfigs().Count == 1)
        {
            solo = true;
            P2_Panel.SetActive(false);
        }
        else
            solo = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Player 1 Check Values
        if (p1_HP != player1.hp)
        {
            p1_HP = player1.hp;
            UpdateBars(player1_HealthBars, p1_HP);
        }
        if (p1_STAM != player1.stamina)
        {
            p1_STAM = player1.stamina;
            UpdateBars(player1_StaminaBars, p1_STAM);
        }
        
        //Player 2 Check Values
        if (!solo)
        {
            if (p2_HP != player2.hp && !solo)
            {
                p2_HP = player2.hp;
                UpdateBars(player2_HealthBars, p2_HP);
            }
            if (p2_STAM != player2.stamina && !solo)
            {
                p2_STAM = player2.stamina;
                UpdateBars(player2_StaminaBars, p2_STAM);
            }
        }
    }

    public void AssignPlayer(int index, PlayerStats stats)
    {
        if (index == 0)
        {
            player1 = stats;
            p1_HP = player1.hp;
            SetUpBars(player1_HealthBars, p1_HP);
            p1_STAM = player1.stamina;
            SetUpBars(player1_StaminaBars, p1_STAM);
        }
        else if (index == 1)
        {
            player2 = stats;
            p2_HP = player2.hp;
            SetUpBars(player2_HealthBars, p2_HP);
            p2_STAM = player2.stamina;
            SetUpBars(player2_StaminaBars, p2_STAM);
        }
    }

    void UpdateBars(DynamicBar_Slider[] sliders, float value)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].ChangeValue(value);
        }
    }

    void SetUpBars(DynamicBar_Slider[] sliders, float value)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].SetSliderValues(value);
        }
    }
}
