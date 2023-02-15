using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Attack), true)]
public class AttackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Attack swipe = (Attack)target;
        if(GUILayout.Button("Activate Attack"))
        {
            swipe.AttackType();
        }
    }
}
