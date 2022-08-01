using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawningTool))]
public class SpawningToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Show the default behaviour of the inspector
        //base.OnInspectorGUI();

        //Builds default inspector and allows us to change or override certain functionalities
        DrawDefaultInspector();

        SpawningTool spawner = (SpawningTool)target;

        GUIContent label = new GUIContent();
        
        label.text = "Bad collectible probability (%)";
        label.tooltip = "Determines the maximum percentage of bad collectibles in the set of all spawned collectibles";
        spawner.ChanceToSpawnBad = EditorGUILayout.Slider(label, spawner.ChanceToSpawnBad * 100.0f, 0.0f, 100.0f) / 100.0f;

        label.text = "Total number of collectibles";
        label.tooltip = "Determines the total number of collectibles in the level";
        spawner.TotalCollectibles = EditorGUILayout.IntSlider(label, spawner.TotalCollectibles, 10, 50);

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        label.text = "Spawn Terrain";
        label.tooltip = "Spawns a terrain, given a prefab";
        if (GUILayout.Button(label))
        {
            spawner.SpawnTerrain();
        }

        label.text = "Spawn Collectibles";
        label.tooltip = "Spawns collectibles on the generated terrain, given two prefabs and some config";
        if (GUILayout.Button(label))
        {
            spawner.SpawnCollectibles();
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        
        label.text = "Save Configuration";
        label.tooltip = "Saves the current configuration in a .json file";
        if (GUILayout.Button(label))
        {
            spawner.SaveConfiguration();
        }

        label.text = "Load Configuration";
        label.tooltip = "Loads a new configuration from a .json file";
        if (GUILayout.Button(label))
        {
            spawner.LoadConfiguration();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        label.text = "Save Level Prefab";
        label.tooltip = "Saves a prefab of the entire level";
        if (GUILayout.Button(label))
        {
            spawner.SaveLevelPrefab();
        }
    }
}
