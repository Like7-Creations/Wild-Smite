using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangeEnemy : EnemyStats
{
    [Header("Range Stats")]
    public Vector2 min_rangeDMG;
    public float max_rangeDMG;
    [Space(10)]
    public float min_rangeDEF;
    public float max_rangeDEF;
    [Space(10)]
    public float rangeCD;

    [Header("Range Multipliers")]
    public float hpMultiplier;
    public float dmgMultiplier = 1.15f;
    public float resMultiplier = 1f;
}
