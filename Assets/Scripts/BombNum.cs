using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// TODO: unused file by now
public class BombNum : MonoBehaviour
{
	public static int num = 8;
	public Text BombText;
    // Start is called before the first frame update
    void Start()
    {
        displayNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
        	if(num == 0) {
        		return;
        	}
            num -= 1;
        }
        displayNum();
    }
    public void addNum() {
    	num += 1;
    	displayNum();
    }
    public void loseNum() {
    	num -= 1;
    	displayNum();
    }
    public void displayNum() {
        BombText.text = num.ToString();
    }
    public int getNum() {
    	return num;
    }
}
