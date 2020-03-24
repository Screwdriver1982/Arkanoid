using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBalls : MonoBehaviour
{
    Ball ball;

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

        if (collision.CompareTag("Platform"))
        {

            ApplyEffect();
            FindObjectOfType<GameManager>().RemovePickupFromList(gameObject);
            Destroy(gameObject);

        }
        else if (collision.CompareTag("Wall"))
        {
            FindObjectOfType<GameManager>().RemovePickupFromList(gameObject);
            Destroy(gameObject);
            Debug.Log("bottom");
        }

    }
}
