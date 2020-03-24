using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBallSpeed : MonoBehaviour
{
    public float speedCoef;
    Ball[] rballs;
    
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
