using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Flurry : Attack
{
    public GameObject Bullet;
    public float spinDuration;
    public float speed;
    public float fireRate;

    public override IEnumerator AttackType()
    {
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
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
                Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(ultimateAI.shooter.transform.forward * 10f, ForceMode.Impulse);
                if (sfx.isEnabled)
                {
                    var obj = GetComponent<Boss_SFXHandler>();
                    var clip = obj.flurrySFX[Random.Range(0, obj.flurrySFX.Length)];
                    audioSource.PlayOneShot(clip);
                }
                time = 0;
            }
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / spinDuration) % 360.0f;
            ultimateAI.shooter.transform.eulerAngles = new Vector3(ultimateAI.shooter.transform.eulerAngles.x, yRotation, ultimateAI.shooter.transform.eulerAngles.z);
            yield return null;
        }
    }
}
