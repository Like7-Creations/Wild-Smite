using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerStat_Data playerData;

    #region Player_Stat Variables
    [field: Space(10)]
    [field: Header("Stats")]

    [field: SerializeField]
    public int hp { get; private set; }

    [field: SerializeField]
    public float stamina { get; private set; }

    [field: SerializeField]
    public int m_ATK { get; private set; }

    [field: SerializeField]
    public int r_ATK { get; private set; }
    #endregion

    #region XP & Leveling Variables
    [field: Space(10)]
    [field: Header("Leveling Stuff")]
    [field: SerializeField]
    public int exp { get; private set; }

    public List<EnemyDefeats> defeatedEnemies;
    #endregion

    #region Health Variables & Settings
    [field: Space(10)]
    [field: Header("Health Consumption & Recovery Settings")]
    [field: SerializeField]
    public float recovRate_HP { get; private set; }
    #endregion

    #region Stamina Variables & Settings
    [field: Space(10)]
    [field: Header("Sprint & Dash Settings")]
    [field: SerializeField]
    public float dash { get; private set; }

    [field: SerializeField]
    public float sprint { get; private set; }


    [field: Space(10)]
    [field: Header("Stamina Consumption & Recovery Settings")]
    [field: SerializeField]
    public float recovRate_STAMINA { get; private set; }

    [field: SerializeField]
    public float stamRecov_Delay { get; private set; }

    [field: SerializeField]
    public bool begin_STAMRecov { get; private set; }

    [field: SerializeField]
    public bool stam_recov { get; private set; }
    #endregion

    #region Area Of Effect Variables & Settings
    [field: Space(10)]
    [field: Header("Area Of Effect Settings")]
    [field: SerializeField]
    public float aoe_Tap { get; private set; }

    [field: SerializeField]
    public float aoe_Hold { get; private set; }         //Max Charge Amount

    [field: SerializeField]
    public float aoe_ChargeRate { get; private set; }   //Amount per second
    #endregion

    void AddEXP(int addXP)
    {
        exp += addXP;
    }

    //For Testing
    public void SetEnemyCount(EnemyDefeats.EnemyType type, int count)
    {
        bool hasEntry = false;
        for (int i = 0; i < defeatedEnemies.Count; i++)
        {
            if (defeatedEnemies[i].enemyType == type)
            {
                hasEntry = true;
                defeatedEnemies[i].count = count;
            }
        }

        if (!hasEntry)
            defeatedEnemies.Add(new EnemyDefeats(type, count));
    }
    public void SetEnemyCount(EnemyDefeats.EnemyType type)
    {
        bool hasEntry = false;
        for (int i = 0; i < defeatedEnemies.Count; i++)
        {
            if (defeatedEnemies[i].enemyType == type)
            {
                hasEntry = true;
                defeatedEnemies[i].count++;
            }
        }

        if (!hasEntry)
            defeatedEnemies.Add(new EnemyDefeats(type, 1));
    }

    public void SetData(PlayerStat_Data data)
    {
        playerData = data;
    }

    #region Player Health Functions
    public void LoseHealth(int dmg)
    {
        hp -= dmg;
    }

    public void RecoverHP(int amount)
    {
        hp += amount;
    }
    #endregion

    #region Player Stamina Functions
    public void UseDash(int amount)
    {
        stamina -= amount;

        stam_recov = false;
        begin_STAMRecov = false;
    }

    public void UseSprint(float amount)
    {
        stamina -= amount * Time.deltaTime;

        stam_recov = false;
        begin_STAMRecov = false;
    }

    public void RecoverStamina(float amount)
    {
        Debug.Log("recover stamina called");
        stamina += playerData.recovRate_STAMINA * Time.deltaTime;
        if (stamina >= playerData.stamina)
        {
            stamina = playerData.stamina;

            stam_recov = false;
            begin_STAMRecov = false;
        }
    }

    public IEnumerator STAM_RecovDelay()
    {
        begin_STAMRecov = true;

        Debug.Log("Beginning Stam_Recov delay");
        yield return new WaitForSeconds(stamRecov_Delay);

        stam_recov = true;
    }
    #endregion

    #region AOE Functions
    public void NullifyAOE_Tap()
    {
        aoe_Tap = 0;
    }

    public void ResetAOE_TapVal()
    {
        aoe_Tap = playerData.aoe_TAP;
    }
    #endregion

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
    public enum EnemyType
    {
        Melee,
        Ranged,
        Tank
    }

    public EnemyType enemyType;
    public string name { set { name = enemyType.ToString(); } }
    public int count;

    public EnemyDefeats(EnemyType type, int count)
    {
        this.enemyType = type;
        this.count = count;
    }
}
