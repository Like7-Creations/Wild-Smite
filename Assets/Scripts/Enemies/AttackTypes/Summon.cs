using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Attack
{
    public int NoOfSpawns;
    [SerializeField] GameObject[] prefabsToSpawn;
    [SerializeField] float distFromTank;

    public override IEnumerator AttackType()
    {
        //Debug.Log("Summon");
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<BaseEnemy_SFXHandler>();
            if (obj.GetComponent<Tank_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Tank_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (obj.GetComponent<Boss_SFXHandler>() != null)
            {
                var clipObj = obj.GetComponent<Boss_SFXHandler>();
                var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }

        anim.SetTrigger("SummonPrep");
        yield return new WaitForSeconds(timeToAttackAfterIndicator);

        //ultimateAI.anim.SetTrigger("Summon");
        if (vfx.isEnabled)
        {
            if (vfx.GetComponent<Tank_VFXHandler>() != null) 
            {
                vfx.GetComponent<Tank_VFXHandler>().SummonVFX();
            }
            if (vfx.GetComponent<Boss_VFXHandler>() != null)
            {
                vfx.GetComponent<Boss_VFXHandler>().SummonVFX();
            }
        }
        for (int i = 0; i < NoOfSpawns; i++)
        {
            float angleIteration = 360 / NoOfSpawns;

            float currentRotation = angleIteration * i;

            GameObject elem = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], transform.position, transform.rotation);

            elem.transform.Rotate(new Vector3(0, currentRotation, 0));
            elem.transform.Translate(new Vector3(distFromTank, 5, 0));
            if (sfx.isEnabled)
            {
                var obj = GetComponent<BaseEnemy_SFXHandler>();
                if(obj.GetComponent<Tank_SFXHandler>() != null)
                {
                    var clipObj = obj.GetComponent<Tank_SFXHandler>();
                    var clip = clipObj.summonSFX[Random.Range(0, clipObj.summonSFX.Length)];
                    audioSource.PlayOneShot(clip);
                }

                if (obj.GetComponent<Boss_SFXHandler>() != null)
                {
                    var clipObj = obj.GetComponent<Boss_SFXHandler>();
                    var clip = clipObj.summonSFX[Random.Range(0, clipObj.summonSFX.Length)];
                    audioSource.PlayOneShot(clip);
                }
            }
        }
        if (vfx.isEnabled)
        {
            if (vfx.GetComponent<Tank_VFXHandler>() != null)
            {
                vfx.GetComponent<Tank_VFXHandler>().SummonVFX();
            }
            if (vfx.GetComponent<Boss_VFXHandler>() != null)
            {
                vfx.GetComponent<Boss_VFXHandler>().SummonVFX();
            }
        }
    }
}
