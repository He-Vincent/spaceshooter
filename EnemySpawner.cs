using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // public GameObject enemyPrefab;
    public GameObject[] enemyPool;
    public float spawnRangeX;
    public float spawnRangeY;

    public Transform player;
    // public float[] spawnLocations;
    // int nextSpawnLocationIndex = 0;
    public float spawnLocation;
    public float spawnOffset;
  

    public int enemyPerWave;
    public Transform bound;

    int waveCount;
    int enemyRemaining;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > spawnLocation)
        {
            
            // spawning enemies x amount of time per wave
            for (int i = 0; i < enemyPerWave; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnRangeX, spawnRangeX),
                    Random.Range(-spawnRangeY, spawnRangeY) + 
                    spawnLocation + 10,
                    0);

                int randomEnemyNum = Random.Range(0, enemyPool.Length);
                GameObject enemyToSpawn = enemyPool[randomEnemyNum];
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

                enemyRemaining++;

            }


            Vector3 newBoundPos = bound.position;
            newBoundPos.y = spawnLocation;
            bound.position = newBoundPos;

            Vector3 newBoundScale = bound.localScale;
            newBoundScale.y = spawnRangeY + 20;
            bound.localScale = newBoundScale;

            spawnLocation += spawnOffset;

            waveCount++;

            if(waveCount % 5 == 0)
            {
                enemyPerWave++;
            }
        }
    }

    //called by the Enemy script each time an enemy dies
    public void EnemyDeath()
    {
        enemyRemaining--;

        if (enemyRemaining <= 0)
        {
            enemyRemaining = 0;

            Vector3 newBoundScale = bound.localScale;
            newBoundScale.y = spawnOffset + spawnRangeY + 25;
            bound.localScale = newBoundScale;
        }
    }
    
}