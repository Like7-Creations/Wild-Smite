using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DMGEffect : MonoBehaviour
{
    public Vector3 Dir;

    [HideInInspector] public float amount;
    public float speed;
    public float duration;


    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"{amount}";
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Dir, speed * Time.deltaTime);
        transform.LookAt(Camera.main.transform.position);
    }
}
