using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineOfLife))]
public class LineOfLifeTool : Editor
{
    private string[] visu = { "OOO", "OOX", "OXO", "OXX", "XOO", "XOX", "XXO", "XXX" };

    private SerializedProperty rulesP;
    private int rules;

    void OnEnable()
    {
        rulesP = serializedObject.FindProperty("setup");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Rules setup");
        EditorGUILayout.Space();

        rules = rulesP.intValue;
        bool[] _rules = new bool[8];
        for(int i = 0; i < 8; i++)
        {
            _rules[i] = (rules & (1 << i)) != 0;
        }

        int res = 0;
        for (int i = 7; i >= 0; i--)
        {

            _rules[i] = EditorGUILayout.Toggle(visu[7 - i], _rules[i]);

            res = res << 1;
            if (_rules[i])
            {
                res++;
            }
        }
        rules = res;

        rulesP.intValue = rules;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Other option");
        EditorGUILayout.Space();
        serializedObject.FindProperty("delayTurn").floatValue = EditorGUILayout.FloatField("Delay", serializedObject.FindProperty("delayTurn").floatValue);
        EditorGUILayout.Space();
        serializedObject.FindProperty("debug").boolValue = EditorGUILayout.Toggle("Debug", serializedObject.FindProperty("debug").boolValue);

        serializedObject.ApplyModifiedProperties();
    }
}
