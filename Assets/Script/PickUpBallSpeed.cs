using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBallSpeed : MonoBehaviour
{
    public float speedCoef;
    void ApplyEffect()
    {
        Debug.Log("Speed Up");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Platform"))
        {
            
                ApplyEffect();
                Destroy(gameObject);

        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("bottom");
        }

    }
}
