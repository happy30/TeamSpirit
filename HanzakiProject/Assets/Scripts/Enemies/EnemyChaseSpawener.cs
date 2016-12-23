using UnityEngine;
using System.Collections;

public class EnemyChaseSpawener : MonoBehaviour {
    public GameObject enemy;
    public float maySpawn;
    public float spawnRate;
    public Transform spawnPos;
    public float minSpawnRate;
    public float maxSpawnRate;
    public Transform target;


    void Update()
    {
        Spawner();
    }

    void Spawner()
    {
        if (maySpawn > spawnRate)
        {
            GameObject prefab = Instantiate(enemy, spawnPos.position, Quaternion.identity) as GameObject;
            prefab.GetComponent<EnemyChasing>().Running(target);
            maySpawn = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
        else
        {
            maySpawn += Time.deltaTime;
        }
    }
}
