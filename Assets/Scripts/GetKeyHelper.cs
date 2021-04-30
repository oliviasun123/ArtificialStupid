using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyHelper : MonoBehaviour
{
    public static GetKeyHelper Instance;
    // private SpriteRenderer sp;
    // public Sprite KeySprite;
    public GameObject KeyPrefab;
    public GameObject GemPrefab;


    private void Awake()
    {
        Instance = this;
        // sp = GetComponent<SpriteRenderer>();
    }

    public void GenerateKey(Vector2 pos)
    {
        // sp.sprite = KeySprite;
        GameObject keyObj = GameObject.Instantiate(KeyPrefab);
        keyObj.tag = "Untouch";
        keyObj.transform.position = pos;
        StartCoroutine(TouchableKey(keyObj));
    }

    public GameObject GenerateGem(Vector2 pos)
    {
        // sp.sprite = KeySprite;
        GameObject gemObj = GameObject.Instantiate(GemPrefab);
        // flash animation for gem out of box
        gemObj.tag = "Untouch";
        gemObj.transform.position = pos;        
        StartCoroutine(TouchableGem(gemObj));

        return gemObj;
    }

    IEnumerator TouchableKey(GameObject keyObj)
    {
        yield return new WaitForSeconds(1.0f);
        keyObj.tag = "Key";
    }
    IEnumerator TouchableGem(GameObject gemObj)
    {
        yield return new WaitForSeconds(1.0f);
        gemObj.tag = "Gem";
    }

}
