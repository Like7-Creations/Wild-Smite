using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public UltimateAI thisEnemy;
    float myMaxHealth;
    public EnemyStatRange ESR;
    public Image HealthBar;
    public Canvas canvas;

    public float Health {get; private set;}
    public float Speed {get; private set;}
    public float MATK {get; private set;}
    public float MDEF {get; private set;}
    public float MCDN {get; private set;}
    public float RATK {get; private set;}
    public float RDEF {get; private set;}
    public float RCDN {get; private set;}


    void Start()
    {
        thisEnemy = GetComponent<UltimateAI>();
        myMaxHealth = thisEnemy.health;
    }

    void Update()
    { 
        float thisInt = thisEnemy.health / myMaxHealth;
        HealthBar.fillAmount = (float)thisInt;
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void AllocateStats()
    {
        thisEnemy.health = Health;
        thisEnemy.moveSpeed = Speed;
        if(thisEnemy.type == UltimateAI.Type.Melee)
        {
            thisEnemy.damageToDeal = MATK;
            thisEnemy.Defence = MDEF;
            thisEnemy.attackRate = MCDN;
        }
        if (thisEnemy.type == UltimateAI.Type.Ranged)
        {
            thisEnemy.damageToDeal = RATK;
            thisEnemy.Defence = RDEF;
            thisEnemy.reloadTime = RCDN;
        }
        if (thisEnemy.type == UltimateAI.Type.Tank)
        {
            
        }

    }

    public void GenerateStatValues(LevelSettings.Difficulty difficulty)
    { 
        Health = ESR.AllocateStats(ESR.Health, difficulty);
        Speed = ESR.AllocateStats(ESR.SPD, difficulty);
        MATK = ESR.AllocateStats(ESR.MATK, difficulty);
        MDEF = ESR.AllocateStats(ESR.MDEF, difficulty);
        MCDN = ESR.AllocateStats(ESR.MCDN, difficulty);
        RATK = ESR.AllocateStats(ESR.RATK, difficulty);
        RDEF = ESR.AllocateStats(ESR.RDEF, difficulty);
        RCDN = ESR.AllocateStats(ESR.RCDN, difficulty);
        AllocateStats();
    }

   /* public float AllocateStats(Vector2 valu)
    {
        float percentage = ESR.GeneratePosInRange();
        float stat = 0;

        stat = valu.x + (valu.y - valu.x) * percentage;
        stat = Mathf.RoundToInt(stat);
        Debug.Log(stat);
        return stat;
    }*/
}
