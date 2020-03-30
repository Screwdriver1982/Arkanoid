using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public int chosenLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChooseLevel(int level)
    {
        chosenLevel = level;
    }   

}
