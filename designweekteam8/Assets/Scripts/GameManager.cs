using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int[] playerCoins;
    float gameTimer = 0f;
    public float gameTimerMax = 300f;
    bool pauseTimer = false;

    public int timerEndSceneNumber = 4;

    [SerializeField]
    TextMeshPro scoreText;

    [SerializeField]
    TextMeshPro timerText;

    [SerializeField]
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        playerCoins = new int[1];
        gameTimer = gameTimerMax;
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

        UpdateScores(players);

        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);

        timerText.text = $"Time Remaining: {minutes}:{seconds}";
        scoreText.text = $"Player Score: {playerCoins[0]}\n" +
            $"Player Score: {playerCoins[1]}\n" +
            $"Player Score: {playerCoins[2]}\n" +
            $"Player Score: {playerCoins[3]}\n";
    }

    void UpdateScores(GameObject[] playerObjects)
    {
        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerCoins[i] = (int)playerObjects[i].GetComponent<PlayerStats>().money;
        }
    }

    void ResetScores()
    {
        for (int i = 0; i < playerCoins.Length; i++)
        {
            playerCoins[i] = 0;
        }
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
        SceneManager.LoadScene(timerEndSceneNumber);
    }
}