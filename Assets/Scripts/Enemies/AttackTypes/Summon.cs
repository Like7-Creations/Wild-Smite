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
        yield return new WaitForSeconds(timeToAttackAfterIndicator);

        ultimateAI.anim.SetTrigger("Summon");
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Tank_VFXHandler>().SummonVFX();//vfx
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
                var obj = GetComponent<Tank_SFXHandler>();
                var clip = obj.summonSFX[Random.Range(0, obj.summonSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Tank_VFXHandler>().SummonVFX();//vfx
        }
    }
}
