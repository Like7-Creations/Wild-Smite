		///----------------------------\\\				
		//      Ultimate AI System      \\
// Copyright (c) N-Studios. All Rights Reserved. \\
//      https://nikichatv.com/N-Studios.html	  \\
///-----------------------------------------------\\\	



using UnityEngine;
using UnityEditor;
using Ultimate.AI;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI() //This visualises the FOV inside the editor window. NONE OF THAT IS VISIBLE IN THE PLAY WINDOW!
	{
        FieldOfView fow = (FieldOfView)target;
        UltimateAI ai = fow.gameObject.GetComponent<UltimateAI>();

        if (fow.viewAngle < 360)
		{
            Handles.color = Color.white;
            Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
            Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

            Handles.color = Color.white; //Chase Range
            var chaseRange = ai.chaseRange;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewAngle, chaseRange);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * chaseRange);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * chaseRange);

            Handles.color = Color.yellow; //Attack Range
            if (ai.type != UltimateAI.Type.NPC)
            {
                var attackRange = ai.attackRange;
                Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewAngle, attackRange);
                Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * attackRange);
                Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * attackRange);
            }

            Handles.color = Color.magenta; //Wander Range
            var wanderRange = ai.wanderRange;
            Handles.DrawWireDisc(fow.transform.position, new Vector3(0, 1, 0), wanderRange);

            Handles.color = Color.cyan; //Hearing Range
            var hearingRange = ai.hearingRange;
            Handles.DrawWireDisc(fow.transform.position, new Vector3(0, 1, 0), hearingRange);
        }
        else
		{
            Handles.color = Color.white;
            Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
            Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

            Handles.color = Color.white; //Chase Range
            var chaseRange = ai.chaseRange;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewAngle, chaseRange);

            Handles.color = Color.yellow; //Attack Range
            if (ai.type != UltimateAI.Type.NPC)
            {
                var attackRange = ai.attackRange;
                Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewAngle, attackRange);
            }

            Handles.color = Color.magenta; //Wander Range
            var wanderRange = ai.wanderRange;
            Handles.DrawWireDisc(fow.transform.position, new Vector3(0, 1, 0), wanderRange);

            Handles.color = Color.cyan; //Hearing Range
            var hearingRange = ai.hearingRange;
            Handles.DrawWireDisc(fow.transform.position, new Vector3(0, 1, 0), hearingRange);
        }
        


        foreach (Transform visibleTarget in fow.visibleTargets)
		{
            Handles.color = Color.green;
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
		}
    }
}
