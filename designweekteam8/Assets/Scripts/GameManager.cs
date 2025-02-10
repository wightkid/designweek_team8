using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int numberOfPlayers = 0;
    public Vector2 gridSize = Vector2.zero;
    public float cellSize = 1f;

    void Start()
    {
        // Get number of players selected
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
    }

    void Update()
    {
        
    }
}
