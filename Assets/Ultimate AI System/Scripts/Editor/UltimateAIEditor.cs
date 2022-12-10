		///----------------------------\\\				
		//      Ultimate AI System      \\
// Copyright (c) N-Studios. All Rights Reserved. \\
//      https://nikichatv.com/N-Studios.html	  \\
///-----------------------------------------------\\\	



using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Collections.Generic;
using Ultimate.AI;

[CustomEditor(typeof(UltimateAI))]
public class UltimateAIEditor : Editor //This is the editor of the Ultimate AI System. Nothing special actually.
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		UltimateAI ai = (UltimateAI)target;

		UpdateAll(ai);

		if (!ai.useRagdoll) return;

		if (GUILayout.Button("Build Ragdoll"))
		{
			if (ai.useRagdoll)
			{
				RagdollMaker ragdollMaker = ScriptableWizard.DisplayWizard<RagdollMaker>("Set up your Ragdoll");
			}
			else
			{
				Debug.LogWarning("You haven't turned the Ragdoll option on!");
			}
		}

		if (!ai.ragdollCreated) return;

		if (GUILayout.Button("Reset my Ragdoll"))
		{
			ai.WipeRagdolls();
		}
	}

	void UpdateAll(UltimateAI ai)
	{
		if (!Application.isPlaying)
		{
			LayerMask obstacles = ai.gameObject.GetComponent<FieldOfView>().obstacleMask;
			Collider[] colls = Physics.OverlapSphere(ai.transform.position, Mathf.Infinity, obstacles);
			foreach (Collider collider in colls)
			{
				if (!collider.GetComponent<NavMeshSourceTag>())
				{
					collider.gameObject.AddComponent<NavMeshSourceTag>();
				}
			}
		}
	}
}