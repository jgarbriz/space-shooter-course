using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const string SCORE_TEXT = "Score: ";

    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;

    private GameManager _gameManager;

    void Start()
    {
        _scoreText.text = SCORE_TEXT + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (null == _gameManager) {
            Debug.LogError("Game Manager is null.");
        }
    }

    public void UpdateScore(long playerScore) {
        _scoreText.text = SCORE_TEXT + playerScore.ToString();
    }

    public void UpdateLives(int currentLives) {
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0) {
            GameOverSequence();
        }
    }

    IEnumerator GameOverFlickerRoutine() {
        while(true) {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    void GameOverSequence() {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
    }
}
