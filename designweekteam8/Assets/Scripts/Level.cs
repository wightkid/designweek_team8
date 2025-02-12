using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int numberOfPlayers = 0;

    [Header("LEVEL EDIT")]
    public bool TOGGLEEDIT = false;

    GameObject[] cells;
    public Vector2 gridSize = Vector2.zero;
    public float cellSize = 1f;
    int numberOfCells;
    GameObject cellParent;
    public GameObject cellPrefab;
    public Camera mainCam;

    public GameObject[] spawnPoints = new GameObject[4];
    public Color[] playerColors = new Color[4];
    public GameObject[] players;
    public GameObject playerPrefab;
    

    GameObject mouseCollider;

    [Header("SAVE LEVEL")]
    public bool saveLevel;
    public string levelName;
    public TextAsset levelData;

    void Start()
    {
        // Get number of players from the player select screen
        // Initialize array of players
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        players = new GameObject[numberOfPlayers];

        // Instantiate a playerPrefab
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Instantiate(playerPrefab);
            SpriteRenderer player_sr = players[i].GetComponent<SpriteRenderer>();
            player_sr.color = playerColors[i];
            players[i].transform.position = spawnPoints[i].transform.position;
        }

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

        if (TOGGLEEDIT)
        {
            mouseCollider = new GameObject("MOUSECOLLIDER");
            mouseCollider.AddComponent<CircleCollider2D>();
            mouseCollider.AddComponent<Rigidbody2D>();
            mouseCollider.tag = "MouseCollider";
            mouseCollider.GetComponent<CircleCollider2D>().isTrigger = false;
            mouseCollider.SetActive(true);
        }
    }

    void Update()
    {

        if (TOGGLEEDIT)
        {
            Vector3 tempPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            
            mouseCollider.GetComponent<Rigidbody2D>().MovePosition(new Vector2(tempPos.x, tempPos.y));
            //if (Input.GetMouseButton(0))
            //{
            //    mouseCollider.SetActive(true);
            //}
            //else
            //{
            //    mouseCollider.SetActive(false);
            //}

            if (saveLevel)
            {
                string path = Path.Combine(Application.persistentDataPath, levelName);

                float[][] levelPositions = new float[cells.Length][];

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].activeSelf)
                    {
                        // Writes to file: Index_posX_posY|Index_posX_posY|etc.
                        File.WriteAllText(path, $"{i}('_'){cells[i].transform.position.x}('_'){cells[i].transform.position.y}('|')");

                    }
                }

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

                saveLevel = false;
            }

        }
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
