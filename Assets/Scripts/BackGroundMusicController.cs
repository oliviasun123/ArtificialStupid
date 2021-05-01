using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicController : MonoBehaviour
{
    private AudioSource theAS;
    public static BackGroundMusicController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        theAS = GetComponent<AudioSource>();
    }

    public void StopBackGround() 
    {
        theAS.Stop();
    }
    
}   
