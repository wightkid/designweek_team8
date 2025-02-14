using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public UnityEvent damageTaken = new UnityEvent();
    public UnityEvent killed = new UnityEvent();
    public UnityEvent moneyPickedUp = new UnityEvent();

    public GameObject coinPrefab;

    public bool resetMoneyOnDeath = true;
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
        // Removed due to Start on gamepad = kill player -bc
        if (Input.GetKeyDown(KeyCode.Escape)) Kill();
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
        DropMoney();
        Debug.Log("Player Killed");
    }

    public void PickUpCoin(Coin coin)
    {
        money += coin.value;
        moneyPickedUp.Invoke();

        Destroy(coin.gameObject);
    }

    public void DropMoney()
    {
        // Make the money the player is supposed to drop the value of the coin
        Coin coinScript = coinPrefab.GetComponent<Coin>();
        coinScript.value = money * dropPercentage;
        money = (resetMoneyOnDeath) ? 0 : money; // Reset money if bool is true, otherwise just keep it the same

        // Instantiate dropped coin
        GameObject coinInstance = Instantiate(coinScript.gameObject);
        coinInstance.transform.position = transform.position + new Vector3(2, 0, 0); // Vector offset is just so that player dosent pick up again
    }
}
