using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timer;
    PlayerMovement player;
    [SerializeField] bool enemy;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
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
    }
}
