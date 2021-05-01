using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource theAS;

    public AudioClip bomb, playerTouch, monsterDeath, gameOver, swordEffect, bowEffect, coinEffect, openBox, gainPotion, storeSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        theAS = GetComponent<AudioSource>();
    }

    public void PlayTouch()
    {
        theAS.PlayOneShot(playerTouch);
    }


    public void PlayBomb()
    {
        theAS.PlayOneShot(bomb);
    }

    public void PlayMonsterDeath()
    {
        theAS.PlayOneShot(monsterDeath);
    }

    public void PlayGameOver()
    {
        theAS.PlayOneShot(gameOver);
    }
    
    public void PlaySwordGet()
    {
        theAS.PlayOneShot(swordEffect);
    }

    public void PlayBowGet()
    {
        theAS.PlayOneShot(bowEffect);
    }

    public void PlayCoinGet()
    {
        theAS.PlayOneShot(coinEffect);
    }

    public void PlayOpenBox()
    {
        theAS.PlayOneShot(openBox);
    }

    public void PlayPotionGet()
    {
        theAS.PlayOneShot(gainPotion);
    }

    public void PlayStoreSound()
    {
        theAS.PlayOneShot(storeSound);
    }

}
