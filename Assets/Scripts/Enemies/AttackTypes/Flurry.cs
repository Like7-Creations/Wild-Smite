using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Flurry : Attack
{
    public GameObject Bullet;
    public float speed;
    public float fireRate;

    public override void AttackType()
    {
        StartCoroutine(Shoot(speed));
    }

    IEnumerator Shoot(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        float time = 0;

        while (t < duration)
        {
            time += Time.deltaTime;
            if(time > fireRate)
            {
                Rigidbody rb = Instantiate(Bullet, ultimateAI.shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(ultimateAI.shooter.transform.forward * 10f, ForceMode.Impulse);
                time = 0;
            }
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            ultimateAI.shooter.transform.eulerAngles = new Vector3(ultimateAI.shooter.transform.eulerAngles.x, yRotation, ultimateAI.shooter.transform.eulerAngles.z);
            yield return null;
        }
    }
}
