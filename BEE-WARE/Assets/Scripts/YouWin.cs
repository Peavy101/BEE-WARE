using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    [SerializeField] ParticleSystem winEffect;
    [SerializeField] ParticleSystem winEffectTwo;

    float timeSlow = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            winEffect.Play();
            winEffectTwo.Play();
            Time.timeScale = timeSlow;
            FindObjectOfType<Lock>().ShowLock();
            FindObjectOfType<GameSession>().StopTimer();
            Invoke("BackToNormal", 1f);
        }
    }

    void BackToNormal()
    {
        Time.timeScale = 1f;
        timeSlow = 1f;
    }

}
