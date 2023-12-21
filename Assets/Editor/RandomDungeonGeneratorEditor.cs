using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AbstractDungeonGen), true)]
public class RandomDungeonGeneratorEditor : Editor

{
    AbstractDungeonGen generator;
    private void Awake()
    {
        generator = (AbstractDungeonGen)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear"))
        {
            generator.Clear();
        }
    }
}