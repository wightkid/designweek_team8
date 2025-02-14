using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            collision.gameObject.GetComponent<Cell>().DropDeliverable();
            Destroy(collision.gameObject);
        }
    }
}
