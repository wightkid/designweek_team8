using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Power-Ups
    // Playertag
    public int playerNumber = 0;
    public int coinsCollected = 0;

    // Movement
    public float moveSpeed = 10.0f;
    public float moveAccel = 15.0f;
    

    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleMovement();
    }

   
    private void HandleMovement()
    {
        // Get player input
        float inputDirX = Input.GetAxisRaw("Horizontal");
        float inputDirY = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = new Vector2(inputDirX, inputDirY);
        
        // Normalize and calculate the 'wish velocity'
        Vector2 direction = inputDir.normalized;
        Vector2 wishVelocity = direction * moveSpeed;

        // Interpolate smoothly between prev velocity and wish velocity
        float velocityX = Mathf.Lerp(rigidbody2d.velocity.x, wishVelocity.x, moveAccel * Time.deltaTime);
        float velocityY = Mathf.Lerp(rigidbody2d.velocity.y, wishVelocity.y, moveAccel * Time.deltaTime);

        // Add new velocity to rigidbody
        rigidbody2d.velocity = new Vector2(velocityX, velocityY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destructable")
        {
            GameObject.Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinsCollected++;
            GameObject.Destroy(collision.gameObject);
        }
    }
}
