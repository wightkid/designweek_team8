using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillMechanic : MonoBehaviour
{
    public int dashDistance = 4;
    private Rigidbody2D rb2D;
    public float dashDuration = 0.2f;
    public float dashSpeed = 1.0f;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && gameObject.GetComponent<MeleeWeponMechanic>().enabled == false)
        {
            StartCoroutine(DrillForce(Vector2.right));
        }
    }

    IEnumerator DrillForce(Vector2 force)
    {
        isDashing = true;
        rb2D.velocity = force * dashSpeed;

        float distanceTraveled = 0;
        Vector2 starPos = transform.position;

        while(distanceTraveled < dashDistance && dashDuration > 0)
        {
            dashDuration -= Time.deltaTime;
            distanceTraveled = Vector2.Distance(starPos, transform.position);
            yield return null;
        }

        rb2D.velocity = Vector2.zero;
        isDashing = false;
    }
}
