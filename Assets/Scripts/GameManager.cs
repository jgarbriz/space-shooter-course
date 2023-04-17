using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int GAME_SCENE_INDEX = 1;

    [SerializeField] private bool _isGameOver = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver) {
            SceneManager.LoadScene(GAME_SCENE_INDEX);
        }
    }

    public void GameOver() {
        _isGameOver = true;
    }
}
