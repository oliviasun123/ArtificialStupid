using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guidetext : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Text text,bombs,health;
    public GameObject text2,bowtext,attention;
    public GameObject bow;
    float t1,t2,t3,t4,t5;
    bool showplaceflag=false,showbowflag=false,bowactive=false,attentionflag=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text=GameObject.Find("Canvas/Text").GetComponent<Text>();
        bombs=GameObject.Find("Canvas/TopBar/Bomb").GetComponent<Text>();
        health=GameObject.Find("Canvas/TopBar/HP").GetComponent<Text>();
        if((player.transform.position.x>=-7 || player.transform.position.y>=4)&&showplaceflag==false){
            text.text="Press SPACE to place a bomb, bomb can destory enemy and wall with the same color";
            text2.SetActive(true);
            showplaceflag=true;
            t1 = Time.fixedTime;
        }
        t2 = Time.fixedTime;
        if(t2 -t1 >=10)
        {
           text2.SetActive(false);
        }
        if(bombs.text!="9"&&showplaceflag==true&&showbowflag==false){
            t3=Time.fixedTime;
            showbowflag=true;
        }
        if(t2-t3>=2&&showbowflag==true&&bowactive==false){
            bowtext.SetActive(true);
            bow.SetActive(true);
            t4=Time.fixedTime;
            bowactive=true;
        }
        if(t2-t4>=5&&bowactive==true){
            bowtext.SetActive(false);
        }
        if(bombs.text=="2"||health.text=="2"&&attentionflag==false){
            attention.SetActive(true);
            t5=Time.fixedTime;
            attentionflag=true;
        }
        if(t2-t5>=8&&attentionflag==true){
            attention.SetActive(false);
        }
    }
}
