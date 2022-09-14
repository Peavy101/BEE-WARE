using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyUp : MonoBehaviour
{
    [SerializeField] AudioClip HoneyUpSound;
    [SerializeField] int livesForLivesPickup = 1;

    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToLives(livesForLivesPickup);
            AudioSource.PlayClipAtPoint(HoneyUpSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
