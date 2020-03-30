using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBallBlast : MonoBehaviour
{
    Ball[] balls;
    public int scores;

    void ApplyEffect()
    {
        balls = FindObjectsOfType<Ball>();
        if (balls != null)
        {
            foreach (Ball objectI in balls)
            {
                objectI.BecomeBlast(true);
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