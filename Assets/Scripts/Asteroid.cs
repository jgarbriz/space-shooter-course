using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private const string LASER_TAG = "Laser";

    [SerializeField] private float _rotateSpeed = 19.0f;

    [SerializeField] private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    void Start() 
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (null == _spawnManager) {
            Debug.LogError("The spawn manager is null");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        switch (collider.tag) {
            case LASER_TAG:
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _spawnManager.StartSpawning();

                Destroy(collider.gameObject);
                Destroy(this.gameObject, 0.2f);
                
                break;

            default:
                break;
        }
    }
}
