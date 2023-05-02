using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public MultiAttacker multiAttack;


    void Start()
    {
        multiAttack = GetComponentInParent<MultiAttacker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerAttack()
    {
        multiAttack.TriggerAttack();
    }

    public void TriggerAttackIndication()
    {
        multiAttack.TriggerAttackIndication();
    }

    public void TriggerAttackSFX()
    {
        multiAttack.TriggerAttackSFX();
    }

    public void TriggerVFX()
    {
        multiAttack.TriggerVFX();
    }

    public void TriggerIndication()
    {

    }

    public void EndAttack()
    {
        Retreat retreatState = GetComponentInParent<Retreat>();
        GetComponentInParent<StateManager>().currentState = retreatState;
        print(retreatState + "i have done that");
    }

    public void bossEndAttack()
    {
        
    }
}
