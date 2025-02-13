using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int player1_coins = 0;
    int player2_coins = 0;
    int player3_coins = 0;
    int player4_coins = 0;

    float gameTimer = 0f;
    float gameTimerMax = 300f;

    bool pauseTimer = false;

    GameObject[] players;

    [SerializeField]
    TextMeshPro scoreText;

    [SerializeField]
    TextMeshPro timerText;

    // Start is called before the first frame update
    void Start()
    {
        

        ResetScores();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseTimer)
        {
            UpdateTimer();
        }
    }

    void ResetScores()
    {
        player1_coins = 0;
        player2_coins = 0;
        player3_coins = 0;
        player4_coins = 0;
    }

    void ResetTimer()
    {
        gameTimer = gameTimerMax;
    }

    void UpdateTimer()
    {
        gameTimer -= Time.deltaTime;

        if (gameTimer < 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {

    }
}
