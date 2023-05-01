using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkyBoxGen : ScriptableObject
{
    public Material[] skyBoxes;
    //public ParticleSystem weather;
    int rando;

    public void ManageWeather()
    {
        RenderSettings.skybox = skyBoxes[Random.Range(0, skyBoxes.Length)];
        /*ParticleSystem test = Instantiate(weather, FindObjectOfType<PlayerMovement>().transform.position, Quaternion.identity);
        test.transform.parent = FindObjectOfType<PlayerMovement>().transform;
        int rando = Random.Range(0, 1);
        if(rando == 1)
        {
            weather.Play();
        }*/
    }
}
