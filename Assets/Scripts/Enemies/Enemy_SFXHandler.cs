using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SFXHandler : MonoBehaviour
{
    public bool isEnabled;

    [SerializeField] public AudioClip[] playerDetectedSFX;

    [SerializeField] public AudioClip[] enemyAttackIndicatorSFX;
                      
    [SerializeField] public AudioClip[] enemyHitSFX;
                      
    [SerializeField] public AudioClip[] enemyDestroyedSFX;

    //AudioClip enemyFootSteps;
}