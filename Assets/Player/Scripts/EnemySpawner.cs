using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //for spawning at spawnPoints
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs; //array cuz many types of enemy


    //for random spawn 
    [SerializeField]
    private GameObject dummy;
    public float spawnInterval = 3.5f;
    public float startRange;
    public float endRange;

    void Start()
    {
        //StartCoroutine(spawnEnemy(spawnInterval,dummy));
        StartCoroutine(spawnEnemyAtSpawnPoint(spawnInterval));
    }

    //random spawn
    private IEnumerator spawnEnemy(float spawnInterval, GameObject dummy)
    {
        yield return new WaitForSeconds(spawnInterval);
        GameObject dummyIns = Instantiate(dummy, new Vector3(Random.Range(startRange,endRange),0,0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, dummy));
    } 

    //spawn Points
    private IEnumerator spawnEnemyAtSpawnPoint(float spawnInterval)
    {
        yield return new WaitForSeconds(spawnInterval);
        int randomEnemy = Random.Range(0,enemyPrefabs.Length);
        int randomSpawnPoint = Random.Range(0,spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyPrefabs[randomEnemy], spawnPoints[i].position, transform.rotation); //to spawn at all points
        }
        //Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation); //for random spawn point
        //StartCoroutine(spawnEnemyAtSpawnPoint(spawnInterval));
    }
}
