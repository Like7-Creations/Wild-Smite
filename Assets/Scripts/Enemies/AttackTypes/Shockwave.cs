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
        Gizmos.DrawSphere(transform.position, radius);
    }
}
