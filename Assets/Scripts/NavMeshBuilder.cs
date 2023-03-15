using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    public bool delay;
    public float startDelay;
    float timer;
    bool built;

    // Start is called before the first frame update
    void Awake()
    {
        if (!delay)
            GetComponent<NavMeshSurface>().BuildNavMesh();
        else
            timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay && !built)
        {
            if (timer >= startDelay)
            {
                GetComponent<NavMeshSurface>().BuildNavMesh();
                built = true;
            }
            else
                timer += Time.deltaTime;
        }
    }
}
