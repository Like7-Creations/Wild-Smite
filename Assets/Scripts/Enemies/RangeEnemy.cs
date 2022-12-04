using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangeEnemy : EnemyStats
{
    [Header("Range Stats")]
    public Vector2 rangeDMG;
    [Space(10)]
    public Vector2 rangeDEF;
    [Space(10)]
    public float rangeCD;

    [Header("Range Multipliers")]
    public float hpMultiplier;
    public float dmgMultiplier = 1.15f;
    public float resMultiplier = 1f;
}
