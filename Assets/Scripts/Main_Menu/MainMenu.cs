using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const int GAME_SCENE_INDEX = 1;

    public void LoadGame() {
        SceneManager.LoadScene(GAME_SCENE_INDEX);
    }
}
