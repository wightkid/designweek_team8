using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPowerup : MonoBehaviour
{
    public PowerUp powerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            powerUp.Use(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
