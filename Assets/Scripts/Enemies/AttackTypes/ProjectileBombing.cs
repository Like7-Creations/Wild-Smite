using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBombing : Attack
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform EndPoint;
    
    [SerializeField] GameObject Rocket;

    [SerializeField] float amountOfProjectiles;
    [SerializeField] float fireRate;
    [SerializeField] float rocketSpeed;
    [SerializeField] float shootAfterWarningTime;

    [SerializeField] ParticleSystem projectileVFX;

    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator AttackType()
    {
        print("projectile bombing");
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
        float gap = Vector3.Distance(startPoint.position, EndPoint.position) / amountOfProjectiles;
        Vector3 direction = (EndPoint.position - startPoint.position).normalized * gap;
        Vector3 bombPosition = startPoint.position;
        Vector3 original = bombPosition;

        for (int i = 0; i < amountOfProjectiles; i++)
        {
            Vector3 particlePos = bombPosition;
            particlePos.y = 0.5f;
            ParticleSystem vfx = Instantiate(projectileVFX, particlePos, Quaternion.identity);
            vfx.Play();
            bombPosition += direction;
        }
        
        yield return new WaitForSeconds(shootAfterWarningTime);
        bombPosition = original;
        
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            GameObject rocket = Instantiate(Rocket, bombPosition, Quaternion.identity);
            rocket.GetComponent<Rigidbody>().AddForce(Vector3.down * rocketSpeed, ForceMode.Impulse);

            bombPosition += direction;


            if (sfx.isEnabled)
            {
                var obj = GetComponent<Boss_SFXHandler>();
                var clip = obj.projectileBombingSFX[Random.Range(0, obj.projectileBombingSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint.position, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(EndPoint.position, 1);
    }
}
