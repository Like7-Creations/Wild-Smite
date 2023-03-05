using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyDefeat : MonoBehaviour
{

    public EnemyDefeats.EnemyType type;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            count++;
            other.GetComponent<PlayerStats>().SetEnemyCount(type, count);
            Debug.Log($"Player Defeated {type.ToString()} Enemy");
        }
    }
}
