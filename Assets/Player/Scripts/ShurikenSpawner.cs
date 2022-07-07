using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject shuriken;
    public float spawnInterval = 7f;
    public float startRange;
    public float endRange;

    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval,shuriken));
    }

    private IEnumerator spawnEnemy(float spawnInterval, GameObject shuriken)
    {
        yield return new WaitForSeconds(spawnInterval);
        GameObject shurikenIns = Instantiate(shuriken, new Vector3(Random.Range(startRange,endRange),0,0), Quaternion.identity);
        StartCoroutine(spawnEnemy(spawnInterval, shuriken));
    } 
}
