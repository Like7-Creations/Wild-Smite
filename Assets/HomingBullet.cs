using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;
using UnityEngine.UIElements;

public class HomingBullet : MonoBehaviour
{
    PlayerMovement player;
    public float speed;
    bool chase;
    float dist;
    void Start()
    {
        chase = true;
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist > 2 & chase) 
        { 
            Vector3 playerPos = player.transform.position;
            playerPos.y = 1;
            transform.position = Vector3.Lerp(transform.position, playerPos, speed * Time.deltaTime);
        }else chase= false;
    }
}
