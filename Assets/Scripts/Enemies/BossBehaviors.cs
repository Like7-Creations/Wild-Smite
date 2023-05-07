using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] public PlayerActions[] players;

    [HideInInspector] public PlayerActions chosenPlayer;

    public Transform rotationPoint;

    Vector3 myPos;

    [HideInInspector] public bool currentAttack;

    public float timeSpeed;

    [SerializeField] bool playerFound;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        multiAttacker = GetComponent<MultiAttacker>();
        players = FindObjectsOfType<PlayerActions>();
        if(players.Length == 1) { chosenPlayer = players[0]; }
        myPos = transform.position;
    }
    
    void Update()
    {
        if (!playerFound)
        {
            players = FindObjectsOfType<PlayerActions>();
            if (boss)
            {
                BossDisable[] gb = FindObjectsOfType<BossDisable>();
                for (int i = 0; i < gb.Length; i++)
                    gb[i].gameObject.SetActive(false);
               
            }
        }

        if (players.Length != 0) playerFound = true;

        print(playerFound);

        if (playerFound)
        {
            Time.timeScale = timeSpeed;
            if (players.Length > 0) chosenPlayer = findClosestPlayer(players);

            dist = Vector3.Distance(transform.position, chosenPlayer.transform.position);

            if (dist < detectionRange) playerDetected = true;
            if (playerDetected && !currentAttack) timer += Time.deltaTime;

            if (transform.position != myPos)
            {
                transform.position = myPos;
            }

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
            if (boss) pos.y = 20; else pos.y = 2.5f;
            //pos.y = boss ? 10 : 2.5f;
            rotationPoint.LookAt(pos);

            if(boss && GetComponent<EnemyStats>().Health <= 0)
            {
                SceneManager.LoadScene("_MVP_MainMenu");
            }
        }
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
