using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanic : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Rigidbody2D rb2D;
    private float bulletTimer = 50f;
    private Camera mainCam;
    private Vector3 mousePos;
    private bool hasHit = false;
    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        //This to make sure that the bullet will move in the mouse direction
        rb2D = GetComponent<Rigidbody2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);       
    }

    // Update is called once per frame
    void Update()
    {
        ////After 50 seconds, destroy the game object
        //bulletTimer--;
        //if (bulletTimer <= 0)
        //{
        //    Destroy(gameObject);
        //}

        
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
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the collided object has a specific tag 
        if (other.gameObject.CompareTag("Destructible"))
        {
            hasHit = true;
            Destroy(other.gameObject);
        }
    }
}
