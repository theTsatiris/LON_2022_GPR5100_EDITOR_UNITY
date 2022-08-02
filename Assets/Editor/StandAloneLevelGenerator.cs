using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StandAloneLevelGenerator : EditorWindow
{
    //TOOL-SPECIFIC VARIABLES
    GameObject TerrainPrefab;
    GameObject GoodCollectiblePrefab;
    GameObject BadCollectiblePrefab;
    float ChanceToSpawnBad;
    int TotalCollectibles;
    float TerrainWidth = 10;
    float TerrainLength = 10;
    float minX;
    float minZ;
    float maxX;
    float maxZ;

    //PRIVATE INTERNAL VARIABLES
    List<GameObject> GoodCollectibles = new List<GameObject>();
    List<GameObject> BadCollectibles = new List<GameObject>();
    GameObject Terrain;
    //GameObject solely for "hanging" and saving the instantiated prefabs as a single "level" prefab
    GameObject Level;

    [MenuItem("Window/Super Level Generator/Level Generator GUI")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StandAloneLevelGenerator));
    }

    private void OnGUI()
    {
        GUIContent label = new GUIContent();

        label.text = "Terrain Prefab";
        label.tooltip = "A cube with fixed height, which will be scaled according to your needs";
        TerrainPrefab = EditorGUILayout.ObjectField(label, TerrainPrefab, typeof(GameObject), true) as GameObject;

        label.text = "Bad Collectible";
        label.tooltip = "The \"bad\" collectible prefab";
        BadCollectiblePrefab = EditorGUILayout.ObjectField(label, BadCollectiblePrefab, typeof(GameObject), true) as GameObject;

        label.text = "Good Collectible";
        label.tooltip = "The \"good\" collectible prefab";
        GoodCollectiblePrefab = EditorGUILayout.ObjectField(label, GoodCollectiblePrefab, typeof(GameObject), true) as GameObject;

        label.text = "Bad collectible probability (%)";
        label.tooltip = "Determines the maximum percentage of bad collectibles in the set of all spawned collectibles";
        ChanceToSpawnBad = EditorGUILayout.Slider(label, ChanceToSpawnBad * 100.0f, 0.0f, 100.0f) / 100.0f;

        label.text = "Total number of collectibles";
        label.tooltip = "Determines the total number of collectibles in the level";
        TotalCollectibles = EditorGUILayout.IntSlider(label, TotalCollectibles, 10, 50);

        GUILayout.BeginHorizontal();

        label.text = "Terrain Width";
        label.tooltip = "Width of the terrain (scales the prefab)";
        TerrainWidth = EditorGUILayout.FloatField(label, TerrainWidth);

        label.text = "Terrain Length";
        label.tooltip = "Length of the terrain (scales the prefab)";
        TerrainLength = EditorGUILayout.FloatField(label, TerrainLength);

        GUILayout.EndHorizontal();

        //*****

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        label.text = "Min X";
        label.tooltip = "Minimum X dimension to spawn collectibles";
        minX = EditorGUILayout.Slider(label, minX, -1 * (TerrainLength / 2.0f), 0.0f);

        label.text = "Max X";
        label.tooltip = "Maximum X dimension to spawn collectibles";
        maxX = EditorGUILayout.Slider(label, maxX, 0.0f, TerrainLength / 2.0f);

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        label.text = "Min Z";
        label.tooltip = "Minimum Z dimension to spawn collectibles";
        minZ = EditorGUILayout.Slider(label, minZ, -1 * (TerrainWidth / 2.0f), 0.0f);

        label.text = "Max Z";
        label.tooltip = "Maximum Z dimension to spawn collectibles";
        maxZ = EditorGUILayout.Slider(label, maxZ, 0.0f, TerrainWidth / 2.0f);

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        //****BUTTONS****

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        label.text = "Spawn Terrain";
        label.tooltip = "Spawns a terrain, given a prefab";
        if (GUILayout.Button(label))
        {
            SpawnTerrain();
        }

        label.text = "Spawn Collectibles";
        label.tooltip = "Spawns collectibles on the generated terrain, given two prefabs and some config";
        if (GUILayout.Button(label))
        {
            SpawnCollectibles();
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        label.text = "Save Configuration";
        label.tooltip = "Saves the current configuration in a .json file";
        if (GUILayout.Button(label))
        {
            SaveConfiguration();
        }

        label.text = "Load Configuration";
        label.tooltip = "Loads a new configuration from a .json file";
        if (GUILayout.Button(label))
        {
            LoadConfiguration();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        label.text = "Save Level Prefab";
        label.tooltip = "Saves a prefab of the entire level";
        if (GUILayout.Button(label))
        {
            SaveLevelPrefab();
        }
    }

    public void SpawnTerrain()
    {
        if (Terrain != null)
            DestroyImmediate(Terrain);

        Terrain = Instantiate(TerrainPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Terrain.transform.localScale = new Vector3(TerrainLength, Terrain.transform.localScale.y, TerrainWidth);

        if (Level == null)
            Level = new GameObject("Level");

        Terrain.transform.parent = Level.transform;
    }

    public void SpawnCollectibles()
    {
        //IF YOU WANT TO ONLY SPAWN COLLECTIBLES IF TERRAIN EXISTS
        /*if (Terrain == null)
        {
            Debug.Log("YOU NEED TO SPAWN THE TERRAIN FIRST!!!");
            return;
        }*/

        foreach (GameObject obj in GoodCollectibles)
        {
            DestroyImmediate(obj);
        }
        foreach (GameObject obj in BadCollectibles)
        {
            DestroyImmediate(obj);
        }
        GoodCollectibles.Clear();
        BadCollectibles.Clear();

        if (Level == null)
            Level = new GameObject("Level");

        for (int i = 0; i < TotalCollectibles; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 randomSpawnPosition = new Vector3(randomX, 1.2f, randomZ);

            float dice = Random.Range(0.0f, 1.0f);
            if (dice < ChanceToSpawnBad)
            {
                GameObject collectible = Instantiate(BadCollectiblePrefab, randomSpawnPosition, Quaternion.identity);
                collectible.transform.parent = Level.transform;
                BadCollectibles.Add(collectible);
            }
            else
            {
                GameObject collectible = Instantiate(GoodCollectiblePrefab, randomSpawnPosition, Quaternion.identity);
                collectible.transform.parent = Level.transform;
                GoodCollectibles.Add(collectible);
            }
        }
    }

    public void SaveConfiguration()
    {
        ConfigData config = new ConfigData();

        config.ChanceToSpawnBad = this.ChanceToSpawnBad;
        config.TotalCollectibles = this.TotalCollectibles;
        config.TerrainWidth = this.TerrainWidth;
        config.TerrainLength = this.TerrainLength;
        config.minX = this.minX;
        config.maxX = this.maxX;
        config.maxZ = this.maxZ;
        config.minZ = this.minZ;

        string jsonString = JsonUtility.ToJson(config);

        string path = EditorUtility.SaveFilePanelInProject("Save configuration data", "", "json", "Choose a suitable name for your configuration file");

        if (!string.IsNullOrEmpty(path))
        {
            System.IO.File.WriteAllText(path, jsonString);
        }
    }

    public void LoadConfiguration()
    {
        string path = EditorUtility.OpenFilePanel("Load configuration data", Application.dataPath, "json");

        if (!string.IsNullOrEmpty(path))
        {
            string jsonString = System.IO.File.ReadAllText(path);

            ConfigData config = JsonUtility.FromJson<ConfigData>(jsonString);

            this.ChanceToSpawnBad = config.ChanceToSpawnBad;
            this.TotalCollectibles = config.TotalCollectibles;
            this.TerrainWidth = config.TerrainWidth;
            this.TerrainLength = config.TerrainLength;
            this.maxZ = config.maxZ;
            this.minZ = config.minZ;
            this.maxX = config.maxX;
            this.minX = config.minX;
        }
    }

    public void SaveLevelPrefab()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save prefab file", "", "prefab", "Select a valid filename for your prefab");

        if (!string.IsNullOrEmpty(path))
        {
            /*//IF WE DON'T WANT TO CONNECT THE PREFAB WITH THE LEVEL GAMEOBJECT IN THE SCENE
            PrefabUtility.SaveAsPrefabAsset(Level, path);*/

            //IF WE WANT TO CONNECT THE PREFAB WITH THE LEVEL GAMEOBJECT IN THE SCENE
            PrefabUtility.SaveAsPrefabAssetAndConnect(Level, path, InteractionMode.UserAction);
        }
    }
}
