using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScale : MonoBehaviour
{
    Platform platform;
    public float scaleEffect;

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

