using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{   
    private Animation anim;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player") || other.CompareTag("Killer"))
        {   
            if (gameObject.tag == "Box")
            {
                if (UIController.Instance.KeyAvailable())
                {
                    GameObject gemObj = GetKeyHelper.Instance.GenerateGem(transform.position);
                    UIController.Instance.HideKey();
                    Destroy(gameObject);
                    anim = gemObj.GetComponent<Animation>();
                    anim.Play("prop_diamond");
                }
            }
            else if (gameObject.tag != "Untouch")
            {
                Destroy(gameObject);
            }
        }
    }
}
