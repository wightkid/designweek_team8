using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanic : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Rigidbody2D rb2D;
    public float bulletTimer = 10f;
    private bool hasHit = false;
    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        //This to make sure that the bullet will move in the mouse direction
        rb2D = GetComponent<Rigidbody2D>();
        this.enabled = false; //Start disabled. Players start with melee
    }

    // Update is called once per frame
    void Update()
    {
        BulletFiring();        
    }

    public void BulletFiring()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            BulletDirection(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            BulletDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            BulletDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            BulletDirection(Vector2.down);
        }
    }

    public void BulletDirection(Vector2 direction)
    {
        float offsetDistance = 1f;

        Vector2 spawnPosition = (Vector2)transform.position + (direction * offsetDistance);

        //Generate the bullet in the scene when this function is called 
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();


        if(bulletRB != null )
        {
            bulletRB.velocity = direction * bulletSpeed;
        }
    }
}
