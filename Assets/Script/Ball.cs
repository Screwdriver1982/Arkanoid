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
    public float currentSpeedCoef = 1f;
    public float dupAng = Mathf.PI/9;
    //public GameObject duplicateBall;
    float ballDeltaX = 0;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //находим компоненту ригидбади на том же объекте, к которому приделан скрипт
    }

    void Start()
    {
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
            LockBallToPlatform(ballDeltaX);
        }

    }

    private void LockBallToPlatform(float deltaX)
    {
        //двигаем мяч с платформой
        transform.position = new Vector3(platform.transform.position.x + deltaX, transform.position.y, 0);
        
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
        float angleBall;
        if (rb.transform.position.x > platform.transform.position.x)
        {
            angleBall = Random.Range(Mathf.PI / 18, Mathf.PI * 8 / 18);
        }
        else
        {
            angleBall = Random.Range(Mathf.PI *10 / 18, Mathf.PI * 17 / 18);
        }

        
        Debug.Log(angleBall);
        Debug.Log(Mathf.Sin(angleBall));
        Vector2 speedVector = new Vector2(Mathf.Cos(angleBall),Mathf.Sin(angleBall)) * speed * currentSpeedCoef;
        rb.velocity = speedVector;
        Debug.Log(rb.velocity);
    }

    public void SetBall(float ballX, float ballY, bool active, bool trailActivity, bool setBaseSpeed)
    {
        
        started = active;
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(ballX, ballY, 0);
        trail.gameObject.SetActive(trailActivity);
        if (setBaseSpeed)
        {
            currentSpeedCoef = 1f;
        }
    }

    public void Duplicate()
    {
        Vector3 newBallposition = transform.position;
        Vector3 vel = rb.velocity;
        GameObject newObject = Instantiate(gameObject);
        Rigidbody2D newObjectRb = newObject.GetComponent<Rigidbody2D>();
        newObjectRb.velocity = new Vector2(vel.x * Mathf.Cos(dupAng) - vel.y * Mathf.Sin(dupAng), vel.x * Mathf.Sin(dupAng) + vel.y * Mathf.Cos(dupAng));
        rb.velocity = new Vector2(vel.x * Mathf.Cos(dupAng) + vel.y * Mathf.Sin(dupAng), - vel.x * Mathf.Sin(dupAng) + vel.y * Mathf.Cos(dupAng));
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

    public void ChangeVelocity(float speedCoef)
    {
        rb.velocity = rb.velocity * speedCoef;
        currentSpeedCoef = currentSpeedCoef * speedCoef;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (platform.sticky)
            {
                ballDeltaX = rb.transform.position.x - collision.transform.position.x;
                SetBall(rb.transform.position.x, rb.transform.position.y, false, false, false);
                
            }
        }
    }


}
