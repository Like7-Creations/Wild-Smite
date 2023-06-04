using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttacker : MonoBehaviour
{
    public Attack[] attacksList;

    public Attack currentAttack;

    public PlayerMovement[] playerAmount;
    public PlayerMovement chosenPlayer;

    public PlayerMovement playerOne;
    public PlayerMovement playerTwo;
    
    PlayerConfigManager playerConfig;

    AudioSource aud;


    public void AttackPlayer(int a, int b)
    {
        int rand = Random.Range(a, b);  
        print(rand);
        //StartCoroutine(attacksList[rand].AttackType());
        attacksList[rand].startAttack();
        currentAttack = attacksList[rand];
    }
    public void TriggerAttack()
    {
        currentAttack.attackLogic();
    }

    public void TriggerAttackIndication()
    {
        currentAttack.AttackIndication();
    }

    public void TriggerAttackSFX()
    {
        currentAttack.attackSFX();
    }

    public void TriggerVFX()
    {
        currentAttack.attackVFX();
    }


}
