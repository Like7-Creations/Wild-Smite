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
    bool exitOpen;

    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (timer > .6f)
        {
            if (!init)
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
            exitOpen = true;
            GetComponent<Collider>().enabled = true;
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
            //pauseMenu = FindObjectOfType<PauseMenuController>();
            //pauseMenu.gameObject.SetActive(false);

            //Maybe disable the player as well?
            //Or perhaps the player input. And then reenable it before loading the next scene as a precaution

            if (Time.timeScale == 0)
            {
                pauseMenu = FindObjectOfType<PauseMenuController>();
                pauseMenu.gameObject.SetActive(false);

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
            }

            List<PlayerConfig> pcs = PlayerConfigManager.Instance.GetPlayerConfigs();

            foreach (PlayerConfig pc in pcs)
            {
                PlayerControl pControl = GameObject.Find(pc.Name).GetComponent<PlayerActions>().Pc;
                pControl.enabled = false;
            }

            LevelCompleteUI.gameObject.SetActive(true);
            LevelCompleteUI.ShowSummary();
        }
    }
}
