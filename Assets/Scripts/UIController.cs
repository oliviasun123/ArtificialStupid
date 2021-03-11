using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text txt_HP, txt_Bomb;
    public GameObject img_gameover;
    public GameObject img_level;

    private void Awake()
    {
        Instance = this;
        InitTryAgain();
    }

    public void RefreshInfo(int HP, int bomb)
    {
        txt_HP.text = "HP : " + HP.ToString();
        txt_Bomb.text = "Bomb : " + bomb.ToString();
    }

    public void ShowGameOver()
    {   
        img_level.SetActive(false);
        img_gameover.SetActive(true);
    }

    private void InitTryAgain() 
    {
        img_gameover.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });

        // print(txt_HP.text[txt_HP.text.Length - 1]);
        // print(txt_Bomb.text[txt_Bomb.text.Length - 1]);
    }

    public int[] GetBasicInfo()
    {
        int HP = int.Parse(txt_HP.text[txt_HP.text.Length - 1] + "");
        int BombCount = int.Parse(txt_Bomb.text[txt_Bomb.text.Length - 1] + "");
        return new int[] {HP, BombCount};
    }
}
