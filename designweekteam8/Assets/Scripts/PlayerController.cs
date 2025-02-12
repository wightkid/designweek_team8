using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Multiplayer functionality
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        gameObject.name = $"Player_{PlayerPrefs.GetInt("playerNumber")}";
        PlayerPrefs.SetInt("playerNumber", PlayerPrefs.GetInt("playerNumber") + 1);
    }

    private void OnEnable()
    {
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
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

    }

    public void OnPlayerJoined()
    {
        Debug.Log("JOINED");
    }

    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        var v = value.Get<Vector2>();
        Vector2 inputDir = v;
        //Debug.Log($"{v.x}, {v.y}");

        // Normalize and calculate the 'wish velocity'
        Vector2 direction = inputDir.normalized;
        Vector2 wishVelocity = direction * moveSpeed;

        // Interpolate smoothly between prev velocity and wish velocity
        float velocityX = Mathf.Lerp(rigidbody2d.velocity.x, wishVelocity.x, moveAccel * Time.deltaTime);
        float velocityY = Mathf.Lerp(rigidbody2d.velocity.y, wishVelocity.y, moveAccel * Time.deltaTime);

        // Add new velocity to rigidbody
        rigidbody2d.velocity = new Vector2(velocityX, velocityY);
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

}
