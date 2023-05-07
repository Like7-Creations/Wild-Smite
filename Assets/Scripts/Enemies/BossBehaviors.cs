using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBehaviors : MonoBehaviour
{
    float timer;
    EnemyStats stats;
    MultiAttacker multiAttacker;

    [Header("General Settings")]
    [SerializeField] float meleeRange;
    [SerializeField] float detectionRange;
    [SerializeField] bool playerDetected;
    public float attackRate;
    public bool boss;
    float dist;

    [HideInInspector] public PlayerActions[] players;

    [HideInInspector] public PlayerActions chosenPlayer;

    public Transform rotationPoint;

    Vector3 myPos;

    [HideInInspector] public bool currentAttack;

    public float timeSpeed;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        multiAttacker = GetComponent<MultiAttacker>();
        players = FindObjectsOfType<PlayerActions>();
        if(players.Length == 1) { chosenPlayer = players[0]; }
        myPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed;
        if (players.Length > 1) chosenPlayer = findClosestPlayer(players);

        dist = Vector3.Distance(transform.position, chosenPlayer.transform.position);

        if(dist < detectionRange) playerDetected = true;
        if (playerDetected && !currentAttack) timer += Time.deltaTime;

        if(transform.position != myPos)
        {
            transform.position = myPos;
        }

        

        /*switch (BossType)
        {
            case Type.Tank:
                
                if (dist < 5) { a = 0; b = 2; }
                else { a = 2; b = 4; }
                   
                break;

            case Type.Boss:
               
                if (dist < 5) { a = 0; b = 3; }
                else { a = 4; b = 7; }
                
                break;
        }

        print(multiAttacker.attacksList.Length / 2);
        print((multiAttacker.attacksList.Length / 2) + 1);
        print(multiAttacker.attacksList.Length);*/
        //print($"{a} / {b}");


        if (timer >= attackRate)
        {
            Debug.Log("Called Attack");
            GetComponentInChildren<Animator>().SetBool("FlurryLoop", true); // this is soo that the flurry attack can keep happening the rig
            if (dist <= meleeRange)
            {
                multiAttacker.AttackPlayer(0, multiAttacker.attacksList.Length / 2);
            }
            else
            {
                multiAttacker.AttackPlayer((multiAttacker.attacksList.Length / 2), multiAttacker.attacksList.Length);
            }
            currentAttack = true;
            timer = 0;
        }

        Vector3 pos = chosenPlayer.transform.position;
        pos.y= 2.5f;
        rotationPoint.LookAt(pos);
       // pos.y = 0;

    }

    public PlayerActions findClosestPlayer(PlayerActions[] Players)
    {
        PlayerActions tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (PlayerActions t in Players)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
