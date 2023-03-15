using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_VFXHandler : MonoBehaviour
{
    public bool isEnabled;

    [SerializeField] public ParticleSystem attackIndicationVFX;
                      
    [SerializeField] public ParticleSystem enemyHitVFX;
                   
    [SerializeField] public ParticleSystem enemyDeathVFX;
}
