using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float value = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player dosen't have player stats script some how, exit early to not cause errors 

        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (playerStats == null) return;

        playerStats.PickUpCoin(this);
    }
}
