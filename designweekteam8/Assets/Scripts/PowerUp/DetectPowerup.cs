using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPowerup : MonoBehaviour
{
    //This script should be placed on the powerup game object

    public PowerUp powerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When player collides with power up object 
        if (collision.CompareTag("Player"))
        {
            //Activate the power up script of the current power up
            powerUp.Use(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
