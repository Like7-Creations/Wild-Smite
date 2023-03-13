using System;
using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [HideInInspector] public UltimateAI ultimateAI;
    [HideInInspector] public FieldOfView fov;
    [HideInInspector] public Enemy_VFXHandler vfx;
    [HideInInspector] public Enemy_SFXHandler sfx;

    public virtual void Start()
    {
        ultimateAI = GetComponent<UltimateAI>();
        fov = GetComponent<FieldOfView>();
        vfx = GetComponent<Enemy_VFXHandler>();
    }
    public abstract void AttackType();
    public virtual void Update(){}
}
