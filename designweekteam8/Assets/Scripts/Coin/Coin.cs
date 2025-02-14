using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float value = 5.0f;
    public bool playerDropped = false;

    public Sprite[] coinSpritesheet;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player dosen't have player stats script some how, exit early to not cause errors 

        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (playerStats == null) return;

        playerStats.PickUpCoin(this);
    }

    private void UpdateSprite()
    {
        if (playerDropped)
        {
            spriteRenderer.sprite = coinSpritesheet[6];
            return;
        }

        int index = (int)Mathf.Min(Mathf.Floor(value / 10f), 5);

        spriteRenderer.sprite = coinSpritesheet[index];
    }
}
