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
    public float dupAng = Mathf.PI / 9;
    //public GameObject duplicateBall;
    float ballDeltaX = 0;
    public float newAngel = Mathf.PI / 18;
    public bool isBlast;
    public float blastRadius;


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
            angleBall = Random.Range(Mathf.PI * 7 / 18, Mathf.PI * 4 / 9);
        }
        else
        {
            angleBall = Random.Range(Mathf.PI *5 / 9, Mathf.PI * 11 / 18);
        }


        //Debug.Log(angleBall);
        //Debug.Log(Mathf.Sin(angleBall));
        Vector2 speedVector = new Vector2(Mathf.Cos(angleBall), Mathf.Sin(angleBall)) * speed * currentSpeedCoef;
        rb.velocity = speedVector;
        //Debug.Log(rb.velocity);
    }

    public void SetBall(float ballX, float ballY, bool active, bool trailActivity, bool setBaseSpeed, bool saveBlast)
    {

        started = active;
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(ballX, ballY, 0);
        trail.gameObject.SetActive(trailActivity);
        if (setBaseSpeed)
        {
            currentSpeedCoef = 1f;
        }
        BecomeBlast(isBlast && saveBlast);
    }

    public void Duplicate()
    {
        Vector3 vel = rb.velocity;
        GameObject newObject = Instantiate(gameObject);
        Rigidbody2D newObjectRb = newObject.GetComponent<Rigidbody2D>();
        newObjectRb.velocity = new Vector2(vel.x * Mathf.Cos(dupAng) - vel.y * Mathf.Sin(dupAng), vel.x * Mathf.Sin(dupAng) + vel.y * Mathf.Cos(dupAng));
        rb.velocity = new Vector2(vel.x * Mathf.Cos(dupAng) + vel.y * Mathf.Sin(dupAng), -vel.x * Mathf.Sin(dupAng) + vel.y * Mathf.Cos(dupAng));
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        float velCos = Mathf.Abs(vel.x / vel.magnitude);


        // проверяем летает ли шар вертикально, если да, то меняем угол немного
        if (velCos > 0.995 || velCos < 0.05)
        {
            rb.velocity = new Vector2(vel.x * Mathf.Cos(newAngel) + vel.y * Mathf.Sin(newAngel), -vel.x * Mathf.Sin(newAngel) + vel.y * Mathf.Cos(newAngel));


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
        speedCoef = Mathf.Clamp(currentSpeedCoef * speedCoef, 0.9f, 2f);
        rb.velocity = rb.velocity / currentSpeedCoef * speedCoef;
        currentSpeedCoef = speedCoef;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (platform.sticky)
            {
                ballDeltaX = rb.transform.position.x - collision.transform.position.x;
                SetBall(rb.transform.position.x, rb.transform.position.y, false, false, false, true);

            }
        }

        if (isBlast)
        {
            Blast();
        }
        
    }

    private void Blast()
    {
        LayerMask layerMask = LayerMask.GetMask("Blocks");
        Collider2D[] objectInRadius = Physics2D.OverlapCircleAll(transform.position, blastRadius, layerMask);
        foreach (Collider2D objectI in objectInRadius)
        {

            Debug.Log("Destroy: " + objectI.gameObject.name);
            Block block = objectI.gameObject.GetComponent<Block>();

            if (block == null)
            {
                Destroy(objectI.gameObject);
            }
            else
            {
                block.DestroyBlock("Blast of" + gameObject.name);
                block.gameObject.SetActive(false);
            }


        }
    }

    public void BecomeBlast(bool blast)
    {
        if (blast)
        {

            isBlast = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            trail.startColor = Color.red;
            trail.endColor = Color.yellow;

        }
        else
        {
            isBlast = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            trail.startColor = Color.grey;
            trail.endColor = Color.clear;
        }
    }

}
