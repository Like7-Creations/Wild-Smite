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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        UltimateAI victim = collision.gameObject.GetComponent<UltimateAI>();

        victim.TakeDamage(actions.pStats.r_ATK);    //Deal damage to the enemy
    }
}
