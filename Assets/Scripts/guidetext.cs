using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guidetext : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Text text;
    public GameObject text2;
    float t1,t2;
    bool flag=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position.x>=-7 || player.transform.position.y>=4)&&flag==false){
            text=GameObject.Find("Canvas/Text").GetComponent<Text>();
            text.text="Press SPACE to place a bomb, bomb can destory enemy and wall with the same color";
            text2.SetActive(true);
            flag=true;
            t1 = Time.fixedTime;
        }
        t2 = Time.fixedTime;
        if(t2 -t1 >=5)
        {
           text2.SetActive(false);
        }
    }
}
