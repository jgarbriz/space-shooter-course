using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const int TRIPLESHOT_ID = 0;
    private const int SPEEDBOOST_ID = 1;
    private const int SHIELD_ID = 2;

    [SerializeField] private float _speed = 3.0f;
    
    [SerializeField] private int _powerupID;

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
                switch (_powerupID) {
                    case TRIPLESHOT_ID:
                        player.TripleShotActive();
                        break;
                    case SPEEDBOOST_ID:
                        player.SpeedBoostActive();
                        break;
                    case SHIELD_ID:
                        player.ShieldsActive();
                        break;
                    default: 
                        Debug.Log("Default switch powerupID");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
