using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// TODO: unused file by now
public class health : MonoBehaviour
{
	public int hp;
	public Text HPText;
    // Start is called before the first frame update
    void Start()
    {
        displayHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gain() {
        hp += 1;
    	displayHP();
    }
    public void lose() {
    	hp -= 1;
    	displayHP();
    }
    public void displayHP() {
        HPText.text = hp.ToString();
    }
}
