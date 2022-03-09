using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private float _spawnTime = 5.0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine() {
        while(true) {
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7.0f, 0f);
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
