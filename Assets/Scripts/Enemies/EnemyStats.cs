using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    float myMaxHealth;
    public EnemyStatRange ESR;
    public Image HealthBar;
    public Canvas canvas;

    Enemy_VFXHandler vfx;
    Enemy_SFXHandler sfx;
    AudioSource audioSource;

    NavMeshAgent agent;

    public bool isDead;
    

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        GenerateStatValues(LevelSettings.Difficulty.Easy);
        
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

        // this is for  testing..
        //
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

    public void TakeDamage(float damageToTake)
    {
        // anim.ResetTrigger("GotHit0"/* randomNumber.ToString()*/);
        Vector3 knockBack = transform.position - transform.forward * 0.5f;
        knockBack.y = 0;
        transform.position = knockBack;

        if (sfx.isEnabled)
        {
            var clip = sfx.enemyHitSFX[Random.Range(0, sfx.enemyHitSFX.Length)];
            audioSource.PlayOneShot(clip, 1);
        }

        //damageToTake *= (1 - Defence); //health must be int or float?
        //health -= damageToTake;
        //player = attacker.transform;

        if (vfx.isEnabled)
        {
            vfx.enemyHitVFX.Play();
        }

        //Anmar sfx
        //var clip = sfx.enemyHitSFX[Random.Range(0, sfx.enemyHitSFX.Length)];
        //audioSource.PlayOneShot(clip);

        //var clip = hitSounds[Random.Range(0, hitSounds.Length)]; //A random sound is loaded and the played.
        //audioSource.PlayOneShot(clip);
        //Anmar
        /* var particle = hitParticles[Random.Range(0, hitParticles.Length)];
         particle.Play();*/


        // once animations are ready...
        //int randomNumber = Random.Range(0, hittedAnimations);
        // anim.SetTrigger("GotHit0"/* randomNumber.ToString()*/);

        if (Type == enemyType.Melee || Type == enemyType.Range)
        {
            GetComponent<Wander>().chaseRange = 100;
        }

        //----
        //Gizmos.DrawSphere(transform.position, 1);
        Health -= damageToTake; //The damage given is being taken from the AI's health.
    }

    public IEnumerator DeathWait(float deathTime)
    {
        yield return new WaitForSeconds(deathTime); //A timer with a value of 2f is created and once it is done ticking - bye bye dear AI :D
        GameObject.Destroy(this.gameObject);
    }

    public void Die()
    {
        agent.ResetPath(); //The AI's path is reset.
        agent.enabled = false;
        
        //int randomNumber = Random.Range(0, deathAnimations); //We are getting a random number.
        //anim.SetTrigger("Death" + randomNumber.ToString()); //And here we are creating a string using the number and the word attack. This way a trigger is being formed and sent to the animator.
        /*var clip = deathSounds[Random.Range(0, deathSounds.Length)]; //A random sound is loaded and the played.
        audioSource.PlayOneShot(clip);*/


        var deathclip = sfx.enemyDestroyedSFX[Random.Range(0, sfx.enemyDestroyedSFX.Length)];

        vfx.enemyDeathVFX.transform.parent = null;
        AudioSource deathsfx = vfx.enemyDeathVFX.gameObject.AddComponent<AudioSource>();
        deathsfx.clip = deathclip;
        deathsfx.Play();
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
