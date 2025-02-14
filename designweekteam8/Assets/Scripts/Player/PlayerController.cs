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

    //Sprites
    public SpriteRenderer playerSprite;
    public Sprite[] otherSprites;
    public SpriteRenderer weaponSprite;
    public Sprite[] powerUpSprite;
    [SerializeField] private GameObject weapon;
    // Multiplayer Functionality
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        if(weapon != null)
        {
            weaponSprite = weapon.GetComponent<SpriteRenderer>();
        }
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

        ////Change the sprite depending the direction of player
        //ChangeSprite(direction);
    }

    //private void ChangeSprite(Vector2 direction)
    //{
    //    if (direction.x > 0)
    //    {
    //        playerSprite.sprite = otherSprites[1];
    //        playerSprite.flipX = false;
    //        if(gameObject.GetComponent<BulletMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[1];
    //            weaponSprite.flipX = false;
    //        }
    //        else if (gameObject.GetComponent<MeleeWeponMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[0];
    //            weaponSprite.flipX = false;
    //        }
            
    //    }
    //    if (direction.x < 0)
    //    {
    //        playerSprite.sprite = otherSprites[1];
    //        playerSprite.flipX = true;
    //        if (gameObject.GetComponent<BulletMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[1];
    //            weaponSprite.flipX = true;
    //        }
    //        else if (gameObject.GetComponent<MeleeWeponMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[0];
    //            weaponSprite.flipX = true;
    //        }
    //    }
    //    if (direction.y > 0)
    //    {
    //        playerSprite.sprite = otherSprites[2];
    //        playerSprite.flipY = false;
    //        if (gameObject.GetComponent<BulletMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[1];
    //            weaponSprite.flipX = false;
    //        }
    //        else if (gameObject.GetComponent<MeleeWeponMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[0];
    //            weaponSprite.flipX = false;
    //        }
    //    }
    //    if (direction.y < 0)
    //    {
    //        playerSprite.sprite = otherSprites[0];
    //        playerSprite.flipY = false;
    //        if (gameObject.GetComponent<BulletMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[2];
    //            weaponSprite.flipX = false;
    //        }
    //        else if(gameObject.GetComponent<MeleeWeponMechanic>().enabled == true)
    //        {
    //            weaponSprite.sprite = powerUpSprite[0];
    //            weaponSprite.flipX = false;
    //        }
    //    }
    //}
}
