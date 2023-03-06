using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timer;
    PlayerMovement player;
    PlayerActions actions;
    [SerializeField] bool enemy;
    [SerializeField] public float damage;

    public bool playershot;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        actions = player.GetComponent<PlayerActions>();
    }

    void Update()
    {
        Destroy(gameObject, timer);
        Vector3 playerPos = player.transform.position;
        if (enemy)
        {
            playerPos.y = 1;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detecteddddddddddd");
        if(other.gameObject.GetComponent<UltimateAI>() != null & playershot)
        {
            UltimateAI victim = other.gameObject.GetComponent<UltimateAI>();
            victim.TakeDamage(actions.pStats.r_ATK, actions.GetComponent<PlayerStats>());// Deal damage to the enemy
            Destroy(gameObject);
        }

        if(other.gameObject.GetComponent<PlayerStats>() != null & !playershot)
        {
            PlayerStats anotherVictim = other.gameObject.GetComponent<PlayerStats>();
            anotherVictim.GetComponent<PlayerActions>().TakeDamage(damage, Vector3.zero);
            Destroy(gameObject);
        }

    }
}
