using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinalDoorController : MonoBehaviour
{
    public GameObject[] finalDoors = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        int num1 = rand.Next(0, 4);
        int num2 = num1;
        while (num2 == num1)
        {
            num2 = rand.Next(0, 4);
        }

        for (int i = 0; i < finalDoors.Length; i++)
        {   
            GameObject door = finalDoors[i];
            if (i != num1 && i != num2)
            {
                door.SetActive(false);
            }
            else if (i == num1)
            {
                door.tag = "Untagged";
            }
        }

    }

}
