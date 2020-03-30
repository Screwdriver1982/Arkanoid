using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBalls : MonoBehaviour
{
    Ball ball;
    public int scores;

    void ApplyEffect()
    {
        ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            FindObjectOfType<GameManager>().ballsNumber += 1;
            ball.Duplicate();
            
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
