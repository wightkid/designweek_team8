using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
    //Written so that no matter which power up we have
    //will always switch back to standard melee once timer ends

    public float powerUpTimer = 10f;
    public PowerUp powerUp;
    MeleeWeponMechanic melee;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        melee = FindAnyObjectByType<MeleeWeponMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the melee script has been disabled
        if(melee.enabled == false)
        {
            powerUpTimer -= Time.deltaTime; //Start timer for how long the power up lasts
            if(powerUpTimer <= 0 )
            {
                powerUp.Use(player);
            }
        }
    }
}
