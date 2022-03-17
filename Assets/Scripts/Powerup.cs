using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private float _speed = 3.0f;

    private float _yOffScreen = -4.5f;

    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _yOffScreen) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == PLAYER_TAG) {
            Player player = collider.transform.GetComponent<Player>();
            if (player != null) {
                player.TripleshotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
