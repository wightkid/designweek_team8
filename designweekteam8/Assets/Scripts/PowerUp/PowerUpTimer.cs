using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
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
        if(melee.enabled == false)
        {
            powerUpTimer -= Time.deltaTime;
            if(powerUpTimer <= 0 )
            {
                powerUp.Use(player);
            }
        }
    }
}
