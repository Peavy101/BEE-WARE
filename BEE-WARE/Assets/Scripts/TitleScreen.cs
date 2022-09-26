using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public void NewGameButton()
    {
        
        FindObjectOfType<Fade>().FadeOut();
        Invoke("StartLevelOne", 2);
        
    }

    void StartLevelOne()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<Fade>().FadeIn();
        FindObjectOfType<GameSession>().StartLives();
        FindObjectOfType<GameSession>().ResetTimer();
    }
}
