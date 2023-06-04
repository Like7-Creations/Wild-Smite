using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public abstract class State : MonoBehaviour
{
    public PlayerActions[] players;

    public PlayerActions chosenPlayer;

    public NavMeshAgent agent;

    [HideInInspector] public Enemy_VFXHandler vfx;
    [HideInInspector] public Enemy_SFXHandler sfx;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Animator anim;
    [HideInInspector] public EnemyStats stats;


    public abstract State RunCurrentState();

    public virtual void Start()
    {
        stats = GetComponent<EnemyStats>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        players = FindObjectsOfType<PlayerActions>();
        audioSource = GetComponent<AudioSource>();
        vfx = GetComponent<Enemy_VFXHandler>();
        sfx = GetComponent<Enemy_SFXHandler>();
    }

    public virtual void Update()
    {
        chosenPlayer = findClosestPlayer(players);
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
