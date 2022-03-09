using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private float _spawnTime = 5.0f;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine() {
        while(!_stopSpawning) {
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7.0f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
