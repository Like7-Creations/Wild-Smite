using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerStat_Data playerData;

    //[Header("Stats")]


    [field: SerializeField]
    public int hp { get; private set; }
    
    [field: SerializeField]
    public int stamina { get; private set; }
    
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
    public float aoe_Hold { get; private set; }


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

    public void UseSprint(int amount)
    {
        stamina -= (int)(amount * Time.deltaTime);
    }

    public void RecoverStamina(int amount)
    {
        stamina += (int)(amount * Time.deltaTime);
    }

    void Start()
    {
        playerData= GetComponent<PlayerStat_Data>();

        hp = playerData.hp;
        stamina = playerData.stamina;
        
        m_ATK = playerData.m_ATK;
        r_ATK = playerData.r_ATK;
        
        exp = playerData.current_XP;

        dash = playerData.dash;
        sprint = playerData.sprint;
        
        aoe_Tap = playerData.aoe_TAP;
        aoe_Hold = playerData.aoe_HOLD;
    }
}
