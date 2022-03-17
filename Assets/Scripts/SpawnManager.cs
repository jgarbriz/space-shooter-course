using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;

    private float _xOffScreen = 8.0f;
    private float _yOffScreen = 7.0f;
    private float _spawnTime = 5.0f;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        while(!_stopSpawning) {
            Vector3 spawnPosition = new Vector3(getRandomXScreenPosition(), _yOffScreen, 0.0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        while (!_stopSpawning) {
            Vector3 spawnPosition = new Vector3(getRandomXScreenPosition(), _yOffScreen, 0.0f);
            Instantiate(_tripleShotPowerupPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }

    private float getRandomXScreenPosition() {
        return Random.Range(-(_xOffScreen), _xOffScreen);
    }
}
