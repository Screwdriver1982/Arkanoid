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
    public int maxLives;
    int livesLeft;
    public Text livesLeftTxt;
    bool pauseActive;


    // Start is called before the first frame update
    void Start()
    {
        livesLeft = maxLives;
        livesLeftTxt.text = "Lives: " + livesLeft;

        LoadLevel(currentLevel);
    }

    // Update is called once per frame
    
    public void LoadLevel(int level)
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
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false); //устанавливаем шарик в начальное положение и делаем его незапущенным

        
        platformVar = FindObjectOfType<Platform>(); 
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY); //устанавливаем платформу в началаьное положение
       
        


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

    public void Death()
    {
        livesLeft -- ;
        livesLeftTxt.text = "Lives: " + livesLeft;
        ballVar = FindObjectOfType<Ball>();
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false);

        platformVar = FindObjectOfType<Platform>();
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY);
    }

    
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseActive)
            {
                //выключить паузу
                Time.timeScale = 1;
                pauseActive = false;
            }
            else
            {
                //включить паузу
                Time.timeScale = 0;
                pauseActive = true;
            }

        }

    }

}
