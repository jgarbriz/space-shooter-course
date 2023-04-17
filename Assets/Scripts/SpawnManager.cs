using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefabs;

    private float _xOffScreen = 8.0f;
    private float _yOffScreen = 7.0f;
    private float _spawnTime = 5.0f;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(3.0f);

        while(!_stopSpawning) {
            Vector3 spawnPosition = new Vector3(getRandomXScreenPosition(), _yOffScreen, 0.0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        yield return new WaitForSeconds(3.0f);
        
        while (!_stopSpawning) {
            Vector3 spawnPosition = new Vector3(getRandomXScreenPosition(), _yOffScreen, 0.0f);
            int randomPowerup = Random.Range(0, _powerupPrefabs.Length);
            Instantiate(_powerupPrefabs[randomPowerup], spawnPosition, Quaternion.identity);
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
