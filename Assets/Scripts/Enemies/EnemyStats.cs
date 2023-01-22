using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public UltimateAI thisEnemy;
    public EnemyStatRange ESR;
    //public Image HealthBar;

    public float Health; //{get; private set;}
    public float Speed; //{get; private set;}
    public float MATK; //{get; private set;}
    public float MDEF; //{get; private set;}
    public float MCDN; //{get; private set;}
    public float RATK; //{get; private set;}
    public float RDEF; //{get; private set;}
    public float RCDN; //{get; private set;}


    void Start()
    {
        thisEnemy = GetComponent<UltimateAI>();
        Vector2 hell = new Vector2(90, 100);
        ESR.GenerateStats(Health,Speed,MATK, MDEF, MCDN, RATK, RDEF, RCDN);
        Debug.Log(Health);
        AllocateStats();
    }

    void Update()
    {
        //Debug.Log(thisInt);
        //float thisInt = thisEnemy.health / 200f;
       // HealthBar.fillAmount = (float)thisInt;
       // AllocateStats();
    }

    public void AllocateStats()
    {
        thisEnemy.health = Health;
        thisEnemy.moveSpeed = Speed;
        if(thisEnemy.type == UltimateAI.Type.Melee)
        {
            thisEnemy.damageToDeal = MATK;
            thisEnemy.attackRate = MCDN;
        }
        if (thisEnemy.type == UltimateAI.Type.Ranged)
        {
            thisEnemy.damageToDeal = RATK;
            thisEnemy.reloadTime = RCDN;
        }
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
