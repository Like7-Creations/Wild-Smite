using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedLoad : MonoBehaviour
{

    public float delay = 2;

    private void Awake()
    {
        StartCoroutine(DelayedLoading());
    }

    IEnumerator DelayedLoading()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<loadScene>().LoadScene();
    }
}
