using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDoorUI : MonoBehaviour
{   

    public GameObject doorMsg;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (gameObject.tag == "Untagged" && other.CompareTag("Player")) 
        {   
            doorMsg.SetActive(true);
            StartCoroutine("finishDisplay");
        }
    }

    IEnumerator finishDisplay()
    {
        yield return new WaitForSeconds(2.0f);
        doorMsg.SetActive(false);
    }
}
