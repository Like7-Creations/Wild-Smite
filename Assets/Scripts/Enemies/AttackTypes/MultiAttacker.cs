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
    public void AttackPlayer(int a, int b)
    {
        StartCoroutine(attacksList[Random.Range(a, b)].AttackType());     
    }

    
}
