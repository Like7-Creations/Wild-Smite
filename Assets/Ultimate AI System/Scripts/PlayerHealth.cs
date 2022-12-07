		///----------------------------\\\				
		//      Ultimate AI System      \\
// Copyright (c) N-Studios. All Rights Reserved. \\
//      https://nikichatv.com/N-Studios.html	  \\
///-----------------------------------------------\\\	



using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ultimate.AI;

public class PlayerHealth : MonoBehaviour
{
	[Tooltip("The max value of the player's total hitpoints.")]
    public float health;
	[Tooltip("This is the empty game object that will be used as a parent for the animation rigging target and will only apply when the AI can see the player.")]
	public Transform IKPosition;
	[Tooltip("The audio source attached to the player.")]
	public AudioSource audioSource;
	[Tooltip("Make an empty game object and position it in the center of your player object.")]
	public Transform playerCenter;

	private void Update()
	{
		if (health < 0f) health = 0f;
		if (health <= 0f) Die();


		if (Input.GetKeyDown(KeyCode.G))  //If the G-Key is pressed the closest AI will take 20 damage. This is just for testing and a better way of calling
		{                   					 
			GetClosestAI().GetComponent<UltimateAI>().TakeDamage(20, this);  // the TakeDamage() function is from the player itself when attacking the AI
																			 // (e.g. if you have a pistol its bullets should call this function whenever
																			 // they collide with the AI).
		}

		else if (Input.GetKeyDown(KeyCode.P) && audioSource != null)
		{
			audioSource.Play();
		}
	}

	private Transform GetClosestAI()
	{
		List<Transform> aiTransforms = new List<Transform>();

		foreach (UltimateAI ai in Object.FindObjectsOfType(typeof(UltimateAI))) aiTransforms.Add(ai.transform);

		Vector3 position = transform.position;
		return aiTransforms.OrderBy(o => (o.transform.position - position).sqrMagnitude).FirstOrDefault();
	}

	public void TakeDamage(float damageToTake)
	{
		if (health - damageToTake > 0f) health -= damageToTake;
		else Die();
	}

	public void Die()
	{
		foreach (UltimateAI ai in Object.FindObjectsOfType(typeof(UltimateAI))) if (ai.players.Contains(this)) ai.players.Remove(this);
		Destroy(this.gameObject);
	}
}
