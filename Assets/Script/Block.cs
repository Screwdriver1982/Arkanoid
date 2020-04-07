using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] //этот скрипт нельзя повесить на объект, у которого нет компоненты коллайдер
public class Block : MonoBehaviour
{
    public int blockHP;
    public Sprite dmgIcon;
    public int blockScore;
    public GameObject[] pickups;
    public bool isExploding;
    public float explosionRadius;
    public bool liveOrNot = true;
    public float dropChance;
    public AudioClip destroySound;
    public GameObject destroyFX;

    AudioSource audio;
    SpriteRenderer blockIcon;
    GameManager gameManagerVar;
    LevelMeta levelMeta;



    // Start is called before the first frame update
    void Awake()
    {
        levelMeta = FindObjectOfType<LevelMeta>();
        levelMeta.AddBlockCount();

    }

    // Update is called once per frame
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            if (ball.isBlast != true)
            {
                DecreaseHp(1);
            }
        }
        
    }

    private void DecreaseHp(int power)
    {
        if (blockHP > 1)
        {
            blockHP = blockHP - power;
            blockIcon = GetComponent<SpriteRenderer>();
            blockIcon.sprite = dmgIcon;

        }
        else
        {
            DestroyBlock("Ball");
        }
    }

    public void DestroyBlock(string actor)
    {
        if (liveOrNot)
        { 
        
            Destroy(gameObject);

            AudioSource audio = FindObjectOfType<AudioSource>();
            audio.PlayOneShot(destroySound);

            liveOrNot = false;
            audio.Play();
            Debug.Log(actor + " kill: " + gameObject.name);

            gameObject.SetActive(false);
            Debug.Log("SetActiveFalse for " + gameObject.name);

            float dice = Random.Range(0f, 1f);
            Debug.Log(gameObject.name + " " + dice);
            Debug.Log(gameObject.name + " " + dropChance);

            if (pickups != null && pickups.Length != 0 && dice <= dropChance)
            {
                int choosenPickupNumber = Random.Range(0, pickups.Length);
                GameObject pickup = pickups[choosenPickupNumber];

                Vector3 pickupPosition = transform.position;
                //Instantiate(pickup, pickupPosition, Quaternion.identity);

                GameObject newObject = Instantiate(pickup);
                newObject.transform.position = pickupPosition;
                FindObjectOfType<GameManager>().AddPickupInList(newObject);
            }

            if (destroyFX != null)
            {
                Vector3 fxPosition = transform.position;

                //запомнили созданный объект, чтобы потом его прибить
                GameObject newObject = Instantiate(destroyFX, fxPosition, Quaternion.identity);

                //уничтожить через N секунд
                Destroy(newObject, 5f);
            }


            if (isExploding)
            {
                // маска слоя, по которому мы ищем объекты
                LayerMask layerMask = LayerMask.GetMask("Blocks");

                //ищем объекты в радиусе взрыва с центром в центре блока
                Collider2D[] objectInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);
            
                //for (int i = 0; i < objectInRadius.Length; i++)
                //{
                //    Collider2D objectI = objectInRadius[i];
                //    Debug.Log(objectI.gameObject.name);
                //}


                // ищем в массиве все объекты типа коллайдера и выводим их имена
                foreach (Collider2D objectI in objectInRadius)
                {

                    if (objectI.gameObject == gameObject)
                    {
                        continue; //пойдет на следующую итерацию цикла
                    }

                    Debug.Log("Destroy: " + objectI.gameObject.name);
                    Block block = objectI.gameObject.GetComponent<Block>();

                    if (block == null)
                    {
                        Destroy(objectI.gameObject);
                    }
                    else
                    {
                        block.DestroyBlock(gameObject.name);
                        block.gameObject.SetActive(false);
                    }
                }
            }

            FindObjectOfType<GameManager>().DestroyBlock(blockScore);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    
}
