using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum playerCharacter
    {
        One,
        Two,
        Three,
        Four
    }

    private enum playerDirections
    {
        Up = 4,
        Down = 0,
        Side = 8,
    }

    // Power-Ups
    public PowerUp primaryPowerUp;
    public PowerUp secondaryPowerUp;

    // Movement
    public float moveSpeed = 10.0f;
    public float moveAccel = 5.0f;

    [SerializeField]
    private playerCharacter currentPlayerCharacter = playerCharacter.One;
    [SerializeField]
    private playerDirections currentPlayerDirection = playerDirections.Down;

    private Rigidbody2D rigidbody2d;

    // Sprites
    [SerializeField]
    public Sprite[] playerSpritesheet;
    private SpriteRenderer spriteRenderer;

    public Sprite[] otherSprites;
    public SpriteRenderer weaponSprite;
    public Sprite[] powerUpSprite;
    [SerializeField] private GameObject weapon;

    // Multiplayer Functionality
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    Vector2 inputDir;

    // Landmine
    public GameObject landminePrefab;

    private void Awake()
    {
        int playerNumber = PlayerPrefs.GetInt("playerNumber");

        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        gameObject.name = $"Player_{playerNumber}";

        gameObject.transform.position = GameObject.Find("Spawn Locations").transform.GetChild(playerNumber).position;

        player.Enable();

        if (playerNumber <= 3)
        {
            PlayerPrefs.SetInt("playerNumber", playerNumber + 1);
        }
        
        //GameObject.Find("GameManager").GetComponent<GameManager>().players[playerNumber - 1] = this.gameObject;
        //currentPlayerCharacter = 
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Calculate the index for the sprite
        int spriteIndex = (int)playerDirections.Down + (int)currentPlayerCharacter;

        // Debug logs to check values
        //Debug.Log($"playerDirections.Down: {(int)playerDirections.Down}");
        //Debug.Log($"currentPlayerCharacter: {(int)currentPlayerCharacter}");
        //Debug.Log($"Calculated spriteIndex: {spriteIndex}");
        //Debug.Log($"playerSpritesheet.Length: {playerSpritesheet.Length}");

        // Check if the index is within the bounds of the array
        if (spriteIndex >= 0 && spriteIndex < playerSpritesheet.Length)
        {
            spriteRenderer.sprite = playerSpritesheet[spriteIndex];
        }
        else
        {
            Debug.LogWarning($"Sprite index {spriteIndex} is out of bounds for playerSpritesheet array.");
        }

        if (weapon != null) weaponSprite = weapon.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UsePowerUp();


        //HandleMovement();

        // Landmine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject landmine = Object.Instantiate(landminePrefab);
            landmine.transform.position = transform.position;
        }
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
        inputDir = value.Get<Vector2>();
        // Normalize and calculate the 'wish velocity'
        Vector2 direction = inputDir.normalized;
        HandlePlayerSprite(direction);
        Vector2 wishVelocity = direction * moveSpeed;

        // Interpolate smoothly between prev velocity and wish velocity
        float velocityX = Mathf.Lerp(rigidbody2d.velocity.x, wishVelocity.x, moveAccel * Time.deltaTime);
        float velocityY = Mathf.Lerp(rigidbody2d.velocity.y, wishVelocity.y, moveAccel * Time.deltaTime);

        // Add new velocity to rigidbody
        rigidbody2d.velocity = new Vector2(velocityX, velocityY);
    }

    private void HandlePlayerSprite(Vector2 inputDirection)
    {
        // Not the best way to do something like this but this is the first idea i came up with -Jon-Marc

        // If not going up or down, determine if going left or right and flip sprite
        if (inputDirection.x == 0)
        {
            spriteRenderer.flipX = false;
            currentPlayerDirection = (inputDirection.y <= 0) ? playerDirections.Down : playerDirections.Up;
        }
        else
        {
            spriteRenderer.flipX = (inputDirection.x < 0) ? true : false;
            currentPlayerDirection = playerDirections.Side;
        }

        spriteRenderer.sprite = playerSpritesheet[(int)currentPlayerDirection + (int)currentPlayerCharacter];
    }
}
