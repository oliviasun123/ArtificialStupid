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
        StartCoroutine(Touchable(keyObj));
    }

    public void GenerateGem(Vector2 pos)
    {
        // sp.sprite = KeySprite;
        GameObject gemObj = GameObject.Instantiate(GemPrefab);
        gemObj.transform.position = pos;
    }

    IEnumerator Touchable(GameObject keyObj)
    {
        yield return new WaitForSeconds(1.0f);
        keyObj.tag = "Key";
    }

}
