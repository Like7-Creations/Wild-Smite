using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
        Destroy(gameObject, timer);
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (enemy)
        {
            playerPos.y = 1;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collision detection of bullet,
        Debug.Log(other.gameObject.name);
        if(playershot && other.gameObject.tag != "Player")
        {
            if (other.gameObject.GetComponent<EnemyStats>() != null)
            {
                EnemyStats victim = other.gameObject.GetComponent<EnemyStats>();
                int knockbackMulti = 1;
                if (!victim.weakness) { knockbackMulti = 2; }
                victim.TakeDamage(actions.pStats.r_ATK, actions.pStats, transform.forward, knockbackMulti);// Deal damage to the enemy
                BulletDie();
            }
            else BulletDie();
        }

        if(!playershot && other.gameObject.tag == "Player")
        {
            BulletDie();
        }

        if(!playershot && other.gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<PlayerActions>() != null)
            {
                PlayerStats anotherVictim = other.gameObject.GetComponent<PlayerStats>();
                anotherVictim.GetComponent<PlayerActions>().TakeDamage(damage, transform.forward);
                BulletDie();
            }
            else BulletDie();
        }
    }

    void BulletDie()
    {
        destroyedVFX.transform.parent = null;
        destroyedVFX.Play();
        Destroy(gameObject);
    }
}
