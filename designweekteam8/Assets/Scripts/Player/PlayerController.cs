using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Power-Ups
    public PowerUp primaryPowerUp;
    public PowerUp secondaryPowerUp;

    // Movement
    public float moveSpeed = 10.0f;
    public float moveAccel = 15.0f;

    private Rigidbody2D rigidbody2d;

    // Multiplayer Functionality
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    public bool isTestScene = false;

    private void Awake()
    {

        int playerNumber = PlayerPrefs.GetInt("playerNumber");

        if (isTestScene)
        {
            playerNumber = 1;
        }


        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        gameObject.name = $"Player_{playerNumber}";

        gameObject.transform.position = GameObject.Find("Spawn Locations").transform.GetChild(playerNumber).position;

        player.Enable();

        if (playerNumber <= 3)
        {
            PlayerPrefs.SetInt("playerNumber", playerNumber + 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UsePowerUp();
        
        //HandleMovement();
    }

    private void OnEnable()
    {
        move = player.FindAction("Move");
        
    }

    private void OnDisable()
    {
        player.Disable();
    }

    private void UsePowerUp()
    {
        // Check if player has Power Ups and safety check for if some how secondary Power Up dosen't get set to primary
        if (primaryPowerUp == null)
        {
            // No Power Ups, return early
            if (secondaryPowerUp == null) return;

            // No primary Power Up but secondary Power Up is available, set it to primary
            primaryPowerUp = secondaryPowerUp;
        }

        //// Use Power Up
        //primaryPowerUp.Use();
        primaryPowerUp = null;

        // After successful use of primary Power Up move secondary to primary
        if (secondaryPowerUp) 
        {
            primaryPowerUp = secondaryPowerUp;
            secondaryPowerUp = null;
        }
    }
    
    private void OnMove(InputValue value)
    {
        // Get player input
        //float inputDirX = Input.GetAxisRaw("Horizontal");
        //float inputDirY = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = value.Get<Vector2>();
        
        // Normalize and calculate the 'wish velocity'
        Vector2 direction = inputDir.normalized;
        Vector2 wishVelocity = direction * moveSpeed;

        // Interpolate smoothly between prev velocity and wish velocity
        float velocityX = Mathf.Lerp(rigidbody2d.velocity.x, wishVelocity.x, moveAccel * Time.deltaTime);
        float velocityY = Mathf.Lerp(rigidbody2d.velocity.y, wishVelocity.y, moveAccel * Time.deltaTime);

        // Add new velocity to rigidbody
        rigidbody2d.velocity = new Vector2(velocityX, velocityY);
    }
}
