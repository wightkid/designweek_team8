using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int numberOfPlayers = 0;

    // TODO: change cells from gameObject to cells
    GameObject[] cells;
    public Vector2 gridSize = Vector2.zero;
    public float cellSize = 1f;
    int numberOfCells;

    void Start()
    {
        // Get number of players selected
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        numberOfCells = (int)gridSize.x * (int)gridSize.y;

        cells = new GameObject[numberOfCells];

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new GameObject($"Cell_{i + 1}");
        }
    }
     
    void Update()
    {
        
    }
}
