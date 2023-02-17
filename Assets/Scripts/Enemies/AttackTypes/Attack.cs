using System;
using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [HideInInspector] public UltimateAI ultimateAI;
    public virtual void Start()
    {
       ultimateAI = GetComponent<UltimateAI>(); 
    }
    public abstract void AttackType();
    public virtual void Update()
    {
        
    }
}
