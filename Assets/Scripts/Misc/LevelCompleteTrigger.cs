using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{

    public LevelCompletion LevelCompleteUI;

    [SerializeField] PauseMenuController pauseMenu;
    int totalEnemies;


    [Range(0, 1)]
    public float killPercent;
    public int killAmount;
    public int remaningEnemies;
    public int kills;

    public ParticleSystem RedParticle;
    public ParticleSystem GreenParticle;

    //DelayedStart
    float timer;
    bool init;

    private void Update()
    {
        if (timer > .6f && !init)
        {
            pauseMenu = FindObjectOfType<PauseMenuController>();
            totalEnemies = FindObjectOfType<Spawner>().spawnedEnemies;

            killAmount = Mathf.RoundToInt(totalEnemies * killPercent);
            if (!RedParticle.isPlaying)
                RedParticle.Play();
            if (GreenParticle.isPlaying)
                GreenParticle.Stop();

            init = true;
        }
        else
            timer += Time.deltaTime;

        if (init)
        {
            remaningEnemies = FindObjectsOfType<EnemyStats>().Length;
            kills = totalEnemies - remaningEnemies;
            CheckConditions();
        }
    }

    bool CheckConditions()
    {
        remaningEnemies = FindObjectsOfType<EnemyStats>().Length;

        if (kills >= killAmount)
        {
            if (RedParticle.isPlaying)
                RedParticle.Stop();
            if (!GreenParticle.isPlaying)
                GreenParticle.Play();
            return true;
        }
        else
        {
            if (!RedParticle.isPlaying)
                RedParticle.Play();
            if (GreenParticle.isPlaying)
                GreenParticle.Stop();
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CheckConditions())
            {
                pauseMenu = FindObjectOfType<PauseMenuController>();
                pauseMenu.gameObject.SetActive(false);

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }

                LevelCompleteUI.gameObject.SetActive(true);
                LevelCompleteUI.ShowSummary();
            }
        }
    }
}
