using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer sr;
    public Vector2 size;
    public int renderOrder = 1;
    public GameObject[] deliverablePrefabs;
    public GameObject deliverableParent;
    public bool isToggled = false;
    public bool isEditing = false;


    private void Awake()
    {
        // Create sprite renderer and set sorting order
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = renderOrder;

        // Find deliverables parent, enable queriesHitTriggers for levelEditing
        deliverableParent = GameObject.Find("Deliverables");
        Physics2D.queriesHitTriggers = true;
    }


    private void OnMouseDown()
    {
        // Only allow toggling sprite renderer of a cell if isEditing is enabled
        if (isEditing)
        {
            isToggled = !isToggled;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* 
        // Temproary player collision with cells
        if (collision.gameObject.tag == "Player")
        {
            GameObject deliverable = Instantiate(deliverablePrefabs[0], gameObject.transform.position, Quaternion.identity);
            deliverable.transform.SetParent(deliverableParent.transform);
            Destroy();
        }
        */
    }

    private void Update()
    {
        sr.enabled = !isToggled;
    }


    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
