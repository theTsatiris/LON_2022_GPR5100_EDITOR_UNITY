using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject GoodCollectible;
    [SerializeField]
    GameObject BadCollectible;

    [SerializeField]
    float ChanceToSpawnBad;

    [SerializeField]
    int TotalCollectibles;

    [SerializeField]
    float minX;
    [SerializeField]
    float minZ;
    [SerializeField]
    float maxX;
    [SerializeField]
    float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < TotalCollectibles; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 randomSpawnPosition = new Vector3(randomX, 1.2f, randomZ);

            float dice = Random.Range(0.0f, 1.0f);
            if (dice < ChanceToSpawnBad)
            {
                Instantiate(BadCollectible, randomSpawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(GoodCollectible, randomSpawnPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollectiblesOnLoad()
    {
        GameObject[] GoodCollectibles = GameObject.FindGameObjectsWithTag("GoodCollectible");
        GameObject[] BadCollectibles = GameObject.FindGameObjectsWithTag("BadCollectible");

        foreach (GameObject good in GoodCollectibles)
        {
            Destroy(good);
        }
        foreach (GameObject bad in BadCollectibles)
        {
            Destroy(bad);
        }

        foreach (Vector3 goodPosition in GameData.PLAYER_DATA.goodCollectiblePositions)
        {
            Instantiate(GoodCollectible, goodPosition, Quaternion.identity);
        }
        foreach (Vector3 badPosition in GameData.PLAYER_DATA.badCollectiblePositions)
        {
            Instantiate(BadCollectible, badPosition, Quaternion.identity);
        }
    }
}
