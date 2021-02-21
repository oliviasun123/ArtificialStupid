using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{   
    private float delayTime = 1.5f;

    public void InitBomb(int range, float delayTime)
    {
        // this.explodeRange = range;
        StartCoroutine("explode", delayTime);
    }

    IEnumerator explode(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.tag = "Explode";
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * 2.0f;
        yield return new WaitForSeconds(delayTime / 4);
        Destroy(gameObject);
    }

}
