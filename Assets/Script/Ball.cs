using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public bool started; // запущен ли мяч
    Platform platform;
    Rigidbody2D rb;
    public float speed;
    public TrailRenderer trail;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //находим компоненту ригидбади на том же объекте, к которому приделан скрипт
    }

    void Start()
    {
        started = false;
        platform = FindObjectOfType<Platform>(); //находим компоненту платформы
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            // ничего не делаем
           // Debug.Log(rb.velocity.magnitude);
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

            trail.gameObject.SetActive(true);
            

        }
    }

    private void LaunchBall()
    {
        float angleBall = Random.Range(10, 80);
        Vector2 speedVector = new Vector2(Mathf.Cos(angleBall),Mathf.Sin(angleBall)) * speed;
        rb.velocity = speedVector;
    }

    public void SetBall(float ballX, float ballY, bool active, bool trailActivity)
    {
        started = active;
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(ballX, ballY, 0);
        trail.gameObject.SetActive(trailActivity);
    }




    
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        // проверяем летает ли шар горизонтально, если да, то меняем угол немного
        if (Mathf.Approximately(rb.velocity.x,0) )
        {
            float newAngel = Random.Range(85, 95);
            rb.velocity = new Vector2(Mathf.Cos(newAngel), Mathf.Sin(newAngel)) * rb.velocity.magnitude;

        }
        
        // проверяем летает ли шар вертикально и если да, то меняем немного
        
        if (Mathf.Approximately(rb.velocity.y, 0))
        {

            float newAngel = Random.Range(-5, 5);
            rb.velocity = new Vector2(Mathf.Cos(newAngel), Mathf.Sin(newAngel)) * rb.velocity.magnitude;

        }

    }

    

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rb.velocity);
        } 
    }

}
