using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawn))]
public class SpawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //target: inspected object
        Spawn spawn = (Spawn)target;

        if (GUILayout.Button("Spawn"))
        {
            spawn.spawnEnemy();
        }
    }
}
