using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int numberOfPlayers = 0;

    public GameObject cellPrefab;
    public Camera mainCam;
    GameObject[] cells;
    GameObject cellParent;
    public Vector2 gridSize = Vector2.zero;
    public float cellSize = 1f;
    int numberOfCells;
    
    public GameObject[] spawnPoints = new GameObject[4];
    public Color[] playerColors = new Color[4];
    public GameObject playerPrefab;
    public GameObject[] players;

    [Header("LEVEL EDIT")]
    public bool TOGGLEEDIT;

    [Header("SAVE LEVEL")]
    public bool saveLevel = false;
    public bool flipCellActives = false;
    public string saveLevelName;
    public TextAsset levelData;

    //[Header("LOAD LEVEL")]
    //public bool loadLevel = false;
    //public string loadLevelName;

    void Start()
    {
        // Get number of players from the player select screen
        // Initialize array of players
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        PlayerPrefs.SetInt("playerNumber", 0);
        players = new GameObject[numberOfPlayers];
        
        numberOfCells = (int)gridSize.x * (int)gridSize.y;
        cells = new GameObject[numberOfCells];
        cellParent = new GameObject("Cells");

        //------------------------------------------------------------------------------------
        // LEVEL EDITING
        if (TOGGLEEDIT)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = Instantiate(playerPrefab);
                SpriteRenderer player_sr = players[i].GetComponent<SpriteRenderer>();
                player_sr.color = playerColors[i];
                players[i].transform.position = spawnPoints[i].transform.position;
            }

            CreateCells();
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].GetComponent<Cell>().isEditing = true;
            }
            for (int player = 0; player < players.Length; player++)
            {
                GameObject.Destroy(players[player].gameObject);
            }
        }
        else
        {
            LoadLevel(PlayerPrefs.GetString("CurrentLevel"));
        }
        //------------------------------------------------------------------------------------
    }

    void Update()
    {
        //------------------------------------------------------------------------------------
        // LEVEL EDITING
        if (TOGGLEEDIT)
        {
            SaveLevel(saveLevelName);
        }
        //------------------------------------------------------------------------------------
    }

    void CreateCells()
    {
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
    }

    void SaveLevel(string levelName)
    {
        // saveLevel *****************DEPRECIATED(TOO MUCH INVOLVEMENT)
        if (saveLevel)
        {
            string levelData = "";
            string path = Path.Combine(Application.streamingAssetsPath, "\\LevelData\\" + levelName + ".txt");
            float[][] levelPositions = new float[cells.Length][];

            // Only write the cells that are not toggled
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].GetComponent<Cell>().isToggled == false)
                {
                    // Writes to file: Index_posX_posY|Index_posX_posY|etc.
                    levelData += $"{i}-";
                }
            }
            File.WriteAllText(path, levelData);

            saveLevel = false;
        }
    }

    // This is multiple levels of jank, but it functions
    void LoadLevel(string levelName)
    {
        // Create cell grid
        CreateCells();
        // Read data from leveldata.txt, split strings into ints
        StreamReader sr = new StreamReader(Application.streamingAssetsPath + "\\LevelData\\" + levelName + ".txt");
        string[] tempStrings = sr.ReadToEnd().Split('-');
        int[] tempInts = new int[tempStrings.Length];

        for (int i = 0; i < tempInts.Length; i++)
        {
            int currentVal;
            string currentChar = tempStrings[i];
            if (int.TryParse(currentChar, out currentVal))
            {
                tempInts[i] = currentVal;
            }
        }
        sr.Close();

        // loop through ints, and toggle active cells
        for (int i = 0; i < tempInts.Length; i++)
        {
            cells[tempInts[i]].GetComponent<Cell>().isToggled = true;
        }

        // Flip toggled cells
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].GetComponent<Cell>().isToggled = !cells[i].GetComponent<Cell>().isToggled;
            if (cells[i].GetComponent<Cell>().isToggled)
            {
                cells[i].SetActive(false);
            }
        }
    }
}
