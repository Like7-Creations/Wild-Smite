using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DMGEffect : MonoBehaviour
{
    public Vector3 Dir;
    public float speed;
    public float duration;
    float timer;
    [HideInInspector] public float amount;

    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"{amount}uwu";
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Dir, speed * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
