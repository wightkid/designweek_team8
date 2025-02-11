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
    GameObject cellParent;
    public GameObject cellPrefab;
    public Camera mainCam;
    

    void Start()
    {
        // Get number of players selected
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        numberOfCells = (int)gridSize.x * (int)gridSize.y;
        cells = new GameObject[numberOfCells];
        cellParent = new GameObject("Cells");

        // Create new gameobject, format name, set parent
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = Instantiate(cellPrefab, cellParent.transform);
        }
        
        // Instantiate all cells with cell prefabs, and update position
        // Position is determined by cell size and count
        int cellIndex = 0;
        for (int row = 0; row < gridSize.y; row++)
        { 
            for (int col = 0; col < gridSize.x; col++)
            {
                Vector2 newPosition = new Vector2(col * cellSize, row * cellSize);
                cells[cellIndex].transform.position = newPosition;
                cellIndex++;
            }
        }
        UpdateCameraPosition();
    }
     
    void Update()
    {
        int cellIndex = 0;
        for (int row = 0; row < gridSize.y; row++)
        {
            for (int col = 0; col < gridSize.x; col++)
            {
                Vector2 newPosition = new Vector2(col * cellSize, row * cellSize);
                cells[cellIndex].transform.position = newPosition;
                cellIndex++;
            }
        }
        UpdateCameraPosition();
    }

    // Set camera position to the midpoint of the grid matrix
    void UpdateCameraPosition()
    {
        // Update camera
        float camX = cells[0].transform.position.x + cells[cells.Length - 1].transform.position.x * 0.5f;
        float camY = cells[0].transform.position.y + cells[cells.Length - 1].transform.position.y * 0.5f;
        mainCam.transform.position = new Vector3(camX, camY, -0.35f);
    }
}
