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

    void Start()
    {
        _scoreText.text = SCORE_TEXT + 0;
    }

    public void UpdateScore(long playerScore) {
        _scoreText.text = SCORE_TEXT + playerScore.ToString();
    }

    public void UpdateLives(int currentLives) {
        _livesImg.sprite = _livesSprites[currentLives];
    }
}
