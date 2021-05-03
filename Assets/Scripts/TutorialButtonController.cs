using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialButtonController : MonoBehaviour
{   
    public static TutorialButtonController Instance;

    public Button btn_YES, btn_NO;
    // public GameObject tutorialShowObj;
    private GameObject player;
    private GameObject dataController;
    private void Start()
    {   
        Instance = this;

        player = GameObject.Find("Player");
        dataController = GameObject.Find("PlayerDataController");
        InitButton();
        // if (!TutorialData.Instance.GetTutorialFlag())
        // {   
        // }
        // else 
        // {   
            // tutorialShowObj.SetActive(false);
        // }
    }


    public void InitButton()
    {
        btn_YES.onClick.AddListener(() =>
        {   
            Time.timeScale = 1;
            Destroy(player);
            Destroy(dataController);
            SceneManager.LoadScene("sample");
            // TutorialData.Instance.SetTutorialFlag(true);
        });

        btn_NO.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
        });
    }

}
