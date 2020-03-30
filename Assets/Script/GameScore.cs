using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int playerScore;
    public bool gameResult;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ScoreRecord(int score)
    {
        playerScore += score;
    }
}
