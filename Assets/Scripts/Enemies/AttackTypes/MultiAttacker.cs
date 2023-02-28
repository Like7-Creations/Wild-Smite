using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttacker : MonoBehaviour
{
    public Attack[] attacksList;

    public PlayerMovement[] playerAmount;
    public PlayerMovement chosenPlayer;

    public PlayerMovement playerOne;
    public PlayerMovement playerTwo;
    
    PlayerConfigManager playerConfig;
    public void AttackPlayer()
    {
        //StartCoroutine(attacksList[Random.Range(0, attacksList.Length)].AttackType());
        attacksList[Random.Range(0, attacksList.Length)].AttackType();
        //playerConfig = FindObjectOfType<PlayerConfigManager>();
        
    }

    public void Start()
    {
        if (playerAmount.Length == 2)
        {
            playerOne = playerAmount[0];
            playerTwo = playerAmount[1];
        }
        else
        {
            chosenPlayer= playerAmount[0];
        }
    }

    public void Update()
    {
        if(playerAmount.Length == 2)
        {
            float playerOneDist = Vector3.Distance(transform.position, playerOne.transform.position);
            float playerTwoDist = Vector3.Distance(transform.position, playerTwo.transform.position);   
            if(playerOneDist < playerTwoDist) 
            {
                chosenPlayer = playerOne;
            }
            else
            {
                chosenPlayer = playerTwo;
            }
        }
    }
}
