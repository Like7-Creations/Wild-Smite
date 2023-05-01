using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBehaviors : MonoBehaviour
{
    float timer;
    EnemyStats stats;
    MultiAttacker multiAttacker;

    [SerializeField] float detectionRange;
    float dist;
    bool playerDetected;

    public PlayerActions[] players;

    public PlayerActions chosenPlayer;

    public float attackRate;

    public bool boss;

    public enum Type
    {
        Tank,
        Boss
    }
    public Type BossType;

    int a, b;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        multiAttacker = GetComponent<MultiAttacker>();
        players = FindObjectsOfType<PlayerActions>();
        if(players.Length == 1) { chosenPlayer = players[0]; }
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length > 1) chosenPlayer = findClosestPlayer(players);

        dist = Vector3.Distance(transform.position, chosenPlayer.transform.position);

        if(dist < detectionRange) playerDetected = true;
        if (playerDetected) timer += Time.deltaTime;

        switch (BossType)
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

        if(timer >= 10000)
        {
            print($"{a} / {b}");
            multiAttacker.AttackPlayer(a,b);
            timer = 0;
        }

        Vector3 pos = chosenPlayer.transform.position;
       // pos.y = 0;
        transform.LookAt(pos);

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
