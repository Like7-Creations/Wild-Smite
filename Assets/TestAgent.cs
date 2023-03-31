using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    [SerializeField] float time;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = time;
    }
}
