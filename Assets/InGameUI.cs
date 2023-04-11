using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("Player 1")]
    public GameObject P1_Panel;
    Item heldItem;
    public DynamicBar_Slider[] player1_HealthBars;
    public DynamicBar_Slider[] player1_StaminaBars;
    float p1_HP, p1_STAM;
    public bool p1_dead;
    public Image P1_ItemUI;
    public Slider p1TimerSlider;

    public Camera P1_Cam;
    public CamTrackerMove p1_Tracker;
    Dynamic_SplitScreen splitScreen;

    [Header("Player 2")]
    public GameObject P2_Panel;
    public DynamicBar_Slider[] player2_HealthBars;
    public DynamicBar_Slider[] player2_StaminaBars;
    float p2_HP, p2_STAM;
    public bool p2_dead;
    public Image P2_ItemUI;
    public Slider p2TimerSlider;

    public Camera P2_Cam;
    public CamTrackerMove p2_Tracker;

    PlayerStats player1;
    PlayerStats player2;

    PlayerInventory p1Inv;
    PlayerInventory p2Inv;

    public Sprite empty;

    Item p1Held;
    Item p2Held;

    public GameObject gameOverUI;

    bool solo;

    // Start is called before the first frame update
    void Start()
    {
        p1TimerSlider.value = 0;
        p2TimerSlider.value = 0;

        if (PlayerConfigManager.Instance.GetPlayerConfigs().Count == 1)
        {
            solo = true;
            P2_Panel.SetActive(false);
        }
        else
            solo = false;

        splitScreen = FindObjectOfType<Dynamic_SplitScreen>();

        P1_Cam = splitScreen.p1_Cam.GetComponent<Camera>();
        p1_Tracker = splitScreen.camTracker_P1.GetComponent<CamTrackerMove>();

        P2_Cam = splitScreen.p2_Cam.GetComponent<Camera>();
        p2_Tracker = splitScreen.camTracker_P2.GetComponent<CamTrackerMove>();
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
        //if (p1_Inventory.currentitem != heldItem)
        //{
        //    //update icon
        //}

        if (p1Inv.itemDuration > 0)
        {
            //Run timer slider coroutine
            StartCoroutine(itemSliderTimer(p1TimerSlider, p1Held.duration, P1_ItemUI));
            p1Inv.itemDuration = 0;
        }

        if (p1Inv.itemDuration > 0)
        {
            //Run timer slider coroutine
            StartCoroutine(itemSliderTimer(p2TimerSlider, p2Held.duration, P2_ItemUI));
            p1Inv.itemDuration = 0;
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
            if (p2Inv.heldItem != p2Held)
            {
                p2Held = p2Inv.heldItem;
                UpdateItemIcon(p2Held, P2_ItemUI);
            }
        }

        if(p1Inv.heldItem != p1Held)
        {
            p1Held = p1Inv.heldItem;
            UpdateItemIcon(p1Held, P1_ItemUI);
        }

        

        if (p1_HP <= 0)
        {
            p1_dead = true;
        }
        if (p2_HP <= 0 && !solo)
        {
            p2_dead = true;
        }

        if (solo)
        {
            if (p1_dead)
            {
                gameOverUI.SetActive(true);
            }
        }

        else if (!solo)
        {


            if (p2_dead & p1_dead)
            {
                gameOverUI.SetActive(true);
            }
            else if (p2_dead & !p1_dead)
            {
                //-Reset the Split-Offset for the P1_CamTracker
                p1_Tracker.splitOffset = Vector3.zero;

                //-Disable Dynamic Splitscreen component
                splitScreen.enabled = false;

                //- Disable the Splitter Child obj
                P1_Cam.transform.Find("Splitter").gameObject.SetActive(false);

                //- Disable the P2_CamTracker
                p2_Tracker.gameObject.SetActive(false);

                //- Disable the P2_Cam
                P2_Cam.gameObject.SetActive(false);
            }
            else if (!p2_dead & p1_dead)
            {
                //-Reset the Split-Offset for the P2_CamTracker
                p2_Tracker.splitOffset = Vector3.zero;

                //-Disable DynamicSplitscreen component
                splitScreen.enabled = false;

                //- Disable the Camera component on P1_Cam
                P1_Cam.enabled = false;

                //- Disable the Splitter Child obj
                P1_Cam.transform.Find("Splitter").gameObject.SetActive(false);

                //- Disable the P1_CamTracker
                p1_Tracker.gameObject.SetActive(false);
            }
        }
    }

    public void AssignPlayer(int index, PlayerStats stats, PlayerInventory pinv)
    {
        if (index == 0)
        {
            player1 = stats;
            p1Inv = pinv;
            p1Held = pinv.heldItem;
            UpdateItemIcon(p1Held, P1_ItemUI);
            p1_HP = player1.hp;
            SetUpBars(player1_HealthBars, p1_HP);
            p1_STAM = player1.stamina;
            SetUpBars(player1_StaminaBars, p1_STAM);

            //
        }
        else if (index == 1)
        {
            player2 = stats;
            p2Inv = pinv;
            p1Held = pinv.heldItem;
            UpdateItemIcon(p1Held, P2_ItemUI);
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

    void UpdateItemIcon(Item item, Image Icon)
    {
        if(item != null)
        {
            Icon.sprite = item.itemUI;
        }
        else
        {
            Icon.sprite = empty;
        }
    }

    IEnumerator itemSliderTimer(Slider sliderTimer, float duration, Image icon)
    {
        sliderTimer.maxValue = duration;
        sliderTimer.value = 0;
        float timer = 0;
        
        while (timer <= duration)
        {
            sliderTimer.value = timer;
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        sliderTimer.value = 0;
        UpdateItemIcon(null, icon);
    }

}
