using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawningTool : MonoBehaviour
{
    [SerializeField]
    GameObject TerrainPrefab;
    [SerializeField]
    GameObject GoodCollectiblePrefab;
    [SerializeField]
    GameObject BadCollectiblePrefab;

    [HideInInspector]
    public float ChanceToSpawnBad;

    [HideInInspector]
    public int TotalCollectibles;

    [SerializeField]
    float TerrainWidth;
    [SerializeField]
    float TerrainLength;

    [SerializeField]
    float minX;
    [SerializeField]
    float minZ;
    [SerializeField]
    float maxX;
    [SerializeField]
    float maxZ;

    private List<GameObject> GoodCollectibles = new List<GameObject>();
    private List<GameObject> BadCollectibles = new List<GameObject>();
    private GameObject Terrain;

    //GameObject solely for "hanging" and saving the instantiated prefabs as a single "level" prefab
    private GameObject Level;

    // Start is called before the first frame update
    void Start()
    {
        //RUNTIME ONLY!!!!
    }

    // Update is called once per frame
    void Update()
    {
        //RUNTIME ONLY!!!!
    }

    public void SpawnTerrain()
    {
        if(Terrain != null)
            DestroyImmediate(Terrain);

        Terrain = Instantiate(TerrainPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Terrain.transform.localScale = new Vector3(TerrainLength, Terrain.transform.localScale.y, TerrainWidth);

        if (Level == null)
            Level = new GameObject("Level");

        Terrain.transform.parent = Level.transform;

        this.minX = -1 * (TerrainLength / 2.0f);
        this.minZ = -1 * (TerrainWidth / 2.0f);
        this.maxX = TerrainLength / 2.0f;
        this.maxZ = TerrainWidth / 2.0f;
    }

    public void SpawnCollectibles()
    {
        //IF YOU WANT TO ONLY SPAWN COLLECTIBLES IF TERRAIN EXISTS
        /*if (Terrain == null)
        {
            Debug.Log("YOU NEED TO SPAWN THE TERRAIN FIRST!!!");
            return;
        }*/

        foreach(GameObject obj in GoodCollectibles)
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

        if(!string.IsNullOrEmpty(path))
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

        if(!string.IsNullOrEmpty(path))
        {
            /*//IF WE DON'T WANT TO CONNECT THE PREFAB WITH THE LEVEL GAMEOBJECT IN THE SCENE
            PrefabUtility.SaveAsPrefabAsset(Level, path);*/

            //IF WE WANT TO CONNECT THE PREFAB WITH THE LEVEL GAMEOBJECT IN THE SCENE
            PrefabUtility.SaveAsPrefabAssetAndConnect(Level, path, InteractionMode.UserAction);
        }
    }
}
