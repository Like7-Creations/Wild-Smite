using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    //[HideInInspector] public UltimateAI ultimateAI;
    //[HideInInspector] public FieldOfView fov;
    [HideInInspector] public Enemy_VFXHandler vfx;
    [HideInInspector] public Enemy_SFXHandler sfx;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public State state;
    [HideInInspector] public EnemyStats stats;
    [HideInInspector] public Animator anim;

    public string animTriggerName; 

    public float timeToAttackAfterIndicator;

    public virtual void Start()
    {
        //ultimateAI = GetComponent<UltimateAI>();
        //fov = GetComponent<FieldOfView>();
        anim = GetComponentInChildren<Animator>();
        state = GetComponent<State>();
        stats = GetComponent<EnemyStats>();
        vfx = GetComponent<Enemy_VFXHandler>();
        sfx = GetComponent<Enemy_SFXHandler>();
        audioSource = GetComponent<AudioSource>();
    }
    //public abstract IEnumerator AttackType();

    public void startAttack() 
    {
        //StartCoroutine(AttackType());
        anim.SetTrigger(animTriggerName);
        //Debug.Log("trigggered" + animTriggerName);
    }

    /*public void CallCour()
    {
        StartCoroutine(AttackType());
    }*/

    public virtual void attackSFX() { }

    public virtual void attackVFX() { }

    public virtual void attackLogic() { }

    public virtual void AttackIndication() { }


    public virtual void Update(){}

    protected AnimationClip getAnimationClip(Animator anim, string clipname)
    {
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if(clip.name == clipname)
            {
                return clip;
            }
        }
        return null;
    }
}
