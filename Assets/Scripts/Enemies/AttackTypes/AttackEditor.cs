using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Attack), true)]
public class AttackEditor : Editor
{   
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Attack attack = (Attack)target;

        if (GUILayout.Button("Activate Attack"))
        {
            attack.attackLogic();
        }
    }
}
#endif
