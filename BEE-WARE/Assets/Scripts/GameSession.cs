using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI scoreSignText;
    [SerializeField] int score = 0;

    float totalScore;

    float currentTime = 0f;
    float startingTime = 500f;

    bool timerStop = true;

    void Awake()
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        currentTime = startingTime;
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        startingTime = 500f;
        currentTime = startingTime;
        timerStop = false;
        timerText.text = currentTime.ToString("0");
    }

    public void StartTimer()
    {
        timerStop = false;
    }
    public void StopTimer()
    {
        timerStop = true;
    }

    void Update()
    {
        if(!timerStop)
        {
            currentTime -= 1 *Time.deltaTime;
            timerText.text = currentTime.ToString("0");
        }

    }

    public void Pause()
    {
        pauseText.text = "Pause";
    }

    public void UnPause()
    {
        pauseText.text = "";
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void AddToLives(int livesToAdd)
    {
        playerLives += livesToAdd;
        livesText.text = playerLives.ToString();

    }

    void TakeLife()
    {
        playerLives = playerLives -1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        timerText.text = ("");
        totalScoreText.text = ("");
        scoreSignText.text = ("");
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void CalculateTotalScore()
    {
        totalScore = score + playerLives*500 + currentTime;
        totalScoreText.text = totalScore.ToString("0");
        scoreSignText.text = ("-Score-");
    }

}
