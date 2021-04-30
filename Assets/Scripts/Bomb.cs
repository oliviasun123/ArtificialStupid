using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float delayTime = 1.5f;
    private SpriteRenderer sp;
    public Sprite greenBomb;
    public Sprite blueBomb;
    private bool isGreen;
    public GameObject boomEffect;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void InitBomb(float explodeRange, bool isGreen)
    {
        this.isGreen = isGreen;

        if (isGreen)
        {
            sp.sprite = greenBomb;
        }
        else
        {
            sp.sprite = blueBomb;
        }
        StartCoroutine("explode", explodeRange);
    }

    IEnumerator explode(float explodeRange)
    {
        yield return new WaitForSeconds(delayTime);
        
        // create bomb effect at the same location TODO: add tag, change size(explode range)
        GameObject bombEffectObj = Instantiate(boomEffect, transform.position, Quaternion.identity);
        bombEffectObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * explodeRange;

        if (isGreen)
        {
            bombEffectObj.tag = "GreenBomb";
        }
        else
        {
            bombEffectObj.tag = "BlueBomb";
        }
        // old way to explode ↓
        // gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * explodeRange;


        yield return new WaitForSeconds(delayTime / 4);
        Destroy(gameObject);
    }

}
