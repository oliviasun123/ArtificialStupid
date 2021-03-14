using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {   
            if (gameObject.tag == "Box")
            {
                if (UIController.Instance.KeyAvailable())
                {
                    GetKeyHelper.Instance.GenerateGem(transform.position);
                    Destroy(gameObject);
                }
            }
            else if (gameObject.tag != "Untouch")
            {
                Destroy(gameObject);
            }
        }
    }
}
