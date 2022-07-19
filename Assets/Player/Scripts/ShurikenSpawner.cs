using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] shuriken; 

    public Transform[] spawnPoints;
    public float spawnInterval = 3.5f;
    
    void Start()
    {
        StartCoroutine(spawnShurikenAtSpawnPoint(spawnInterval));
    }

    // private IEnumerator spawnEnemy(float spawnInterval, GameObject shuriken)
    // {
    //     yield return new WaitForSeconds(spawnInterval);
    //     GameObject shurikenIns = Instantiate(shuriken, new Vector3(Random.Range(startRange,endRange),0,0), Quaternion.identity);
    //     StartCoroutine(spawnEnemy(spawnInterval, shuriken));
    // } 
    private IEnumerator spawnShurikenAtSpawnPoint(float spawnInterval)
    {
        yield return new WaitForSeconds(spawnInterval);
        int randomEnemy = Random.Range(0,shuriken.Length);
        int randomSpawnPoint = Random.Range(0,spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(shuriken[randomEnemy], spawnPoints[i].position, transform.rotation); //to spawn at all points
        }
        //Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation); //for random spawn point
        //StartCoroutine(spawnEnemyAtSpawnPoint(spawnInterval));
    }
}
