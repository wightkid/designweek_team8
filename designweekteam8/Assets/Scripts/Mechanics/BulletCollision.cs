using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject != this.gameObject)
        {
            stats.health--;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Cell"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
