using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBallSpeed : MonoBehaviour
{
    public float speedCoef;
    Ball[] rballs;
    public int scores;
    
    void ApplyEffect()
    {
        rballs = FindObjectsOfType<Ball>();
        if (rballs != null)
        {
            for (int i = 0; i < rballs.Length; i++)
            {
                rballs[i].ChangeVelocity(speedCoef);
                Debug.Log("increase");
            }
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
