using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeEnemy : EnemyStats
{
    [Header("Melee Base Stats")]
    public Vector2 meleeDMG;
    [Space(5)]
    public Vector2 meleeDEF;
    [Space(5)]
    public float meleeCD;

    [Header("Melee Current Stats")]
    public Vector2 cur_meleeDMG;
    [Space(5)]
    public Vector2 cur_meleeDEF;
    [Space(5)]
    public float cur_meleeCD;

    [Header("Melee Multipliers")]
    public float hpMultiplier;
    public float dmgMultiplier = 1.15f;
    public float resMultiplier = 1f;
}
