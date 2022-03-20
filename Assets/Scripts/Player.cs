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

    [SerializeField] private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldsVisualizer;

    private float _laserYOffset = 1.07f;

    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = 0.0f;

    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private float _tripleShotDurationTime = 5.0f;
    private float _speedBoostDurationTime = 5.0f;
    private bool _isShieldActive = false;

    [SerializeField] private long _score = 0;

    private UIManager _uiManager;

    void Start() {
        // Set initial position
        transform.position = new Vector3(_PLAYER_INITIAL_POS_X, _PLAYER_INITIAL_POS_Y, _PLAYER_INITIAL_POS_Z);

        _spawnManager = GameObject.Find(_SPAWN_MANAGER_NAME).GetComponent<SpawnManager>();
        if (null == _spawnManager) {
            Debug.LogError("The spawn manager is null.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (null == _uiManager) {
            Debug.LogError("The UIManager is null.");
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

        if (_isTripleShotActive) {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } else {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserYOffset, 0), Quaternion.identity);
        }
    }

    public void Damage() {
        if (_isShieldActive) {
            _isShieldActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(_tripleShotDurationTime);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive() {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostCoroutine());
    }

    IEnumerator SpeedBoostCoroutine() {
        yield return new WaitForSeconds(_speedBoostDurationTime);
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive() {
        _isShieldActive = true;
        _shieldsVisualizer.SetActive(true);
    }

    public void AddScore(int points) {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
