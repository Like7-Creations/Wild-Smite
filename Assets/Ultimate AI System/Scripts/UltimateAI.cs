///----------------------------\\\				
//      Ultimate AI System      \\
// Copyright (c) N-Studios. All Rights Reserved. \\
//      https://nikichatv.com/N-Studios.html	  \\
///-----------------------------------------------\\\	


using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using Ultimate.AI.Utils;

namespace Ultimate.AI
{
	#region NeededScripts
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(FieldOfView))]
	[RequireComponent(typeof(LocalNavMeshBuilder))]
	#endregion
	public class UltimateAI : MonoBehaviour
	{
		#region Variables
		[Header("Needed Objects")]
		[Space]
		[Tooltip("Assign the player object.")]
		public List<PlayerHealth> players;

		[Space]
		[Header("AI Parameters")]
		[Space]

		[Tooltip("Maximum movement speed when following a path.")]
		[SerializeField]
		private float moveSpeed;
		[HideInInspector]
		private float acceleration;
		[Tooltip("Maximum distance at wich the AI is able to trace and chase the player.")]
		public float chaseRange;
		[Tooltip("How long the AI would stop for before finding another path")]
		[SerializeField]
		private float idlingDuration;
		[Tooltip("Maximum distance at wich the AI is able to attack and deal damage to the player.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		public float attackRange;
		[Tooltip("Maximum distance at wich the AI is able to picк a random point and walk to it.")]
		public float wanderRange;
		[Tooltip("Maximum distance at wich the AI is able to hear the player.")]
#if (UNITY_EDITOR)
		[DrawIf("reactToSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		public float hearingRange;
		[Tooltip("Maximum traverse speed.")]
		public int lookSpeed;
		[Tooltip("Because the pathfinding is baked in real-time this might be a very heavy proccess. Here you can set maximum distance to calculate in real-time when pathfinding.")]
		[SerializeField]
		private Vector3 renderDistance;
		[Tooltip("Maximum health of the AI.")]
		[Range(0, 1000)]
		[SerializeField]
		private int health;
		[Tooltip("If this is toggled a green bounding box will be displayed in the editor window. It represents the render distance of your AI.")]
		[SerializeField]
		private bool showRenderDistance;

		[Space]
		[Header("Booleans")]
		[Space]

#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool chasing;
#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool attacking;
#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool wandering;
#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool wanderPointSet;
#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool hearingPointSet;
#if UNITY_EDITOR
		[ReadOnly]
#endif
		[SerializeField]
		private bool isDead;

		[Space]
		[Header("Attacking")]
		[Space]

		[Tooltip("Choose the type of your AI: \n\n --  Melee makes it attack the player from close range.\n\n --  Ranged will make it shoot the player from longer distance.\n\n -- NPC will make the AI just wander around and when the player is nearby it will stop and look at the player.")]
		public Type type = new Type();
		[Tooltip("Choose what effect to be applied when the AI performs a successful: \n\n -- None means that no effect will be applied.\n\n -- Poison will make the player take more damage apart from the attack damage from time to time.\n\n -- Burning will make the player burn and take" +
			" more damage apart from the attack damage from time to time.\n\n -- Slow will divide the player's speed when propperly attacking. If you choose \"Slow\" make sure to check the comments we've left on line 389, 394 & 395!\n\n -- Freeze will make your player freeze for the given" +
			" amount of time.\n\n -- Stun will make the player's camera really blurry and it would be hard to play until the effect disapears.\n\n -- Health will make the AI regain some health when propperly attacking.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private AttackEffect attackEffect = new AttackEffect();
		[Tooltip("This is the value that will be used when applying effect to the player (deals poisonDamage as poison damage).")]
#if (UNITY_EDITOR)
		[DrawIf("attackEffect", AttackEffect.Poison, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int poisonDamage;
		[Tooltip("This is the value that will be used when applying effect to the player (deals burnDamage as burn damage).")]
#if (UNITY_EDITOR)
		[DrawIf("attackEffect", AttackEffect.Burn, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int burnDamage;
		[Tooltip("This is the amount of healt the AI will regain once it attacks successfully.")]
#if (UNITY_EDITOR)
		[DrawIf("attackEffect", AttackEffect.Health, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int healthRegain;
		[Tooltip("This is how much the player's speed is going to be divided.")]
#if (UNITY_EDITOR)
		[DrawIf("attackEffect", AttackEffect.Slow, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int slownessFactor;
		[Tooltip("This is how many times the effect will be executed when affected.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayCount", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int effectCount;
		[Tooltip("Once you have set up how many times to repeat the effect the script will need to know how much time to wait between executing the effect once again or simply the duration of the first one.")]
#if (UNITY_EDITOR)
		[DrawIf("effectsBigger", true, DrawIfAttribute.ComparisonType.Equals)]
#endif
		[SerializeField]
		private float effectFrequency;
		[Space(15)]

		[Tooltip("If this is on the AI will remain passive until provoked (The AI will not attack until being hit).")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool attackOnProvoke;
		[Tooltip("If this is on the AI will remain provoked even when it loses track of the player. If it's not the once the player is far enought the AI will have to be provoked again.")]
#if (UNITY_EDITOR)
		[DrawIf("attackOnProvoke", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool remainProvoked;
		[Tooltip("If this is on the AI will be able to hear whenever a player produces sounds. Once it hears a player it will pick a random spot around the player based on the hearingOffset and will go there.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool reactToSounds;

		[Space(10)]

		[Tooltip("If this is toggled the once the AI hears a player will not be able to change its path until it reaches the heard point.")]
#if (UNITY_EDITOR)
		[DrawIf("reactToSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool lockToHeardPoint;
		[Tooltip("If this is toggled the AI will change its path everytime it hears a player.")]
#if (UNITY_EDITOR)
		[DrawIf("reactToSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool alwaysGoToHeardPoint;
		[Space(10)]

		[Tooltip("This setting makes the AI go to a heard point based on the percentage below (there is a chance it will hear the player but this won't be always happening).")]
#if (UNITY_EDITOR)
		[DrawIf("reactToSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool goToHeardPointRandomly;
		[Tooltip("This setting makes the AI go to a heard point after multiple sounds were played.")]
#if (UNITY_EDITOR)
		[DrawIf("reactToSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private bool goToHeardPointAfterMultipleSounds;

		[Tooltip("Chances of the AI hearing a player.")]
#if (UNITY_EDITOR)
		[DrawIf("goToHeardPointRandomly", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private double hearingPercentage;
		[Tooltip("This is how many times the AI has to hear a sound before it goes to the player.")]
#if (UNITY_EDITOR)
		[DrawIf("goToHeardPointAfterMultipleSounds", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int neededSoundsToHear;

		[Space(15)]


		[Tooltip("Maximum number of health points to take from the player's total health when successfully attacking.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		public float damageToDeal;
		[Tooltip("This is how fast the AI is able to perform attacks.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private float attackRate;
		[Tooltip("This is how many bullets each ammo clip has.")]
#if (UNITY_EDITOR)
		[DrawIf("type", Type.Ranged, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int clipAmmo;
		[Tooltip("This is how much time is needed to reload the ammo clip when the AI is out of ammo.")]
#if (UNITY_EDITOR)
		[DrawIf("type", Type.Ranged, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private float reloadTime;

		[Space(15)]

		[Tooltip("This is the projectile that the AI will shoot if is set to ranged. Make sure it has rigidbody attached!")]
#if (UNITY_EDITOR)
		[DrawIf("type", Type.Ranged, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private GameObject projectile; //Make sure it has rigidbody attached!
		[Tooltip("This is the place where the projectiles will come from.")]
#if (UNITY_EDITOR)
		[DrawIf("type", Type.Ranged, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private Transform shooter;

		[Space]
		[Header("Visible Adjustments")]
		[Space]

		[Tooltip("This is the animation rigging target itself that will change its parents according to what is happenning.")]
#if (UNITY_EDITOR)
		[DrawIf("verSuitable", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.ReadOnly)]
#endif
		[SerializeField]
		private Transform IKPoint;
		[Tooltip("This is the empty game object that will be used as a parent for the animation rigging target and will only apply when the AI can not see the player.")]
#if (UNITY_EDITOR)
		[DrawIf("verSuitable", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.ReadOnly)]
#endif
		public Transform defaultIKPosition;

		[Space]
		[Header("Animation Settings & Arrays")]
		[Space]

		[Tooltip("The number of attack animations so that the script knows from how many animations it can get a random one.")]
#if (UNITY_EDITOR)
		[DrawIf("canDisplayEffects", true, DrawIfAttribute.ComparisonType.Equals, DrawIfAttribute.DisablingType.DontDraw)]
#endif
		[SerializeField]
		private int attackAnimations;
#if (UNITY_EDITOR)
		[DrawIf("useRagdoll", false, DrawIfAttribute.ComparisonType.Equals)]
#endif
		[Tooltip("The number of death animations so that the script knows from how many animations it can get a random one.")]
		[SerializeField]
		private int deathAnimations;
		[Tooltip("If turned on the AI will generate a Ragdoll and will apply it on death.")]
		public bool useRagdoll;
		[Space]
		[Tooltip("Those are all the particles that will be played when an effect is applied.")]
		[SerializeField]
		private ParticleSystem[] effectParticles;
		[Tooltip("A list of particles to play when something happens. (VFX)")]
		[SerializeField]
		private ParticleSystem[] attackParticles, chaseParticles, wanderParticles, deathParticles;
		[Tooltip("A list of sounds to play when somehting happens. The AI will get a random sound of the whole list and then play it.")]
		[SerializeField]
		private AudioClip[] hitSounds, attackSounds, stepSounds, deathSounds;

		private AudioSource audioSource;
		private Animator anim;
		private NavMeshAgent agent;
		private GameObject objToSpawn;
		private Vector3 wanderPoint;

		[HideInInspector]
		public Transform player, playerCenter, playerIKPosition;

		[HideInInspector]
		public int effectsDealt;
		private int curAmmo, heardSounds, maxHealth;

		private float defaultSpeed;
		private double version;

		[HideInInspector]
		public bool effectsBigger, canDisplayEffects, timeToInfect, infected, bulletHit, inPlayer, firstPathGotten, idling, canDisplayCount, verSuitable, ragdollCreated;
		private bool moving, timeToRotate, canFire = true, inAttackRange, provoked, hearing, canHearAgain = true, canCheckSound = true;

		public enum Type { Melee, Ranged, NPC };
		public enum AttackEffect { None, Poison, Burn, Slow, Freeze, Stun, Health };
		#endregion
		private void Start() //This function will trigger once the game is started.
		{
			wanderPointSet = false; //We are making sure the AI will pick a wander point because if there is not one this is exactly what would happen.
			if (IKPoint != null && playerIKPosition != null) //Then we are checking if we have an assigned IK animation rigging point (look like a ball :D).
			{
				SetIKBools(playerIKPosition, 1f); //And if so we then need to smoothly change its position making the AI look towards the player.
			}

			audioSource = gameObject.GetComponent<AudioSource>(); //On the next 3 rows the script finds the needed components and stores them in a variable to use them more easily.
			anim = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
			defaultSpeed = moveSpeed; //A default speed is being stored so that if you want to change the speed with for example a power-up the AI remembers the original speed.
			curAmmo = clipAmmo; //Current ammo is set to the maximum clip size.
			maxHealth = health; //We are storing the starting health so that we cannot pass it whenever we are adding health using the regeneration effect later in the code.

			if (!attackOnProvoke || type == Type.NPC) provoked = true; //If the AI is set to hostile it will automatically get provoked.
			if (useRagdoll) DisableRagdolls(); //And if you have any ragdolls set they are disabled so they won't gain any unnecessary velocity.
		}

		private void Update() //This function is being triggerd once each frame.
		{
			if (reactToSounds && GetHeardPlayers().Count > 0) //If the AI is set to hear sounds we check whether there are players producing any sounds.
			{
				if (canCheckSound) StartCoroutine(WaitUntillSoundWasReloaded()); //This is a small timer that waits untill the player's sound is changed or played again.

				if (!attacking && !chasing) //If it is not currently focused on a player by chasing or attacking them 
				{
					player = GetHeardPlayers().ToArray()[0].transform; //it will automatically get the closest heard player as a temporary player variable.
				}
				else player = GetClosestPlayer(); //however if it is focused then it will grab the closest player as a temporary variable.
				hearing = true; //Since it has detected players producing sound we need to make the "hearing" bool true.
			}
			else //In case it doesn't hear or simply is not set to react to sounds 
			{
				player = GetClosestPlayer(); //the temporary player is set to the closest one
				hearing = false; //and the bool is set to false.
			}

			if (player == null) //But if there is still a player missing this means they are all dead or destroyed.
			{
				print("No players found!"); //In that case we return and the AI stops working.
				return;
			}

			if (IKPoint != null && player != null) playerIKPosition = player.gameObject.GetComponent<PlayerHealth>().IKPosition;
			if (type == Type.Ranged) playerCenter = player.GetComponent<PlayerHealth>().playerCenter;

			Vector3 distanceToPlayer = player.position - transform.position; //Defining a vector to store the distance between the AI and the player.
			if (distanceToPlayer.magnitude <= chaseRange && provoked && distanceToPlayer.magnitude > attackRange && !isDead && Time.timeScale != 0 && GetComponent<FieldOfView>().canSee)
			{
				if (type != Type.NPC) Chase(); //This checks whether it is time to chase...
				else CheckAttack();
			}
			else if (distanceToPlayer.magnitude < chaseRange && distanceToPlayer.magnitude <= attackRange && !isDead && Time.timeScale != 0 && GetComponent<FieldOfView>().canSee && provoked)
			{
				if (attackOnProvoke && type != Type.NPC) //This checkes if the AI set to attack only when provoked.
				{
					if (provoked) CheckAttack(); //If so and is provoked - it will execute an attack.
				}
				else if (!attackOnProvoke && type != Type.NPC) CheckAttack(); //But if it is set to hostile and is not an NPC we need to get in the attack checker.
				else if (type == Type.NPC) CheckAttack(); //And lastly if it is an NPC we still need to play the attack checker.
			}
			else if (Time.timeScale != 0 && !attacking && !isDead) Wander(); //If it is neither attacking nor dead it will wander around.

			if (health <= 0 && Time.timeScale != 0 && !isDead) Die(); //If our health gets low the AI will die.

			if (type == Type.Ranged) shooter.transform.LookAt(playerCenter); //This keeps the "barrel" of the gun aim towards the player.

			StartCoroutine(Affect()); //This will check if an effect has to be applied.

			DoVarChecks(); //Those 2 functions update the values and set the animation settings.
			DoAnimChecks();
		}

		public void Wander()
		{
			wandering = true; //Bools are being set here.
			attacking = false;
			chasing = false;
			timeToRotate = false;
			inAttackRange = false;

			if (!remainProvoked && attackOnProvoke) provoked = false;

			foreach (ParticleSystem particle in wanderParticles) if (!particle.isPlaying) particle.Play(); //Only needed particles are being played.
			foreach (ParticleSystem particle in attackParticles) if (particle.isPlaying) particle.Stop();
			foreach (ParticleSystem particle in chaseParticles) if (particle.isPlaying) particle.Stop();

			if (IKPoint != null && playerIKPosition != null)
			{
				SetIKBools(defaultIKPosition, 1f); //If we have an IK point set we need to change its position to the default one.
			}

			Vector3 distanceToWalkPoint = transform.position - wanderPoint; //Another vector that stores the distance between the AI and the random point it picked to go to.

			CheckSuitablePoint(); //This function is triggered to check a suitable point to go to.
		}

		public void Chase()
		{
			chasing = true; //Bools are being set.
			attacking = false;
			wandering = false;
			timeToRotate = false;
			inAttackRange = false;

			heardSounds = 0;

			foreach (ParticleSystem particle in wanderParticles) if (particle.isPlaying) particle.Stop(); //Only needed particles are being played.
			foreach (ParticleSystem particle in attackParticles) if (particle.isPlaying) particle.Stop();
			foreach (ParticleSystem particle in chaseParticles) if (!particle.isPlaying) particle.Play();

			if (IKPoint != null && playerIKPosition != null)
			{
				SetIKBools(playerIKPosition, 1f); //If we have an IK point set we need to change its position to the player one.
			}
			agent.SetDestination(player.transform.position); //The nav mesh agent receives a path.

			wanderPointSet = false; //And we reset the wander point we currenlty have.
			hearingPointSet = false;
			CheckSuitablePoint(); //To check for another.
		}

		public void CheckAttack()
		{
			chasing = false; //Bools are being set.
			wandering = false;
			inAttackRange = true;

			heardSounds = 0; //Heard sounds are reset.

			Vector3 distanceToPlayer = player.position - transform.position; //Defining a vector to store the distance between the AI and the player.

			if (type == Type.Melee) //This checks if the AI is set to melee.
			{
				timeToRotate = false; //A simple bool for rotating is set to false.
				Vector3 closeMeleeRange = new Vector3(attackRange, attackRange) * 1 / 2;
				if (distanceToPlayer.magnitude > closeMeleeRange.magnitude) agent.SetDestination(player.transform.position);
				else agent.ResetPath(); timeToRotate = true;

				if (!attacking) //If currently is NOT attacking then attack.
				{
					attacking = true;
					int randomNumber = Random.Range(0, attackAnimations); //We are getting a random number. And here we are creating a string using the number and the word attack.
					anim.SetTrigger("Attack" + randomNumber.ToString());  //This way a trigger is being formed and sent to the animator.
					return;
				}
			}
			else if (type == Type.Ranged) //This checks if the AI is set to ranged.
			{
				timeToRotate = true; //A simple bool for rotating is set to true.
				Vector3 closeShootRange = new Vector3(attackRange, attackRange) * 1 / 3; //A close range is defined and if the player is in that range the AI should stop moving.
				if (distanceToPlayer.magnitude <= closeShootRange.magnitude) agent.ResetPath(); if (distanceToPlayer.magnitude > closeShootRange.magnitude) agent.SetDestination(player.transform.position);
				StartCoroutine(AttackPlayer()); //Right after the attack method is being executed.
				attacking = true; //And so is that bool.
			}

			else if (type == Type.NPC) //This checks if the AI is set to npc.
			{
				timeToRotate = true; //A simple bool for rotating is set to true.
				Vector3 closeNpcRange = new Vector3(chaseRange, chaseRange); //A close range is defined and if the player is inside that range the AI will should stop moving.
				if (distanceToPlayer.magnitude <= closeNpcRange.magnitude) agent.ResetPath();
				if (distanceToPlayer.magnitude > closeNpcRange.magnitude) agent.SetDestination(player.transform.position);
				StartCoroutine(NPCWait());
			}

			if (IKPoint != null && playerIKPosition != null)
			{
				SetIKBools(playerIKPosition, 1f); //If we have an IK point set we need to change its position to the player one.
			}

			CheckSuitablePoint(); //Here we are checking for a new point to go to.
		}
		public IEnumerator AttackPlayer()
		{
			if (inAttackRange) //This checks whether the player is inside the attack sphere.
			{
				if (IKPoint != null && playerIKPosition != null) //If the look aim is assigned:
				{
					SetIKBools(playerIKPosition, 1f); //If we have an IK point set we need to change its position to the player one.
				}

				if (type == Type.Melee) ///If the AI is set to melee the MeleeAttack function will be called.
				{
					MeleeAttack();
					effectsDealt = 0;
					infected = false; //The effects are reset.
					timeToInfect = true;
					effectsDealt = 0;
				}
				else if (type == Type.Ranged)
				{
					StartCoroutine(WaitUntilImpact()); //We are waiting for our bullet to hit a player.
					StartCoroutine(RangedAttack());
				}

				attacking = true;
				yield return new WaitForSeconds(attackRate); //A timer is created with the attackRate time.
				attacking = false;
			}
		}

		public void MeleeAttack()
		{
			player.GetComponent<PlayerHealth>().health -= damageToDeal; //Here the given damage is taken from the player's health when successfully attacking.

			var clip = attackSounds[Random.Range(0, attackSounds.Length)]; //A random sound is loaded and the played.
			audioSource.PlayOneShot(clip);

			agent.SetDestination(player.transform.position); //Here we set our player's position as a destination for the AI.

			foreach (ParticleSystem particle in wanderParticles) if (particle.isPlaying) particle.Stop(); //Only needed particles are being played.
			foreach (ParticleSystem particle in attackParticles) particle.Play();
			foreach (ParticleSystem particle in chaseParticles) if (particle.isPlaying) particle.Stop();

			wanderPointSet = false; //And the waner point is reset once again.
			hearingPointSet = false;
		}

		IEnumerator RangedAttack()
		{
			if (canFire) //If the AI is allowed to fire:
			{
				if (curAmmo <= 0) //and the current ammo is lowere or equal to 0 the AI will have to reload.
				{
					StartCoroutine(Reload(reloadTime)); //The Reload funtion is called.
				}
				else //If the AI has ammo:
				{
					curAmmo--; //The ammo is being lowered with 1 bullet for each shot.
					var clip = attackSounds[Random.Range(0, attackSounds.Length)]; //A random sound is loaded and the played.
					audioSource.PlayOneShot(clip);

					Rigidbody rb = Instantiate(projectile, shooter.transform.position, Quaternion.identity).GetComponent<Rigidbody>(); //The rigidbody of the projectile is being accessed.
					rb.GetComponent<Projectile>().ai = gameObject;
					rb.AddForce(transform.forward * 10f, ForceMode.Impulse); //The projectiles get pushed so that they can move using physics force.

					foreach (ParticleSystem particle in wanderParticles) if (particle.isPlaying) particle.Stop(); //Only needed particles are being played.
					foreach (ParticleSystem particle in attackParticles) particle.Play();
					foreach (ParticleSystem particle in chaseParticles) if (particle.isPlaying) particle.Stop();
					canFire = false;
					yield return new WaitForSeconds(attackRate); //This makes the delay between each shot (attack rate).
					canFire = true;
				}
			}

			wanderPointSet = false; //Oh forgot what that does... I guess I have used it too less times :P
			hearingPointSet = false;
		}

		public IEnumerator Affect() //This is the function that is responsible for applying effects.
		{
			if (type == Type.Ranged) bulletHit = false;
			if ((attackEffect == AttackEffect.Poison) && !infected && effectsDealt < effectCount && timeToInfect)
			{  //If the effect is poison:
				if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
				effectsDealt++;

				player.GetComponent<PlayerHealth>().health -= poisonDamage; //Then the damage of the poison is subtracted from the player's health.

				infected = true;
				yield return new WaitForSeconds(effectFrequency); //A timer is created to wait until the next effect is applied.
				infected = false;
			}
			else if (attackEffect == AttackEffect.Burn && !infected && effectsDealt < effectCount && timeToInfect)
			{   //If the effect is burning:
				if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
				effectsDealt++;

				player.GetComponent<PlayerHealth>().health -= burnDamage; //Then the damage of the fire is subtracted from the player's health.

				infected = true;
				yield return new WaitForSeconds(effectFrequency); //A timer is created to wait until the next effect is applied.
				infected = false;
			}
			else if ((attackEffect == AttackEffect.Health) && !infected && effectsDealt < effectCount && timeToInfect)
			{   //If the effect is regeneration:
				if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
				effectsDealt++;

				if (health + healthRegain <= maxHealth) health += healthRegain; else { health = maxHealth; } //The health of the AI is being regenerated.

				infected = true;
				yield return new WaitForSeconds(effectFrequency); //A timer is created to wait until the next effects is applied.
				infected = false;
			}
			else if ((attackEffect == AttackEffect.Slow) && !infected && effectsDealt < effectCount && timeToInfect)
			{   //If the effect is set to slowing:
				if (slownessFactor != 0) //And the value is not equal to 0 (You cannot divide by 0!):
				{
					if (player.GetComponent<FPSController>()) //Remember to change "FPSController" to your controller's class name!!
					{
						if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
						effectsDealt++;
						if (!infected)
						{
							player.GetComponent<FPSController>().walkingSpeed /= slownessFactor;   //Remember to change "FPSController" to your controller's class name!!
							player.GetComponent<FPSController>().runningSpeed /= slownessFactor;  //Also you should replace "walkingSpeed" & "runningSpeed" with what 																	
						}                                                                        //you call the player's speed in your controller!
						infected = true;
						yield return new WaitForSeconds(effectFrequency);

						player.GetComponent<FPSController>().walkingSpeed *= slownessFactor;  //Remember to change "FPSController" to your controller's class name!!
						player.GetComponent<FPSController>().runningSpeed *= slownessFactor; //Also you should replace "walkingSpeed" & "runningSpeed" with what 
																							 //you call the player's speed in your controller!
						infected = false;
					}
					else Debug.LogError("   Since your player's controller is NOT called \"FPSController\", please either rename your controller to \"FPSController\" or change " +
						"on line 516, 522, 523, 528, 529 in the \"Ultimate AI\" script where it says \"FPSController\" to your controller's name!\n It is also necessary to change the vars to what yours are!");
				}
				else Debug.LogError("You cannot divide the player's speed by 0!");
			}
			else if ((attackEffect == AttackEffect.Freeze) && !infected && effectsDealt < effectCount && timeToInfect)
			{   //If the effect is set to freeze:
				if (player.GetComponent<FPSController>()) //Remember to change "FPSController" to your controller's class name!!
				{
					if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
					effectsDealt++;

					player.GetComponent<FPSController>().enabled = false;

					infected = true;
					yield return new WaitForSeconds(effectFrequency);

					player.GetComponent<FPSController>().enabled = true;    //with your controller's variables!		

					infected = false;
				}
				else Debug.LogError("   Since your player's controller is NOT called \"FPSController\", please either change the name of your controller to \"FPSController\" or change " +
					"on line 540, 545, 550 in the \"Ultimate AI\" script where it says \"FPSController\" to your controller's name!");
			}
			else if ((attackEffect == AttackEffect.Stun) && !infected && effectsDealt < effectCount && timeToInfect)
			{   //If the effect is set to stunning:
				if (effectParticles != null) foreach (ParticleSystem effectParticle in effectParticles) effectParticle.Play(); //The particles are played.
				effectsDealt++;

				foreach (Camera cam in Object.FindObjectsOfType(typeof(Camera))) cam.clearFlags = CameraClearFlags.Nothing; //And then the blurry effects is being applied.

				infected = true;
				yield return new WaitForSeconds(effectFrequency); //A timer is created and once it is done ticking the effect is being removed.
				foreach (Camera cam in Object.FindObjectsOfType(typeof(Camera))) cam.clearFlags = CameraClearFlags.Skybox;
				infected = false;
			}
		}

		public void CheckSuitablePoint() //This is the function that takes care of locating random points for our AI to go to.
		{
			if ((transform.position - wanderPoint).magnitude < 1f) wanderPointSet = false;
			if (!reactToSounds) //First off we are checking whether we need to execute the rest of the code bellow. If it doesn't react to sounds it is pointless to do so.
			{
				Vector3 distanceToWalkPoint = transform.position - wanderPoint; //Then we are defining a vector to store the distance between the current point and our AI.
				if (!wanderPointSet || (distanceToWalkPoint.magnitude < 1f)) //And it that's the case we will just check if the AI is missing a random point and will pick one using the function "GetWanderPoint".
				{
					GetWanderPoint();
					return;
				}
			}
			//However if the guy who's using this system decided they need their AI to react to sounds... well then they probably hate me :D
			if (lockToHeardPoint) //We are checking to see if the AI will lock its pathing when it has a point.
			{
				if (!goToHeardPointRandomly && !goToHeardPointAfterMultipleSounds) //First we need to see if the user wants natural hearing - no randomness.
				{
					Vector3 distanceToWalkPoint = transform.position - wanderPoint; //Then we are defining a vector to store the distance between the current point and our AI.
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						hearingPointSet = false; //If so we are resetting the path (I think we have never done that before? Just tried to be funny, sorry :) ).
						wanderPointSet = false;

						if (hearing) GetHeardPoint(); //And if we are hearing something we need to get a heard point.
						else GetWanderPoint(); //Else we pick a ranom wander point.
					}

					if (!wanderPointSet && !hearing) //If we don't have a point assigned and are not hearing anything we get a new one.
					{
						GetWanderPoint();
					}

					else if (hearing && !hearingPointSet) //But if that's not the case we are going to pick a heard point.
					{
						GetHeardPoint();
					}
				}

				else if (goToHeardPointAfterMultipleSounds && !goToHeardPointRandomly) //If the AI is set to go to a heard point after multiple sounds
				{
					bool willHear = false;

					if (heardSounds >= neededSoundsToHear) //We need to check whether there are enough sounds heard already.
					{
						willHear = true;
					}

					Vector3 distanceToWalkPoint = transform.position - wanderPoint; //Another vector for distance.
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						hearingPointSet = false; //I don't know what that is - you tell me :P
						wanderPointSet = false;

						if (hearing && willHear && !hearingPointSet) //If we are hearing and there is not currently a heard point set and we have enough sounds we get a heard point.
						{
							GetHeardPoint();
						}
						else GetWanderPoint(); //But if those conditions are not met we get a simple wander point.
					}

					if (!wanderPointSet && !hearing) //If we don't have a point assigned we get a new one.
					{
						GetWanderPoint();
					}
					if (hearing && canHearAgain && !hearingPointSet)  //If we are hearing and there is not currently a heard point set and we have enough sounds we get a heard point.
					{
						if (willHear)
						{
							GetHeardPoint();
						}
					}
				}

				else if (!goToHeardPointAfterMultipleSounds && goToHeardPointRandomly) //In case the AI is set to go to a heard point based on percentage the code below is executed.
				{
					Vector3 distanceToWalkPoint = transform.position - wanderPoint;
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						hearingPointSet = false; //Yeah you guessed it... we reset the current point.
						wanderPointSet = false;

						if (hearing && WillHear() && !hearingPointSet) //And then we see if we had enough luck for the AI to get a heard point.
						{
							GetHeardPoint();
						}
						else GetWanderPoint();
					}

					if (!wanderPointSet && !hearing) //If we don't have a point assigned we get a new one.
					{
						GetWanderPoint();
					}
					if (hearing && canHearAgain && !hearingPointSet) //And same thing here - we see if we had enough luck for the AI to get a heard point.
					{
						if (WillHear())
						{
							GetHeardPoint();
						}
					}
				}
			}
			else if (alwaysGoToHeardPoint) //However the AI might not be set to lock its pathing. In that case the code below will get executed.
			{
				if (!goToHeardPointRandomly && !goToHeardPointAfterMultipleSounds) //Same checks here. If it is set to hear as a real creature would.
				{
					Vector3 distanceToWalkPoint = transform.position - wanderPoint; //We need a vector once again.
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						if (hearing && canHearAgain) GetHeardPoint(); //Now if we are hearing we are getting a new heard point.
						else GetWanderPoint(); //Otherwise we don't.
					}

					if (!wanderPointSet && !hearing) //If there aren't any sounds we can hear we then get a normal wander point.
					{
						GetWanderPoint();
					}
					if (hearing && canHearAgain) //But in case it hears something we get a heard point.
					{
						GetHeardPoint();
					}
				}

				else if (goToHeardPointAfterMultipleSounds && !goToHeardPointRandomly) //If we need multiple sounds
				{
					bool willHear = false;

					if (heardSounds == neededSoundsToHear) //We check if we have them.
					{
						heardSounds = 0;
						willHear = true;
					}

					Vector3 distanceToWalkPoint = transform.position - wanderPoint; //What's that? Ohh a vector...
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						if (hearing && canHearAgain) //If we are hearing and have enough sounds heard we get a heard point.
						{
							if (willHear) GetHeardPoint();
						}
						else //But else we get a wander point.
						{
							GetWanderPoint();
						}
					}

					if (!wanderPointSet && !hearing) //If we don't have a point set and are not hearing we get a wander point.
					{
						GetWanderPoint();
					}
					if (hearing && canHearAgain) //But if we are hearing and have heard enough sounds we simply get a heard point.
					{
						if (willHear) GetHeardPoint();
					}
				}

				else if (!goToHeardPointAfterMultipleSounds && goToHeardPointRandomly) //Now for those who like lotteries 
				{
					Vector3 distanceToWalkPoint = transform.position - wanderPoint; //They will definitely need a vector to store the distance.
					if ((distanceToWalkPoint.magnitude < 1f) || (!wanderPointSet && !hearingPointSet)) //A simle If statement that checks if the AI rached the given random point 
					{
						if (hearing && canHearAgain && WillHear()) GetHeardPoint(); //If the AI is hearing and won the lottery by calling the bool "WillHear()" we get a heard point.
						else //Otherwise we dont...
						{
							GetWanderPoint();
						}
					}

					if (!wanderPointSet && !hearing) GetWanderPoint(); //Another check for a wander point

					if (hearing && canHearAgain && WillHear()) GetHeardPoint(); //Just like this one which is for a heard point.
				}
			}
			else //But if none of the above are true we get a simple wander point.
			{
				if (!wanderPointSet) GetWanderPoint();
			}
		}

		public IEnumerator HearAgain() //This is a coroutine that waits for the player's sound to be modified by either being changed or replayed.
		{
			canHearAgain = false;
			AudioSource audio = player.GetComponent<AudioSource>();
			var clip = audio.clip;
			yield return new WaitUntil(() => audio.time == 0.0f || !audio.isPlaying);
			canHearAgain = true;
		}

		public IEnumerator WaitUntillSoundWasReloaded() //And this is a coroutine that waits untill the player's sound is modified.
		{
			if (canCheckSound)
			{
				AudioSource audio = player.GetComponent<AudioSource>();
				Vector3 distanceToPlayer = transform.position - player.position;

				if (canCheckSound && distanceToPlayer.magnitude <= hearingRange) //If it is the AI counts its sounds as heard.
				{
					heardSounds++;
					canCheckSound = false;
				}

				var clip = audio.clip;

				StartCoroutine(WaitForSoundCheck());
				yield return new WaitUntil(() => audio.time == 0.0f || !audio.isPlaying); //Another check for the player's sound.
			}
		}

		public IEnumerator WaitForSoundCheck() //This is the timer that waits a second and prevents the AI from pathing based on hearing before it ends.
		{
			yield return new WaitForSecondsRealtime(1f);
			canCheckSound = true;
		}

		public IEnumerator WaitUntilImpact() //We need to make sure that the bullet has hit a player and this function waits for exactly that to happen.
		{
			yield return new WaitUntil(() => bulletHit);
			effectsDealt = 0;
			infected = false; //The effects are resetted.
			timeToInfect = true;
			effectsDealt = 0; //The effects are resetted once again.
		}

		public bool WillHear() //This is the boolean that checks whether you won the lotterai... get it? LotterAI... like ai and lottery...
		{
			float i = Random.Range(0.0f, 100.0f); //We get a random number between 0 and 100
			bool b = i < hearingPercentage; //And if it is smaller than the hearing percentage then you won the lotterai.
			StartCoroutine(HearAgain());
			return b;
		}

		void SetIKBools(Transform parent, float speed) //This makes the IK point smoothly move its position.
		{
			if (parent == defaultIKPosition) inPlayer = false;
			else inPlayer = true;

			if (inPlayer) IKPoint.position = Vector3.MoveTowards(IKPoint.position, playerIKPosition.position, Time.deltaTime * 15f);
			else IKPoint.position = Vector3.MoveTowards(IKPoint.position, defaultIKPosition.position, Time.deltaTime * 15f);
		}

		IEnumerator SmoothTranlation(Vector3 target, float speed)
		{
			while (IKPoint.transform.position != target)
			{
				IKPoint.transform.position = Vector3.Lerp(IKPoint.transform.position, target, Time.deltaTime * speed);
				yield return null;
			}
		}

		public void GetWanderPoint() //This is a more complicated function. It is responsible for getting a random wandering spot.
		{
			if (idling) return;
			StartCoroutine(AwaitWanderPoint());
		}

		IEnumerator AwaitWanderPoint()
		{
			if (attacking || chasing) yield break;

			idling = true;
			if (firstPathGotten) yield return new WaitForSeconds(idlingDuration);

			Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
			randomDirection += transform.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
			wanderPoint = hit.position;
			agent.SetDestination(wanderPoint);
			wanderPointSet = true;

			firstPathGotten = true;
			idling = false;
		}

		public void GetHeardPoint() //This is a more complicated function. It is responsible for getting a random hearing spot.
		{
			if (attacking || chasing) return;

			StartCoroutine(HearAgain());

			heardSounds = 0;

			float accuracy = ((transform.position - player.transform.position).sqrMagnitude - chaseRange) / (hearingRange - chaseRange);
			if (accuracy < 1) accuracy = 1;

			Vector3 randomDirection = Random.insideUnitSphere * (accuracy / Random.Range(1, 5));
			randomDirection += player.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, accuracy, 1);
			if (hit.position.x != Mathf.Infinity && hit.position.y != Mathf.Infinity && hit.position.z != Mathf.Infinity) wanderPoint = hit.position;
			else
			{
				GetHeardPoint();
				return;
			}
			agent.SetDestination(wanderPoint);
			hearingPointSet = true;
			wanderPointSet = true;
		}


		public void DisableRagdolls() //This function disables every collider in the object other than the important ones and generates a special physics material for all of our limbs.
		{
			PhysicMaterial mat = new PhysicMaterial();
			mat.dynamicFriction = 0.2f;
			mat.staticFriction = 0.2f;
			mat.bounciness = 0;

			foreach (Rigidbody c in gameObject.GetComponentsInChildren<Rigidbody>())
			{
				if (c.gameObject != gameObject)
				{
					c.useGravity = false;
				}
			}

			foreach (CapsuleCollider c in gameObject.GetComponentsInChildren<CapsuleCollider>())
			{
				if (c.gameObject != gameObject)
				{
					c.material = mat;
					c.isTrigger = true;
				}
			}

			foreach (SphereCollider c in gameObject.GetComponentsInChildren<SphereCollider>())
			{
				if (c.gameObject != gameObject)
				{
					c.material = mat;
					c.isTrigger = true;
				}
			}

			foreach (BoxCollider c in gameObject.GetComponentsInChildren<BoxCollider>())
			{
				if (c.gameObject != gameObject)
				{
					c.material = mat;
					c.isTrigger = true;
				}
			}
		}

		public void EnableRagdolls() //Every not-so-special collider is activated as well as the rigidbody it has.
		{
			GetComponent<CapsuleCollider>().isTrigger = true;
			anim.enabled = false;

			foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
			{
				rb.velocity = Vector3.zero;
			}

			foreach (Collider c in GetComponentsInChildren<Collider>())
			{
				c.isTrigger = false;
			}

			foreach (Rigidbody c in gameObject.GetComponentsInChildren<Rigidbody>())
			{
				if (c.gameObject != gameObject)
				{
					c.useGravity = true;
				}
			}
		}

		public IEnumerator Reload(float time) //This is the reload function.
		{
			canFire = false;
			yield return new WaitForSeconds(time);
			canFire = true;
			curAmmo = clipAmmo;
		}

		public IEnumerator DeathWait(float deathTime)
		{
			yield return new WaitForSeconds(deathTime); //A timer with a value of 2f is created and once it is done ticking - bye bye dear AI :D
			GameObject.Destroy(this.gameObject);
		}

		public void Die()
		{
			isDead = true;

			moveSpeed = 0; //And both the rotation and moving speed are set to 0.
			lookSpeed = 0;
			agent.speed = 0;
			agent.angularSpeed = 0;

			agent.ResetPath(); //The AI's path is reset.
			agent.enabled = false;

			foreach (ParticleSystem particle in wanderParticles) if (particle.isPlaying) particle.Stop(); //Only needed particles are being played.
			foreach (ParticleSystem particle in attackParticles) if (particle.isPlaying) particle.Stop();
			foreach (ParticleSystem particle in chaseParticles) if (particle.isPlaying) particle.Stop();
			foreach (ParticleSystem particle in deathParticles) if (!particle.isPlaying) particle.Play();

			if (!useRagdoll)
			{
				int randomNumber = Random.Range(0, deathAnimations); //We are getting a random number.
				anim.SetTrigger("Death" + randomNumber.ToString()); //And here we are creating a string using the number and the word attack. This way a trigger is being formed and sent to the animator.
				var clip = deathSounds[Random.Range(0, deathSounds.Length)]; //A random sound is loaded and the played.
				audioSource.PlayOneShot(clip);
				StartCoroutine(DeathWait(2f));
			}
			else
			{
				if (!ragdollCreated)
				{
					Debug.LogError("There is not a created ragdoll yet!");
					return;
				}
				EnableRagdolls();
				StartCoroutine(DeathWait(5f));
			}
		}

		public void TakeDamage(int damageToTake, PlayerHealth attacker)
		{
			if (attackOnProvoke) provoked = true;

			player = attacker.transform;

			var clip = hitSounds[Random.Range(0, hitSounds.Length)]; //A random sound is loaded and the played.
			audioSource.PlayOneShot(clip);

			health -= damageToTake; //The damage given is being taken from the AI's health.
		}

		public void Footstep()
		{
			var clip = stepSounds[Random.Range(0, stepSounds.Length)]; //A random sound is loaded and the played.
			audioSource.PlayOneShot(clip);
		}

		public void DoVarChecks() //This funcion simply updates the variables.
		{
			if (health < 0) health = 0; //This prevents the health from going below zero.

			if (timeToRotate)
			{
				Vector3 direction = (player.position - transform.position).normalized; //This tells the AI how to properly rotate when set to ranged.
				Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
				transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
			}
		}

		IEnumerator NPCWait() //The function waits until the player is not in the NPC's range so that it can continue its work... or should I say path?
		{
			yield return new WaitUntil(() => (player.position - transform.position).magnitude > chaseRange);
			wanderPointSet = false;
			CheckSuitablePoint();
		}

		public void DoAnimChecks() //This funcion simply updates the animator values.
		{
			float velocity = agent.velocity.magnitude;
			if (velocity > 0f) moving = true;
			else moving = false;

			anim.SetBool("Moving", moving);
		}

		public void WipeRagdolls() //This deletes all the ragdolls the AI has.
		{
			foreach (Collider c in GetComponentsInChildren<Collider>())
			{
				if (c.gameObject != gameObject)
				{
					DestroyImmediate(c);
				}
			}

			foreach (CharacterJoint cj in GetComponentsInChildren<CharacterJoint>())
			{
				DestroyImmediate(cj);
			}

			foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
			{
				if (rb.gameObject != gameObject)
				{
					DestroyImmediate(rb);
				}
			}

			ragdollCreated = false;

			Debug.LogWarning("All ragdoll settings were reset!");
		}

		private Transform GetClosestPlayer()
		{
			List<Transform> playerTransforms = new List<Transform>();

			foreach (PlayerHealth pl in players) playerTransforms.Add(pl.transform);

			Vector3 position = transform.position; //Every player is added in a special list and then we order the list based on the distance between each player and the AI.
			return playerTransforms.OrderBy(o => (o.transform.position - position).sqrMagnitude).FirstOrDefault();
		}

		private List<PlayerHealth> GetHeardPlayers() //Same thing for the heard players.
		{
			List<Transform> playerTransforms = new List<Transform>();

			foreach (PlayerHealth pl in players)
			{
				Vector3 distance = pl.transform.position - transform.position; //The only difference is that we need to see if the player we heard is actually in range.
				if (distance.magnitude < hearingRange) playerTransforms.Add(pl.transform);
			}

			Vector3 position = transform.position; //Every player is added in a special list and then we order the list based on the distance between each player and the AI.
			playerTransforms.OrderBy(o => (o.transform.position - position).sqrMagnitude).FirstOrDefault();

			List<PlayerHealth> heardPlayers = new List<PlayerHealth>();

			for (int i = playerTransforms.Count - 1; i >= 0; i--)
			{
				PlayerHealth pl = playerTransforms[i].GetComponent<PlayerHealth>();
				if (pl.GetComponent<AudioSource>().isPlaying) heardPlayers.Add(pl);
			}

			return heardPlayers.OrderBy((d) => (d.transform.position - position).sqrMagnitude).ToList();
		}

		void OnDrawGizmosSelected() //This function visualises all vectors and speheres in the editor window. NONE OF THAT IS VISIBLE WHEN PLAYING!
		{
			if (showRenderDistance)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube(transform.position, renderDistance);
			}
		}

		private void OnDrawGizmos()
		{
			string ver = Application.unityVersion.Substring(0, 6);
			version = double.Parse(ver);
			verSuitable = version >= 2019.1;
			effectsBigger = effectCount > 0;
			if (type != Type.NPC) canDisplayEffects = true;
			else canDisplayEffects = false;
			if (attackEffect != AttackEffect.None) canDisplayCount = true;
			else canDisplayCount = false;

			if (GetComponent<NavMeshAgent>().path.corners.Length != 0)
			{
				for (int i = 0; i < GetComponent<NavMeshAgent>().path.corners.Length - 1; i++)
				{
					Debug.DrawLine(GetComponent<NavMeshAgent>().path.corners[i], GetComponent<NavMeshAgent>().path.corners[i + 1], Color.red);
				}
			}
		}

		private void OnValidate()
		{
			var ai = this;

			bool isCreated;
			if (GameObject.Find("Pathfinding")) isCreated = true;
			else isCreated = false;

			if (!isCreated)
			{
				GameObject pathObj = new GameObject("Pathfinding");
				pathObj.transform.position = Vector3.zero;
				pathObj.AddComponent<NavMeshSurface>();
				isCreated = true;
			}

			string ver = Application.unityVersion.Substring(0, 6);
			ai.version = double.Parse(ver);

			if (ai.type == UltimateAI.Type.NPC)
			{
				ai.attackEffect = UltimateAI.AttackEffect.None;
				ai.reactToSounds = false;
			}

			if (!ai.attackOnProvoke) ai.remainProvoked = false;

			if (!ai.useRagdoll && ai.ragdollCreated) ai.WipeRagdolls();

			if (!ai.reactToSounds)
			{
				ai.hearingPercentage = 0;
				ai.lockToHeardPoint = false;
				ai.alwaysGoToHeardPoint = false;
				ai.goToHeardPointRandomly = false;
				ai.goToHeardPointAfterMultipleSounds = false;
			}

			if (ai.goToHeardPointRandomly) ai.goToHeardPointAfterMultipleSounds = false;
			if (ai.goToHeardPointAfterMultipleSounds) ai.goToHeardPointRandomly = false;
			if (ai.lockToHeardPoint) ai.alwaysGoToHeardPoint = false;
			if (ai.alwaysGoToHeardPoint) ai.lockToHeardPoint = false;
			if (ai.attackEffect == UltimateAI.AttackEffect.None) ai.effectCount = 0;
			if (ai.type == UltimateAI.Type.NPC) ai.attackOnProvoke = false;
			if (ai.attackEffect == UltimateAI.AttackEffect.Stun || ai.attackEffect == UltimateAI.AttackEffect.Freeze) ai.effectCount = 1;

			ai.gameObject.GetComponent<NavMeshAgent>().speed = ai.moveSpeed;
			ai.gameObject.GetComponent<NavMeshAgent>().angularSpeed = ai.lookSpeed;
			ai.gameObject.GetComponent<NavMeshAgent>().acceleration = ai.moveSpeed * 1.25f;
			ai.gameObject.GetComponent<FieldOfView>().viewRadius = ai.chaseRange;

			ai.gameObject.GetComponent<LocalNavMeshBuilder>().m_Tracked = ai.gameObject.transform;
			ai.gameObject.GetComponent<LocalNavMeshBuilder>().m_Size = ai.renderDistance;

			float highestRange = Mathf.Max(ai.wanderRange, ai.chaseRange, ai.attackRange);

			if ((highestRange > ai.renderDistance.x) || (highestRange > ai.renderDistance.y) || (highestRange > ai.renderDistance.z))
			{
				Debug.LogError("\nThe render distance of your ai (" + "\"" + ai.name + "\"" + ") must be bigger than all the ranges it has!"
					+ " Your render distance is: " + ai.renderDistance + " while the biggest range is: " + highestRange + "!");
			}
		}
	}

#if UNITY_EDITOR
	public class ReadOnlyAttribute : PropertyAttribute { }

	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property,
												GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		public override void OnGUI(Rect position,
								   SerializedProperty property,
								   GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
#endif
}