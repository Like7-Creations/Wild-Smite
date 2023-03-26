using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Flurry : Attack
{
    public GameObject Bullet;
    public GameObject origin;
    public float spinDuration;
    public float speed;
    public float fireRate;

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
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        float time = 0;

        while (t < spinDuration)
        {
            time += Time.deltaTime;
            if (time > fireRate)
            {
                vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
                Rigidbody rb = Instantiate(Bullet, origin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(origin.transform.forward * 10f, ForceMode.Impulse);
                if (sfx.isEnabled)
                {
                    var obj = GetComponent<Boss_SFXHandler>();
                    var clip = obj.flurrySFX[Random.Range(0, obj.flurrySFX.Length)];
                    audioSource.PlayOneShot(clip);
                    vfx.GetComponent<Boss_VFXHandler>().FlurryVFX();
                }
                time = 0;
            }
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / spinDuration) % 360.0f;
            origin.transform.eulerAngles = new Vector3(origin.transform.eulerAngles.x, yRotation, origin.transform.eulerAngles.z);

            yield return null;
        }
    }
}
