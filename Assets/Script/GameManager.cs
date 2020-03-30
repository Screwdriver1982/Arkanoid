using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform[] levels;
    int currentLevel = 0;
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
    public int ballsNumber;
    int livesLeft;
    public Text livesLeftTxt;
    bool pauseActive;
    public List<GameObject> pickUpsList;
    Transporter transporter;
    ScenesLoader scenesLoader;
    GameScore gameScore;



    // Start is called before the first frame update
    void Start()
    {
        livesLeft = maxLives;
        livesLeftTxt.text = "Lives: " + livesLeft;
        transporter = FindObjectOfType<Transporter>();
        currentLevel = transporter.chosenLevel;
        Destroy(transporter.gameObject);
        LoadLevel(currentLevel);
        scenesLoader = FindObjectOfType<ScenesLoader>();
        gameScore = FindObjectOfType<GameScore>();
    }

    // Update is called once per frame

    public void LoadLevel(int level)
    {
        Debug.Log("Load level: " + level);

        levelMetaVar = FindObjectOfType<LevelMeta>();
        if (levelMetaVar != null)
        {
            Destroy(levelMetaVar.gameObject);
        }

        if (level >= levels.Length)
        {
            gameScore.gameResult = true;
            scenesLoader.LoadNextLevel();
        }



        Instantiate(levels[level], Vector3.zero, Quaternion.identity);

        levelMetaVar = FindObjectOfType<LevelMeta>();
        blocksLeft = levelMetaVar.levelItems;

        ballVar = FindObjectOfType<Ball>();
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false, true, true); //устанавливаем шарик в начальное положение и делаем его незапущенным


        platformVar = FindObjectOfType<Platform>();
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY); //устанавливаем платформу в началаьное положение




    }
    public void DestroyBlock(int score)
    {
        blocksLeft--;

        Debug.Log("Left: " + blocksLeft);
        AddScore(score);

        if (blocksLeft == 0)
        {
            // чистим пикапы
            if (pickUpsList.Count != 0)
            {
                while (pickUpsList.Count > 0)
                {
                    GameObject pickup = pickUpsList[0];
                    pickUpsList.RemoveAt(0);
                    Destroy(pickup);

                }

            }

            //чистим мячи
            if (ballsNumber > 1)
            {
                Ball[] ballMassive = FindObjectsOfType<Ball>();
                for (int i = 1; i < ballMassive.Length; i++)
                {
                    Destroy(ballMassive[i].gameObject);
                }
            }

            currentLevel = currentLevel + 1;
            LoadLevel(currentLevel);
        }

    }

    public void AddScore(int score)
    {
        currentScores = currentScores + score;
        scoresText.text = "Scores: " + currentScores;
        gameScore.ScoreRecord(score);
    }

    public void Death()
    {
        livesLeft--;
        if (livesLeft > 0)
        {
            livesLeftTxt.text = "Lives: " + livesLeft;
            ballVar = FindObjectOfType<Ball>();
            ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false, true, false);
            ballVar.transform.localScale = new Vector3(1f, 1f, 1f);

            platformVar = FindObjectOfType<Platform>();
            platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY);
        }
        else
        {
            gameScore.gameResult = false;
            scenesLoader.LoadNextLevel();
        }
    }

    public void BallDeath(GameObject ball)
    {
        if (ballsNumber == 1)
        {
            Death();
        }
        else
        {
            ballsNumber--;
            Destroy(ball);
        }
    }

    public void AddPickupInList(GameObject pickup)
    {
        pickUpsList.Add(pickup);
    }

    public void RemovePickupFromList(GameObject pickup)
    {
        pickUpsList.Remove(pickup);
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
