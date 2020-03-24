using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBallScale : MonoBehaviour
{
    Ball[] balls;
    public float ballScaleEffect;

    void ApplyEffect()
    {
        balls = FindObjectsOfType<Ball>();
        if (balls != null)
        {
            for(int i =0; i <balls.Length; i++)
            balls[i].transform.localScale = new Vector3(balls[i].transform.localScale.x * ballScaleEffect, balls[i].transform.localScale.y * ballScaleEffect, balls[i].transform.localScale.z);
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
