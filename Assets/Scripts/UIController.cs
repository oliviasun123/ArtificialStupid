using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text txt_HP, txt_Bomb;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshInfo(int HP, int bomb)
    {
        txt_HP.text = "HP : " + HP.ToString();
        txt_Bomb.text = "Bomb : " + bomb.ToString();
    }
}
