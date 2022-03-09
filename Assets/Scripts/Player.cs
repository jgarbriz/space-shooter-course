using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initial player position coordinates
    private static int PLAYER_INITIAL_POS_X = 0;
    private static int PLAYER_INITIAL_POS_Y = 0;
    private static int PLAYER_INITIAL_POS_Z = 0;

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _laserYOffset = 0.8f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;

    // Start is called before the first frame update
    void Start() {
        // Set initial position
        transform.position = new Vector3(PLAYER_INITIAL_POS_X, PLAYER_INITIAL_POS_Y, PLAYER_INITIAL_POS_Z);
    }

    // Update is called once per frame
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
}
