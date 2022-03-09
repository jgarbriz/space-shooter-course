using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string LASER_TAG = "Laser";

    [SerializeField]
    private float _speed = 4.0f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f) {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        
        switch (collider.tag) {
            case PLAYER_TAG: // Enemy collides with the player.
                Player player = collider.GetComponent<Player>();
                if (player != null) {
                    player.Damage();
                }

                Destroy(this.gameObject);
                
                break;

            case LASER_TAG: // Enemy is hit by the laser.
                Destroy(collider.gameObject);
                Destroy(this.gameObject);
                
                break;
        }
    }
}
