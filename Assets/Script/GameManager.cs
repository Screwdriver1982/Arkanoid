using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform[] levels;
    int currentLevel  = 0;
    public int blocksLeft = 0;
    LevelMeta levelMetaVar;
    public int currentScores;
    public Text scoresText;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(currentLevel);
    }

    // Update is called once per frame
    
    void LoadLevel(int level)
    {
        Destroy(FindObjectOfType<LevelMeta>());
        Instantiate(levels[level], new Vector3(0, 0, 0), Quaternion.identity);
        levelMetaVar = FindObjectOfType<LevelMeta>();
        blocksLeft = levelMetaVar.levelItems;

    }
    void Update()
    {
        if (blocksLeft == 0)
        {
            currentLevel = currentLevel+1;
            LoadLevel(currentLevel);
        }
        
    }
}
