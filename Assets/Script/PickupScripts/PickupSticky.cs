using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSticky : MonoBehaviour
{
    Platform platform;
    public int scores;

    void ApplyEffect()
    {
        platform = FindObjectOfType<Platform>();
        if (platform != null)
        {
            platform.sticky = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (collision.CompareTag("Platform"))
        {

            ApplyEffect();
            gameManager.RemovePickupFromList(gameObject);
            gameManager.AddScore(scores);
            Destroy(gameObject);

        }
        else if (collision.CompareTag("Wall"))
        {
            gameManager.RemovePickupFromList(gameObject);
            Destroy(gameObject);
            Debug.Log("bottom");
        }

    }
}
