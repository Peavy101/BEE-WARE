using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;    
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI scoreSignText;

    // Barry text stuff here // 
    [SerializeField] TextMeshProUGUI barryText;
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;
    public string[] stringArray;
    int i = 0;

    float totalScore;

    float currentTime = 0f;
    float startingTime = 500f;

    bool timerStop = true;

    public Image textBox;
    public Image barryHead;
    public Image img;

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
        img.enabled = false;
        textBox.enabled = false;
        barryHead.enabled = false;
        currentTime = startingTime;
        livesText.text = ("");
        scoreText.text = ("");
        barryText.text = ("");

        EndCheck();
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
        scoreText.text = ("");
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

    public void StartLives()
    {
        img.enabled = true;
        livesText.text = playerLives.ToString();
    }

    private IEnumerator TextVisible()
    {
        barryText.ForceMeshUpdate(); 
        int totalVisibleCharacters = barryText.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            barryText.ForceMeshUpdate();
            int visibleCount = counter % (totalVisibleCharacters + 1);
            barryText.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    void EndCheck()
    {
        if(i< stringArray.Length - 1)
        {
            barryText.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }
}
