using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : ScriptableObject
{
    [Header("General Stats")]
    public int Level;
    public int CurrentLevel;
    public float min_Health;
    public float max_Health;
    public float moveSpeed;

   // [Header("Level multipliers")]
    
}
