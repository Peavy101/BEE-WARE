using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip Oomf;

    BoxCollider2D myBoxCollider;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void OnOpen()
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            AudioSource.PlayClipAtPoint(Oomf, Camera.main.transform.position);
            StartCoroutine(LoadNextLevel());
        }
    }

    public IEnumerator LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex <= 4 )
        {   
            FindObjectOfType<Fade>().FadeOut();
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(currentSceneIndex + 1);
            FindObjectOfType<Fade>().FadeIn();
        }
        else if(currentSceneIndex <= 5)
        {
            FindObjectOfType<Fade>().FadeOut();
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(currentSceneIndex + 1);
            FindObjectOfType<Fade>().FadeIn();
            FindObjectOfType<GameSession>().CalculateTotalScore();
        }
    }
}
