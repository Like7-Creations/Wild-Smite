using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummon : Attack
{
    [SerializeField] GameObject[] meleeOrigins;
    [SerializeField] GameObject[] rangeOrigins;

    [SerializeField] GameObject meleePrefab;
    [SerializeField] GameObject rangePrefab;


    public override void attackLogic()
    {
        for (int i = 0; i < meleeOrigins.Length; i++)
        {
            Instantiate(meleePrefab, meleeOrigins[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < rangeOrigins.Length; i++)
        {
            Instantiate(rangePrefab, rangeOrigins[i].transform.position, Quaternion.identity);
        }
        GetComponent<BossBehaviors>().currentAttack = false;
    }

    public override void attackVFX()
    {
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Tank_VFXHandler>().SummonVFX();//vfx
        }
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.summonSFX[Random.Range(0, obj.summonSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Boss_VFXHandler>().attackIndicationVFX.Play();
        }
    }

    /*public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);

        for (int i = 0; i < meleeOrigins.Length; i++)
        {
            Instantiate(meleePrefab, meleeOrigins[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < rangeOrigins.Length; i++)
        {
            Instantiate(rangePrefab, rangeOrigins[i].transform.position, Quaternion.identity);
        }

        ultimateAI.anim.SetTrigger("Summon");
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Tank_VFXHandler>().SummonVFX();//vfx
        }
    }*/
}
