using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectScreen : MonoBehaviour
{
    public TextMeshPro[] tmps = new TextMeshPro[3];
    public GameObject GameManager;
    public int fontSizeDeslected = 10;
    public int fontSizeSelected = 12;
    int currentSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set current selection to have a larger font
        tmps[currentSelection].fontSize = fontSizeSelected;
    }

    // Update is called once per frame
    void Update()
    {
        // If within index bounds, and either W or S is pressed, update currentselection text
        if (Input.GetKeyDown(KeyCode.W) && currentSelection >= 1)
        {
            currentSelection--;
            UpdateSelectionText();
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentSelection <= 1)
        {
            currentSelection++;
            UpdateSelectionText();
        }

        // If space is pressed, set numberOfPlayers for game scene setup, and swap scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("NumberOfPlayers", currentSelection + 2);
            PlayerPrefs.SetString("CurrentLevel", "LevelOne");
            SceneManager.LoadScene(2);
        }

    }

    // Update fontsize for text selection
    void UpdateSelectionText()
    {
        // Loop through all player number options, and set fontsize to deselected size
        for (int i = 0; i < tmps.Length; i++)
        {
            tmps[i].fontSize = fontSizeDeslected;
        }

        // Update current selection fontsize to selected size
        tmps[currentSelection].fontSize = fontSizeSelected;
    }
}
