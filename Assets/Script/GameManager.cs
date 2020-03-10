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
    Platform platformVar;
    Transform platformPos;
    public float startPlatformPosX;
    public float startPlatfromPosY;
    Ball ballVar;
    public float ballStartPosY;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(currentLevel);
    }

    // Update is called once per frame
    
    void LoadLevel(int level)
    {
        levelMetaVar = FindObjectOfType<LevelMeta>();
        if (levelMetaVar != null)
        {
            Destroy(levelMetaVar.gameObject);
        } 

        Instantiate(levels[level], Vector3.zero, Quaternion.identity);
        
        levelMetaVar = FindObjectOfType<LevelMeta>();
        blocksLeft = levelMetaVar.levelItems;

        ballVar = FindObjectOfType<Ball>();
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false); //устанавливаем шарик в начальное положение и делаем его незапущенным

        
        platformVar = FindObjectOfType<Platform>();
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY);
       
        


    }
    public void DestroyBlock(int score)
    {
        blocksLeft -- ;
        currentScores = currentScores + score;
        scoresText.text = "Scores: " + currentScores;

        if (blocksLeft == 0)
        {
            
            currentLevel = currentLevel + 1;
            LoadLevel(currentLevel);
        }
        
    }
}
