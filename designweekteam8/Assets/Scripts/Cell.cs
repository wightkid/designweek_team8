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


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = renderOrder;

        deliverableParent = GameObject.Find("Deliverables");
        Physics2D.queriesHitTriggers = true;
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject deliverable = Instantiate(deliverablePrefabs[0], gameObject.transform.position, Quaternion.identity);
            deliverable.transform.SetParent(deliverableParent.transform);
            Destroy();
        }

    }

    private void Update()
    {

    }


    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
