using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controlpage : MonoBehaviour
{
    public Button btn_new, btn_setting;


    private void Awake()
    {
        btn_new.onClick.AddListener(() => {
            SceneManager.LoadScene("Level1");
        });

        btn_setting.onClick.AddListener(() => {
            SceneManager.LoadScene("Start");
        });

    }
}
