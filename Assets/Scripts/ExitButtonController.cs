using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonController : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        GameObject dataController = GameObject.Find("PlayerDataController");

        Button exitButton = gameObject.GetComponent<Button>();
        exitButton.onClick.AddListener(() =>
        {
            Destroy(player);
            Destroy(dataController);
        });
    }
}
