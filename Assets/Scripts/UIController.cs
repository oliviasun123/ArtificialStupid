using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text txt_HP, txt_Bomb, txt_Sword, txt_Money, txt_Gem;
    public GameObject img_Key;
    public GameObject img_gameover;
    public GameObject img_level;

    public Button btn_tryagain;
    public Button btn_exit, btn_retry;

    private void Awake()
    {
        Instance = this;
        InitTryAgain();
        InitDialog();
    }

    public void RefreshInfo(int HP, int bomb, int sword, int money, int gem)
    {
        txt_HP.text = "" + HP.ToString();
        txt_Bomb.text = "" + bomb.ToString();
        txt_Sword.text = "" + sword.ToString();
        txt_Money.text = "" + money.ToString();
        txt_Gem.text = "" + gem.ToString();
    }

    public void DisplayKey()
    {
        img_Key.SetActive(true);
    }

    public void HideKey()
    {
        img_Key.SetActive(false);
    }

    public bool KeyAvailable()
    {
        return img_Key.activeSelf;
    }

    public void ShowGameOver()
    {
        img_level.SetActive(false);
        img_gameover.SetActive(true);
    }

    private void InitTryAgain()
    {
        btn_tryagain.onClick.AddListener(() =>
        {   
            SceneManager.LoadScene("SampleScene");
        });

        // print(txt_HP.text[txt_HP.text.Length - 1]);
        // print(txt_Bomb.text[txt_Bomb.text.Length - 1]);
    }

    private void InitDialog()
    {
        btn_retry.onClick.AddListener(() =>
        {
            Scene scene = SceneManager.GetActiveScene ();
            SceneManager.LoadScene(scene.name);
            // GUILayout.Label ("当前场景: " + scene.name);
        });
        btn_exit.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Start");
        });
    }

    public int[] GetBasicInfo()
    {
        int HP = int.Parse(txt_HP.text[txt_HP.text.Length - 1] + "");
        int BombCount = int.Parse(txt_Bomb.text[txt_Bomb.text.Length - 1] + "");
        return new int[] { HP, BombCount };
    }
}
