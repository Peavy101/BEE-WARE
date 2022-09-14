using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] string newGameLevel1 = "Level1";

    public void NewGameButton()
    {
        FindObjectOfType<GameSession>().StartTimer();
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().ResetTimer();
        SceneManager.LoadScene(newGameLevel1);
    }

}
