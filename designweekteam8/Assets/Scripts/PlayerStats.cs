using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public UnityEvent damageTaken = new UnityEvent();
    public UnityEvent killed = new UnityEvent();
    public UnityEvent moneyPickedUp = new UnityEvent();
    
    public float dropPercentage = 0.5f;
    
    public float money = 0.0f;
    public int health = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damage) 
    {
        damageTaken.Invoke();
        
        health -= damage;

        if (health < 0) Kill();
    }


    public void Kill()
    {
        killed.Invoke();
        
        health = 0;

        // NOTE: need more functionality here
    }
}
