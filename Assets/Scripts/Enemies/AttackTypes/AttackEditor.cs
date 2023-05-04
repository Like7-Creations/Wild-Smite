using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
[CustomEditor(typeof(Attack), true)]
public class AttackEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Attack attack = (Attack)target;

        if (GUILayout.Button("Activate Attack"))
        {
            attack.startAttack();
        }
    }
}
#endif
