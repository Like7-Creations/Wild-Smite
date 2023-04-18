using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timer;
    PlayerMovement player;
    PlayerActions actions;
    [SerializeField] bool enemy;
    [SerializeField] public float damage;

    [SerializeField] ParticleSystem destroyedVFX;

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
        //Debug.Log("collision detecteddddddddddd");
        if (playershot & other.gameObject.GetComponent<EnemyStats>() != null)
        {
            EnemyStats victim = other.gameObject.GetComponent<EnemyStats>();
            victim.TakeDamage(actions.pStats.r_ATK, actions.pStats);// Deal damage to the enemy
            destroyedVFX.transform.parent = null;
            destroyedVFX.Play();
            Destroy(gameObject);
        }

        if(!playershot & other.gameObject.GetComponent<PlayerActions>() != null)
        {
            PlayerStats anotherVictim = other.gameObject.GetComponent<PlayerStats>();
            anotherVictim.GetComponent<PlayerActions>().TakeDamage(damage);
            destroyedVFX.transform.parent = null;
            destroyedVFX.Play();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Building"))
        {
            destroyedVFX.transform.parent = null;
            destroyedVFX.Play();
            Destroy(gameObject,0.05f);
        }
    }
}
