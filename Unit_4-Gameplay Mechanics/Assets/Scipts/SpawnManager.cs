using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosoton(), powerupPrefab.transform.rotation);
    }

    void SpawnEnemyWave(int enimiesToSpawn)
    {

        for (int i = 0; i < enimiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosoton(), enemyPrefab.transform.rotation);
        }

    }

    private Vector3 GenerateSpawnPosoton()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosY);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            Instantiate(powerupPrefab, GenerateSpawnPosoton(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(waveNumber);
        }

    }

}
