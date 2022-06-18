using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject dummy;
    public float spawnInterval = 3.5f;
    public float startRange;
    public float endRange;

    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval,dummy));
    }

    private IEnumerator spawnEnemy(float spawnInterval, GameObject dummy)
    {
        yield return new WaitForSeconds(spawnInterval);
        GameObject dummyIns = Instantiate(dummy, new Vector3(Random.Range(startRange,endRange),0,0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, dummy));
    } 
}
