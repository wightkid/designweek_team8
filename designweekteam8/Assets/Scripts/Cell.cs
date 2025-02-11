using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer sr;
    public Vector2 position;
    public Vector2 size;
    public int renderOrder = 1;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = renderOrder;
        
    }

    void Update()
    {
        
    }
}
