using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float spawnDelay = 10f;
    public GameObject monsterPrefab;
    public GameObject[] monsterPrefabs;
    public Transform[] spawnPoint;
    public bool enableSpawn;
    
    float minMapSizex;
    float maxMapSizex;
    float camera_miny;
    float camera_maxy;

    private void Start()
    {
        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        camera_maxy = 0.5f * height;
        camera_miny = -camera_maxy;
        maxMapSizex = 0.5f * width;
        minMapSizex = -maxMapSizex;

        InvokeRepeating("SpawnMonster", 0, 10);
    }

    private void Update()
    {
    }

    private void SpawnMonster()
    {
        float randomX = Random.Range(minMapSizex, maxMapSizex);
        if (enableSpawn)
        {
            monsterPrefabs = GameObject.FindGameObjectsWithTag("Monster");
            if (monsterPrefabs.Length < 20)
            {
                GameObject enemy = (GameObject)Instantiate(monsterPrefab, new Vector3(randomX, camera_maxy, 0f), Quaternion.identity);
            }
        }
    }
}
