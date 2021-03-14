using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GreenBomb") && gameObject.tag == "GreenWall")
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("BlueBomb") && gameObject.tag == "BlueWall")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("GreenBomb") && gameObject.tag == "GreenWall")
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("BlueBomb") && gameObject.tag == "BlueWall")
        {
            Destroy(gameObject);
        }
    }
}
