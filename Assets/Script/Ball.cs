using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    bool started; // запущен ли мяч
    Platform platform;
    Rigidbody2D rb;
    public int speed;


    // Start is called before the first frame update
    void Start()
    {
        started = false;
        platform = FindObjectOfType<Platform>(); //находим компоненту платформы
        rb = GetComponent<Rigidbody2D>(); //находим компоненту ригидбади на том же объекте, к которому приделан скрипт
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            // ничего не делаем
            Debug.Log(rb.velocity.magnitude);
        }
        else
        {
            LockBallToPlatform();
        }

    }

    private void LockBallToPlatform()
    {
        //двигаем мяч с платформой
        transform.position = new Vector3(platform.transform.position.x, transform.position.y, 0);
        
        //стартуем мяч
        if (Input.GetMouseButtonDown(0))
        {
            started = true;
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        Vector2 force = new Vector2(1, 2) * speed;
        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Stay");
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter");
    }
    
}
