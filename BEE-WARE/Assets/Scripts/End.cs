using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] string newGame = "TitleScreen";

    void Start()
    {
        FindObjectOfType<GameSession>().CalculateTotalScore();
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGame);
        FindObjectOfType<GameSession>().ResetGameSession();
    }
}
