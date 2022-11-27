using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeEnemy : EnemyStats
{
    [Header("Melee Stats")]
    public float min_meleeDMG;
    public float max_meleeDMG;
    [Space(10)]
    public float min_meleeDEF;
    public float max_meleeDEF;
    [Space(10)]
    public float meleeCD;

    [Header("Melee Multipliers")]
    public float hpMultiplier;
    public float dmgMultiplier = 1.15f;
    public float resMultiplier = 1f;
}
