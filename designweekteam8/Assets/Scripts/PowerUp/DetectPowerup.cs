using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPowerup : MonoBehaviour
{
    public PowerUp powerUp;
    public bool powerUpActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerUpActive = false;
        if (collision.CompareTag("Player"))
        {
            powerUp.Use(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
