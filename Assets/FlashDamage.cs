using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    SkinnedMeshRenderer smR;

    public Material mat1;
    public Material mat2;

    public Material redMat;
    public float flashTime = 5f;

    void Start()
    {
        smR = GetComponent<SkinnedMeshRenderer>();
        mat1.color = smR.material.color;
        mat2.color = smR.material.color;
    }

    public void HitEnemy()
    {
       Debug.Log("Turned White color"); 
        StartCoroutine(Eflash());
    }

    IEnumerator Eflash()
    {

        mat1.color = redMat.color;
        mat2.color = redMat.color;

        yield return new WaitForSeconds(flashTime);

        mat1.color = smR.material.color;
        mat2.color = smR.material.color;

    }
}


