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
        GenerateStatValues();
        AllocateStats();
    }

    void Update()
    { 
        float thisInt = thisEnemy.health / myMaxHealth;
        HealthBar.fillAmount = (float)thisInt;
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void AllocateStats()
    {
       /* thisEnemy.health = Health;
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
        }*/
    }

    void GenerateStatValues()
    {
        /*Health = ESR.AllocateStats(ESR.Health);
        Speed = ESR.AllocateStats(ESR.SPD);
        MATK = ESR.AllocateStats(ESR.MATK);
        MDEF = ESR.AllocateStats(ESR.MDEF);
        MCDN = ESR.AllocateStats(ESR.MCDN);
        RATK = ESR.AllocateStats(ESR.RATK);
        RDEF = ESR.AllocateStats(ESR.RDEF);
        RCDN = ESR.AllocateStats(ESR.RCDN);*/
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
