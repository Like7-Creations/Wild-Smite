		///----------------------------\\\				
		//      Ultimate AI System      \\
// Copyright (c) N-Studios. All Rights Reserved. \\
//      https://nikichatv.com/N-Studios.html	  \\
///-----------------------------------------------\\\	



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ultimate.AI.Utils
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Projectile : MonoBehaviour
    {
        [Header("Parameters")]
        [Space]

        [Tooltip("This is the particle system (visual effect) that will be played if the projectile explodes.")]
        public ParticleSystem explosionEffect;
        [HideInInspector]
        public LayerMask playerMask;
        [Tooltip("The obstacle layer needs to be assigned here.")]
        public LayerMask obstacleMask;

        [Header("Statistics")]
        [Space]
        [Range(0f, 1f)]
        [Tooltip("This is how high the projectile will bounce once hitting another object.")]
        public float bounciness;
        [Tooltip("If this is marked the projectile will use gravity.")]
        public bool useGravity;

        [Header("Damage Variables")]
        [Space]
        [Tooltip("If this is set to true the player will take damage only once, because when the projectile bounces around it is possible to hit the player multiple times. (RECOMMENDED)")]
        public bool damageOnce;
        [Tooltip("If this is set to true the projectile will get destroyed when it collides.")]
        public bool destroyOnCollision;
        [Tooltip("The maximum damage to take from the playe's health when exploding near him.")]
        public int explosionDamage;
        [Tooltip("The maximum distance at which the explode force is working.")]
        public float explosionRange;
        [Tooltip("The maximum strenght of the explosion.")]
        public float explosionForce;

        [Header("Lifetime")]
        [Space]
        [Tooltip("Maximum possible collisions. If the collisions are beyond the maximum range the projectile will explode and get destroyed.")]
        public int maxCollisions;
        [Tooltip("Maximum time for the projectile to live. If the time passes the maximum range the projectile will explode and get destroyed.")]
        public float maxLifetime;
        [Tooltip("If this is checked the projectile will explode one it collides with anything OTHER THAN ANOTHER BULLET.")]
        public bool explodeOnTouch;


        [HideInInspector]
        public GameObject ai;
        private Rigidbody rb;
        int collisions; //This is just an int that stores the total collisions.
        PhysicMaterial physics_mat;
        private bool neverTouchedPlayer = true;

        private void Start()
        {
            rb = GetComponent<Rigidbody>(); //The rigidbody is being set in a variable.
            Setup(); //Setup function is called.
        }

        private void Update()
        {
            //When to explode:
            if (collisions > maxCollisions) Destroy(gameObject, 0.05f); //If the total collisions are more than the maximum the projectile will explode.

            //Count down lifetime
            maxLifetime -= Time.deltaTime; //The max lifetime is set as a timer and gets lower with every frame.
            if (maxLifetime <= 0) Destroy(gameObject, 0.05f); //If the time runs out the projectile will get destroyed.

        }

        private void Explode()
        {
            //Instantiate explosion
            if (explosionEffect != null)
            {
                var particle = Instantiate(explosionEffect, transform.position, Quaternion.identity); //If there is explosion the particle effect will be cloned.
                particle.Play();                                                                     //and then played.
                Destroy(gameObject, 0.01f);
                Destroy(particle.gameObject, 1f); //After 1 second after the particles are played will get destroyed. IF YOU ARE USING
                                                  //LONGER VFX MAKE SURE TO ADJUST THE TIME (1f) TO WHATEVER YOU LIKE.
            }

            //Check for enemies 
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, obstacleMask); //An array is created storing all the enemies (players) and obstacles with rigidbody.
            for (int i = 0; i < enemies.Length; i++) //In case of explosion all the obstacles will be blown.
            {
                if (enemies[i].GetComponent<Rigidbody>())
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            collisions++; //On each collision the collisions count goes up.

            var col = collision.gameObject;
            if (damageOnce) //If the projectile collides with a player it will deal damage to him according to how are its settings set:
            {
                if (col.GetComponent<PlayerHealth>())
                {
                    if (neverTouchedPlayer)
                    {
                        collision.gameObject.GetComponent<PlayerHealth>().health -= ai.GetComponent<UltimateAI>().damageToDeal;
                        ai.GetComponent<UltimateAI>().bulletHit = true;
                        ai.GetComponent<UltimateAI>().Affect();
                        neverTouchedPlayer = false;
                    }
                }
            }
            else //However this will damage the player every time they collide with each other.
            {
                if (col.GetComponent<PlayerHealth>()) col.GetComponent<PlayerHealth>().health -= ai.GetComponent<UltimateAI>().damageToDeal;
            }

            if (explodeOnTouch) Explode(); //If explosions are turned on an explosion will be triggered.

            if (destroyOnCollision) Destroy(gameObject, 0.01f);
        }

        private void Setup() //In order to make all the functions working we'll need a custom physics material.
        {                   //This method is goint to make one for us.
            physics_mat = new PhysicMaterial();
            physics_mat.bounciness = bounciness;
            physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
            physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
            GetComponent<SphereCollider>().material = physics_mat;

            rb.useGravity = useGravity;
        }

        private void OnDrawGizmosSelected() //This method just visualises the explosion range in the editor window. NONE OF THAT IS VISIBLE IN PLAY MODE!
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }
    }
}
