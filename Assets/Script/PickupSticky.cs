using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSticky : MonoBehaviour
{
    Platform platform;
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
