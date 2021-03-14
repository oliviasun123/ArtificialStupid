using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: unused file by now
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

    public void GenerateGem(Vector2 pos)
    {
        // sp.sprite = KeySprite;
        GameObject gemObj = GameObject.Instantiate(GemPrefab);
        gemObj.tag = "Untouch";
        gemObj.transform.position = pos;
        StartCoroutine(TouchableGem(gemObj));
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
