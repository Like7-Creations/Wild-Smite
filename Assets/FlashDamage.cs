using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer smR;

    public Material mat0;
    public Material mat1;

    public Material redMat;
    public float flashTime = 5f;
    public bool flashing;

    void Start()
    {
        smR = GetComponent<SkinnedMeshRenderer>();
        mat0 = smR.materials[0];
        mat1 = smR.materials[1];
    }


    private void Update()
    {
        if (flashing)
        {
            plsHappen();
            Eflash();
        }
    }

    public void HitEnemy()
    {
        //Debug.Log("Turned White color");
        flashing = true;
        //StartCoroutine(Eflash());
        Eflash();
    }

    public void plsHappen()
    { 
        smR.materials[0] = redMat;
        smR.materials[1] = redMat;
    }

    public void Eflash()
    {
        smR.materials[0] = redMat;
        smR.materials[1] = redMat;

        //yield return new WaitForSeconds(flashTime);

        /*smR.materials[0] = mat0;
        smR.materials[1] = mat1;*/
        flashing = false;

    }
}


