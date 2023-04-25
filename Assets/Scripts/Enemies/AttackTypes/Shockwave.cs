using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Attack
{
    [SerializeField] bool begin;
    [SerializeField] float radius;
    [SerializeField] float radiusEnd;
    [SerializeField] float expandSpeed;
    [SerializeField] float knockBackStr;
    [SerializeField] float knockBacktime;

    public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Boss_SFXHandler>();
            var clip = obj.shockwaveSFX[Random.Range(0, obj.shockwaveSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        begin = true;
    }

    public override void Update()
    {
        if (begin)
        {
            radius += Time.deltaTime * expandSpeed;
            vfx.GetComponent<Boss_VFXHandler>().ShockwaveVFX();
            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, radius);
            foreach(Collider c in hits)
            {
                if (c.GetComponent<PlayerActions>() != null)
                {
                    PlayerActions player = c.GetComponent<PlayerActions>();
                    StartCoroutine(player.Mover(knockBackStr, knockBacktime, transform.forward));
                }
            }
            if(radius >= radiusEnd)
            {
                begin = false;

                vfx.GetComponent<Boss_VFXHandler>().ShockwaveVFX();
                reset();
            }

        }
    }

    private void reset()
    {
        radius = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusEnd);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
