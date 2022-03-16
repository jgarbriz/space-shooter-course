using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initial player position coordinates
    private static float _PLAYER_INITIAL_POS_X = 0f;
    private static float _PLAYER_INITIAL_POS_Y = -3.5f;
    private static float _PLAYER_INITIAL_POS_Z = 0f;

    private static string _SPAWN_MANAGER_NAME = "Spawn_Manager";

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _laserYOffset = 1.07f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    void Start() {
        // Set initial position
        transform.position = new Vector3(_PLAYER_INITIAL_POS_X, _PLAYER_INITIAL_POS_Y, _PLAYER_INITIAL_POS_Z);

        _spawnManager = GameObject.Find(_SPAWN_MANAGER_NAME).GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("The spawn manager is null.");
        }
    }

    void Update() {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
            FireLaser();
        }
    }

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // Limit the player movement on the Y axis.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), 0);

        // Limit the player movement on the X axis.
        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser() {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserYOffset, 0), Quaternion.identity);
    }

    public void Damage() {
        _lives--;

        if (_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
