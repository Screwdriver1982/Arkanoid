using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] //этот скрипт нельзя повесить на объект, у которого нет компоненты коллайдер
public class Block : MonoBehaviour
{
    public int blockHP;
    public Sprite dmgIcon;
    public int blockScore;
    SpriteRenderer blockIcon;
    GameManager gameManagerVar;
    LevelMeta levelMeta;
    public GameObject pickup;

    // Start is called before the first frame update
    void Awake()
    {
        levelMeta = FindObjectOfType<LevelMeta>();
        levelMeta.AddBlockCount();

    }

    // Update is called once per frame
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (blockHP > 1)
        {
            blockHP = blockHP - 1;
            blockIcon = GetComponent<SpriteRenderer>();
            blockIcon.sprite = dmgIcon;
            
        }
        else
        {
            Destroy(gameObject);
            if (pickup != null)
            {
                Vector3 pickupPosition = transform.position;
                //Instantiate(pickup, pickupPosition, Quaternion.identity);
                
                GameObject newObject = Instantiate(pickup);
                newObject.transform.position = pickupPosition;
                FindObjectOfType<GameManager>().AddPickupInList(newObject);
            }
            FindObjectOfType<GameManager>().DestroyBlock(blockScore);
        } 
    }
}
