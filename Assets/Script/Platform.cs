using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float minX = -7.3f;
    public float maxX = 7.3f;
    public float scaleCoef;
    public bool sticky;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition; //координаты на экране
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos); // функция приводит к координатам мира

        float mouseX = mouseWorldPos.x;
        float mouseY = transform.position.y;
        float clampedMouseX = Mathf.Clamp(mouseX, minX + (scaleCoef - 1f) * 1.5f, maxX - (scaleCoef - 1f)*1.5f);
        transform.position = new Vector3(clampedMouseX, mouseY, 0); //матфункция, которая ограничивает число в рамках диапазона
        
    }

    public void SetPlatform(float platformX, float platformY)
    {
        transform.position = new Vector3(platformX, platformY, 0);
    }
}
