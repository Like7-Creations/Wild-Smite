using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool chosenPlayerDetected;

    public float attackRate;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        multiAttacker = GetComponent<MultiAttacker>();
        players = FindObjectsOfType<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected) timer += Time.deltaTime;
       
        if(timer >= 5)
        {
            multiAttacker.AttackPlayer();
            Debug.Log("boss behaviors happened");
            timer= 0;
        }

        if(players.Length > 1)
        {
            chosenPlayer = findClosestPlayer(players);
        }
        else if(!chosenPlayerDetected) {chosenPlayer = players[0]; chosenPlayerDetected = true; }

        dist = Vector3.Distance(transform.position, chosenPlayer.transform.position);
        if(dist < detectionRange)
        {
            playerDetected = true;
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
