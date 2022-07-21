using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTool : MonoBehaviour
{
    [SerializeField]
    GameObject GoodCollectible;
    [SerializeField]
    GameObject BadCollectible;

    [HideInInspector]
    public float ChanceToSpawnBad;

    [HideInInspector]
    public int TotalCollectibles;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollectibles()
    {
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

        for (int i = 0; i < TotalCollectibles; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 randomSpawnPosition = new Vector3(randomX, 1.2f, randomZ);

            float dice = Random.Range(0.0f, 1.0f);
            if (dice < ChanceToSpawnBad)
            {
                BadCollectibles.Add(Instantiate(BadCollectible, randomSpawnPosition, Quaternion.identity));
            }
            else
            {
                GoodCollectibles.Add(Instantiate(GoodCollectible, randomSpawnPosition, Quaternion.identity));
            }
        }
    }
}
