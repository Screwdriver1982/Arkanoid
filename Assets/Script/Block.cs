using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] //этот скрипт нельзя повесить на объект, у которого нет компоненты коллайдер
public class Block : MonoBehaviour
{
    public int blockHP;
    public Sprite dmgIcon;
    SpriteRenderer blockIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        } 
    }
}
