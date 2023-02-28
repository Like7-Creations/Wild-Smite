using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Attack
{
    public int NoOfSpawns;
    [SerializeField] GameObject[] prefabsToSpawn;
    [SerializeField] float distFromTank;

    public override void AttackType()
    {
        Debug.Log("Summon");
        for (int i = 0; i < NoOfSpawns; i++)
        {
            float angleIteration = 360 / NoOfSpawns;

            float currentRotation = angleIteration * i;


            GameObject elem = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], transform.position, transform.rotation);

            elem.transform.Rotate(new Vector3(0, currentRotation, 0));
            elem.transform.Translate(new Vector3(distFromTank, 5, 0));
        }
    }
}
