using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerStat_Data playerData;

    //[Header("Stats")]


    [field: SerializeField]
    public int hp { get; private set; }
    
    [field: SerializeField]
    public float stamina { get; private set; }
    
    [field: SerializeField]
    public int m_ATK { get; private set; }
    
    [field: SerializeField]
    public int r_ATK { get; private set; }
    
    [field: SerializeField]
    public int exp { get; private set; }



    [field: SerializeField]
    public float recovRate_HP { get; private set; }

    [field: SerializeField]
    public float recovRate_STAMINA { get; private set; }



    [field: SerializeField]
    public float dash { get; private set; }

    [field: SerializeField]
    public float sprint { get; private set; }

    [field: SerializeField]
    public float aoe_Tap { get; private set; }

    [field: SerializeField]
    public float aoe_Hold { get; private set; }         //Max Charge Amount

    [field: SerializeField]
    public float aoe_ChargeRate { get; private set; }   //Amount per second


    void AddEXP(int addXP)
    {
        exp += addXP;
    }

    public void LoseHealth(int dmg)
    {
        hp -= dmg;
    }

    public void RecoverHP(int amount)
    {
        hp += amount;
    }

    public void UseDash(int amount)
    {
        stamina -= amount;
    }

    public void UseSprint(float amount)
    { 
        stamina -= amount * Time.deltaTime;
        //stamina = Mathf.RoundToInt(stamina);
    }

    public void RecoverStamina(float amount)
    {
        Debug.Log("recover stamina called");
        stamina += amount * Time.deltaTime;
        if(stamina >= playerData.stamina) 
        {
            stamina = playerData.stamina;
        }
        //stamina = Mathf.RoundToInt(stamina);
    }

    public void NullifyAOE_Tap()
    {
        aoe_Tap = 0;
    }

    public void ResetAOE_TapVal()
    {
        aoe_Tap = playerData.aoe_TAP;
    }

    void Start()
    {
        //playerData= GetComponent<PlayerStat_Data>();

        hp = playerData.hp;
        stamina = playerData.stamina;
        
        m_ATK = playerData.m_ATK;
        r_ATK = playerData.r_ATK;
        
        exp = playerData.current_XP;

        dash = playerData.dash;
        sprint = playerData.sprint;
        
        aoe_Tap = playerData.aoe_TAP;
        aoe_Hold = playerData.aoe_HOLD;
        aoe_ChargeRate = playerData.aoe_ChargeRate;
    }
}
