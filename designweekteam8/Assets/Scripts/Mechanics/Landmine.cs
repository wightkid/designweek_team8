using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    // Start is called before the first frame update

    public float timer;
    public float timerMax = 0.5f;

    private float explodeBuffer;
    private float explodeBufferMax;

    public AudioClip explosionSound;

    CircleCollider2D collider;

    void Start()
    {
        timer = timerMax;
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;

        explodeBufferMax = 0.1f;
        explodeBuffer = explodeBufferMax;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Explode();
        }

        Debug.Log($"t={timer}, e={explodeBuffer}");
    }

    void Explode()
    {
        collider.enabled = true;
        explodeBuffer -= Time.deltaTime;
        
        if (explodeBuffer < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
