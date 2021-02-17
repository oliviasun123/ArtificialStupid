using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    // Use this for initialization
    void Start () 
    {
        rb = GetComponent<Rigidbody2D> ();
    }

 	  public float accelerationTime = 2f;
 	  public float maxSpeed = 5f;
 	  private Vector2 movement;
 	  private float timeLeft;
  
 	  void Update()
 	  {
   		timeLeft -= Time.deltaTime;
   		if(timeLeft <= 0)
   		{
    		movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
     		timeLeft += accelerationTime;
   		}
 	}
 
 	void FixedUpdate()
 	{
   	rb.AddForce(movement * maxSpeed);
 	}
 	
 }
