using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMeta : MonoBehaviour
{
    public int levelItems;
    // Start is called before the first frame update

    private void Awake()
    {
        levelItems = 0;
    }

    public void AddBlockCount()
    
    {
        levelItems++;
    }
}

