using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip[] tracks;
    public float maxVol;
    public float fadeDuration;

    public bool loop;
    public bool playOnStart;
    public bool fadeOnStart;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        source.clip = tracks[Random.Range(0, tracks.Length)];

        if (loop)
            source.loop = true;

        if (playOnStart)
            source.Play();
        if (fadeOnStart)
            StartCoroutine(FadeInTrack());
    }

    IEnumerator FadeInTrack()
    {
        float elapsedTime = 0;
        float startVolume = 0;
        float endVolume = maxVol;
        source.volume = startVolume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            source.volume = Mathf.Lerp(startVolume, endVolume, t);
            yield return null;
        }

        source.volume = endVolume;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutTrack());
        source.Stop();
    }

    public void FadeIn()
    {
        source.Play();
        StartCoroutine(FadeInTrack());
    }

    IEnumerator FadeOutTrack()
    {
        float elapsedTime = 0;
        float startVolume = maxVol;
        float endVolume = 0;
        source.volume = startVolume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            source.volume = Mathf.Lerp(startVolume, endVolume, t);
            yield return null;
        }

        source.volume = endVolume;
    }
}
