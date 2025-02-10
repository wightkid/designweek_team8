using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int numberOfPlayers = 0;


    void Start()
    {
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        Debug.Log(numberOfPlayers);
    }

    void Update()
    {
        
    }
}
