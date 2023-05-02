using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
}
