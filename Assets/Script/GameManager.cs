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
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false, true); //устанавливаем шарик в начальное положение и делаем его незапущенным


        platformVar = FindObjectOfType<Platform>();
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY); //устанавливаем платформу в началаьное положение




    }
    public void DestroyBlock(int score)
    {
        blocksLeft--;
        currentScores = currentScores + score;
        scoresText.text = "Scores: " + currentScores;

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
                Ball[] ballMassive= FindObjectsOfType<Ball>();
                for (int i = 1; i < ballMassive.Length; i++)
                {
                    Destroy(ballMassive[i].gameObject);
                }
            }

            currentLevel = currentLevel + 1;
            LoadLevel(currentLevel);
        }

    }

    public void Death()
    {
        livesLeft--;
        livesLeftTxt.text = "Lives: " + livesLeft;
        ballVar = FindObjectOfType<Ball>();
        ballVar.SetBall(startPlatformPosX, ballStartPosY, false, false, true);
        ballVar.transform.localScale = new Vector3(1f, 1f, 1f);

        platformVar = FindObjectOfType<Platform>();
        platformVar.SetPlatform(startPlatformPosX, startPlatfromPosY);
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
