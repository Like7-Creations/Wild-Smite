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


    public override void AttackType()
    {
        StartCoroutine(ProjectileBomb());
    }

    IEnumerator ProjectileBomb()
    {
        float gap = Vector3.Distance(startPoint.position, EndPoint.position) / amountOfProjectiles;
        Vector3 direction = (EndPoint.position - startPoint.position).normalized * gap;
        Vector3 bombPosition = startPoint.position;

        for (int i = 0; i < amountOfProjectiles; i++)
        {
            GameObject rocket = Instantiate(Rocket, bombPosition, Quaternion.identity);
            rocket.GetComponent<Rigidbody>().AddForce(Vector3.down * rocketSpeed, ForceMode.Impulse);

            bombPosition += direction;
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
