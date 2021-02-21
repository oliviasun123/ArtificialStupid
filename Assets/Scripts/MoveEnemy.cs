using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    // Use this for initialization

    private float accelerationTime = 2f;
    private float maxSpeed = 0.05f;
    private Vector2 movement;
    private float timeLeft;


    void Start()
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        rb.MovePosition((Vector2)transform.position + movement * maxSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explode"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            GetNewDirections();
        }
    }

    private void GetNewDirections()
    {
        //print("1" + movement);
        movement = new Vector2(GetNewXorY(movement.x), GetNewXorY(movement.y));
        //print("2" + movement);
        rb.MovePosition((Vector2)transform.position + movement * maxSpeed);
    }

    private float GetNewXorY(float z)
    {
		z = z * Random.Range(-2f, -1f);
		if (z > 1) z = z - 1;
		if (z < -1) z = z + 1;
		if (z < 0.1 && z > 0) z = 0.1f;
		if (z > -0.1 && z < 0) z = -0.1f; 
		return z;
    }
}
