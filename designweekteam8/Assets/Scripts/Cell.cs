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


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = renderOrder;

        if (deliverableParent == null)
        {
            deliverableParent = GameObject.Find("Deliverables");
        }

    }

    void Update()
    {
        
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
    void Destroy()
    {
        GameObject.Destroy(this);
    }
}
