using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResulting : MonoBehaviour
{
    GameScore gameScore;
    bool gameResult;
    int playerScore;
    public Text scoreText;
    public GameObject win;
    public GameObject lose;

    // Start is called before the first frame update
    void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
        playerScore = gameScore.playerScore;
        gameResult = gameScore.gameResult;
        Destroy(gameScore.gameObject);
        LoadUI(playerScore, gameResult);

    }

    void LoadUI(int score, bool winOrLose)
    {
        scoreText.text = ("" + score);
        if (winOrLose)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }
    
    }


}
