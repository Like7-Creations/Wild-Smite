using System;
using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [HideInInspector] public UltimateAI ultimateAI;
    [HideInInspector] public FieldOfView fov;
    //public PlayerMovement chosenPlayer;
    public virtual void Start()
    {
       ultimateAI = GetComponent<UltimateAI>();
        fov = GetComponent<FieldOfView>();
    }
    public abstract void AttackType();
    public virtual void Update(){}
}
