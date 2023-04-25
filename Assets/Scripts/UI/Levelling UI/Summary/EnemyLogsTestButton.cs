using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogsTestButton : MonoBehaviour
{

    public EnemyInfo.Type type;
    public int count;

    public PlayerStats player;

    public void ChangeValue(string value)
    {
        count = int.Parse(value);
    }

    public void ApplyValues()
    {
        //player.SetEnemyCount(type);
    }
}
