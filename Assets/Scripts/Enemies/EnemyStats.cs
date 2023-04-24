using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    PlayerStats hitPlayer;

    float myMaxHealth;
    public EnemyStatRange ESR;
    public Image HealthBar;
    public Canvas canvas;

    Enemy_VFXHandler vfx;
    Enemy_SFXHandler sfx;
    AudioSource audioSource;

    NavMeshAgent agent;

    public DMGEffect dmgEffect;

    public bool isDead;

    Animator anim;
    

    public enum enemyType
    {
        Melee,
        Range,
        Tank,
        Boss,
    }
    public enemyType Type;


    public float Health;
    public float Speed;
    public float attackRange;
    public float MATK;
    public float MDEF;
    public float MCDN;
    public float RATK;
    public float RDEF;
    public float RCDN;

    public float generalCDN;

    private void Awake()
    {
        anim =  GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GenerateStatValues(LevelSettings.Difficulty.Easy);
        GetComponent<Orbit>().originalSpeed = agent.speed;
        
        myMaxHealth = Health;

        vfx = GetComponent<Enemy_VFXHandler>();
        sfx = GetComponent<Enemy_SFXHandler>();

        switch (Type)
        {
            case enemyType.Melee:
                generalCDN = MCDN;
                break;
            
            case enemyType.Range:
                generalCDN = RCDN;
                break;

            case enemyType.Tank:
                generalCDN= RCDN;
                break;
        }
    }

    void Update()
    { 
        float thisInt = Health / myMaxHealth;
        HealthBar.fillAmount = thisInt;
        canvas.transform.LookAt(Camera.main.transform);
        if(Health <= 0)
        {
            Die();
        }

        float forwardDirection = Vector3.Dot(transform.forward, new Vector3(agent.velocity.x, 0, agent.velocity.z));
        float rightDirection = Vector3.Dot(transform.right, new Vector3(agent.velocity.x, 0f, agent.velocity.z));

        anim.SetFloat("X", rightDirection, 0.05f, Time.deltaTime);
        anim.SetFloat("Y", forwardDirection, 0.05f, Time.deltaTime);
    }

    public void AllocateStats()
    {
        //thisEnemy.health = Health;
        //thisEnemy.moveSpeed = Speed;
        if(Type == enemyType.Melee)
        {
            //thisEnemy.damageToDeal = MATK;
            //thisEnemy.Defence = MDEF;
            //thisEnemy.attackRate = MCDN;
        }
        if (Type == enemyType.Range)
        {
           // thisEnemy.damageToDeal = RATK;
           // thisEnemy.Defence = RDEF;
            //thisEnemy.reloadTime = RCDN;
        }
        if (Type == enemyType.Tank)
        {
            
        }

    }

    public void GenerateStatValues(LevelSettings.Difficulty difficulty)
    { 
        Health = ESR.AllocateStats(ESR.Health, difficulty);
        Speed = ESR.AllocateStats(ESR.SPD, difficulty);
        agent.speed = Speed;
        MATK = ESR.AllocateStats(ESR.MATK, difficulty);
        MDEF = ESR.AllocateStats(ESR.MDEF, difficulty);
        MCDN = ESR.AllocateStats(ESR.MCDN, difficulty);
        RATK = ESR.AllocateStats(ESR.RATK, difficulty);
        RDEF = ESR.AllocateStats(ESR.RDEF, difficulty);
        RCDN = ESR.AllocateStats(ESR.RCDN, difficulty);
        AllocateStats();
    }

    public void TakeDamage(float damageToTake, PlayerStats player)
    {
        hitPlayer = player;

        // anim.ResetTrigger("GotHit0"/* randomNumber.ToString()*/);
        Vector3 knockBack = transform.position - transform.forward * 0.5f;
        knockBack.y = 0;
        transform.position = knockBack;

        if (sfx.isEnabled)
        {
            var clip = sfx.enemyHitSFX[Random.Range(0, sfx.enemyHitSFX.Length)];
            audioSource.PlayOneShot(clip, 1);
        }

        DMGEffect dmg = Instantiate(dmgEffect, transform);
        dmg.transform.LookAt(Camera.main.transform);
        dmg.amount = damageToTake;

        //damageToTake *= (1 - Defence); //health must be int or float?
        //health -= damageToTake;
        //player = attacker.transform;

        if (vfx.isEnabled)
        {
            vfx.enemyHitVFX.Play();
        }

        if (Type == enemyType.Melee || Type == enemyType.Range)
        {
            GetComponent<Wander>().chaseRange = 100;
        }

        Health -= damageToTake;
    }

    public IEnumerator DeathWait(float deathTime)
    {
        yield return new WaitForSeconds(deathTime); //A timer with a value of 2f is created and once it is done ticking - bye bye dear AI :D
        GameObject.Destroy(this.gameObject);
    }

    public void Die()
    {
        hitPlayer.SetEnemyCount(GetComponent<EnemyStats>().ESR.enemyType);

        agent.enabled = false;

        var deathclip = sfx.enemyDestroyedSFX[Random.Range(0, sfx.enemyDestroyedSFX.Length)];

        vfx.enemyDeathVFX.transform.parent = null;
        AudioSource deathsfx = vfx.enemyDeathVFX.gameObject.AddComponent<AudioSource>();
        //deathsfx.clip = deathclip;
        deathsfx.PlayOneShot(deathclip, 0.2f);
        vfx.enemyDeathVFX.Play();
        isDead = true;
        StartCoroutine(DeathWait(0f));
        

        /* public float AllocateStats(Vector2 valu)
         {
             float percentage = ESR.GeneratePosInRange();
             float stat = 0;

             stat = valu.x + (valu.y - valu.x) * percentage;
             stat = Mathf.RoundToInt(stat);
             Debug.Log(stat);
             return stat;
         }*/
    }

    public void playerTakeDamage(float damage)
    {
        // check if player is in range;
        GetComponent<StateManager>().currentState.chosenPlayer.GetComponent<PlayerActions>().TakeDamage(damage);
    }

}
