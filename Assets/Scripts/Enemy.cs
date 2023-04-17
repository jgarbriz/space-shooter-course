using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string LASER_TAG = "Laser";
    private const string ANIM_ON_ENEMY_DESTRUCTION = "OnEnemyDestruction";

    [SerializeField] private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) {
            Debug.LogError("The player is null.");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null) {
            Debug.LogError("The animator is null.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("AudioSource on the enemy is null.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f) {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        
        switch (collider.tag) {
            case PLAYER_TAG: // Enemy collides with the player.
                Player player = collider.GetComponent<Player>();
                if (player != null) {
                    player.Damage();
                }

                _anim.SetTrigger(ANIM_ON_ENEMY_DESTRUCTION);
                _speed = 0f;
                
                _audioSource.Play();

                Destroy(this.gameObject, 2.4f);

                break;

            case LASER_TAG: // Enemy is hit by the laser.
                if (null != _player) {
                    _player.AddScore(10);
                }

                _anim.SetTrigger(ANIM_ON_ENEMY_DESTRUCTION);
                _speed = 0f;

                _audioSource.Play();

                Destroy(collider.gameObject);
                Destroy(this.gameObject, 2.4f);
                
                break;
        }
    }
}
