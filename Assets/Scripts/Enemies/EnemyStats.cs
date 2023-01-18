using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public EnemyStatRange ESR;
    public Image HealthBar;

    public float Health {get; private set;}
    public float Speed {get; private set;}
    public float MATK {get; private set;}
    public float MDEF {get; private set;}
    public float MCDN {get; private set;}
    public float RATK {get; private set;}
    public float RDEF {get; private set;}
    public float RCDN {get; private set;}

    UltimateAI thiss;

    void Start()
    {
        thiss = GetComponent<UltimateAI>();
        AllocateStats();
    }

    void Update()
    {
        float thisInt = thiss.health / 200f;
        //Debug.Log(thisInt);
        HealthBar.fillAmount = (float)thisInt;
        AllocateStats();
    }


    public void AllocateStats()
    {
        float percentage = ESR.GeneratePosInRange();

        Health = ESR.Health.x + (ESR.Health.y - ESR.Health.x) * percentage;
        Health = Mathf.RoundToInt(Health);

        Debug.Log(Health);
    }
}
