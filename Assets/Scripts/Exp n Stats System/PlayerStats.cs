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
	public List<EnemyDefeats> defeatedEnemies;

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

	//For Testing
    /* void SetEnemyCount(EnemyInfo.Type type, int count)
    {
        bool hasEntry = false;
        for (int i = 0; i < defeatedEnemies.Count; i++)
        {
            if (defeatedEnemies[i].Type == type)
            {
                hasEntry = true;
                defeatedEnemies [i].count = count;
            }            
        }

        if (!hasEntry)
            defeatedEnemies.Add(new EnemyDefeats(type, count));
    }*/
    public void SetEnemyCount(EnemyInfo.Type type)
    {
        bool hasEntry = false;
        for (int i = 0; i < defeatedEnemies.Count; i++)
        {
            if (defeatedEnemies[i].enemyType == type)
            {
                hasEntry = true;
                defeatedEnemies [i].count++;
            }            
        }

        if (!hasEntry)
            defeatedEnemies.Add(new EnemyDefeats(type, 1));
    }

    public void SetData(PlayerStat_Data data)
    {
        playerData = data;
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

[System.Serializable]
public class EnemyDefeats
{ 
    public EnemyInfo.Type enemyType;
    public string name { set { name = enemyType.ToString(); } }
    public int count;

    public EnemyDefeats(EnemyInfo.Type type, int count)
    {
        this.enemyType = type;
        this.count = count;
    }
}
