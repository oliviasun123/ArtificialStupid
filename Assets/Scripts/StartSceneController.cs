using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public Button btn_new, btn_quit, btn_setting, btn_tutorial;


    private void Awake()
    {
        btn_new.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });

        btn_setting.onClick.AddListener(() => {
            SceneManager.LoadScene("GameControl");
        });

        btn_quit.onClick.AddListener(() => {
            Application.Quit();
        });

        btn_tutorial.onClick.AddListener(() => {
            SceneManager.LoadScene("sample");
        });
    }
}
