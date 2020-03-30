using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScale : MonoBehaviour
{
    Platform platform;
    public float scaleEffect;
    public int scores;

    void ApplyEffect()
    {
        platform = FindObjectOfType<Platform>();
        if (platform != null)
        {
            platform.transform.localScale = new Vector3 (platform.transform.localScale.x * scaleEffect, platform.transform.localScale.y, platform.transform.localScale.z);
            platform.scaleCoef = platform.scaleCoef*scaleEffect;
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

