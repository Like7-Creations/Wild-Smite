using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : ScriptableObject
{
    [Header("General Stats")]
    public int Level;
    public int CurrentLevel;
    [Header("Base General Stats")]
    public Vector2 Health;
    public Vector2 moveSpeed;
    [Header("Current General Stats")]
    public Vector2 cur_Health;
    public Vector2 cur_moveSpeed;

   // [Header("Level multipliers")]
    
}
