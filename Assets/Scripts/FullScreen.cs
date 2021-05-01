using UnityEngine;  
using System.Collections;  
  
public class FullScreen : MonoBehaviour {  
  
    // Use this for initialization  
    void Start () {  
  
       
    }  
      
    // Update is called once per frame  
    void Update () {  
      
        //  按ESC退出全屏  
        if(Input.GetKey(KeyCode.Escape))  
        {  
            Screen.fullScreen = false;  //退出全屏           
             
        }  
        //设置为1366*768不全屏  
        if (Input.GetKey(KeyCode.V))  
        {  
             
            Screen.SetResolution(1366, 768, false);  
  
        }  
        //设置1366*768的全屏  
        if (Input.GetKey(KeyCode.B))  
        {  
             Screen.SetResolution(1366, 768, true);  
         
        }  
  
        //按A全屏  
        if (Input.GetKey(KeyCode.A))  
        {  
            //获取设置当前屏幕分辩率  
            Resolution[] resolutions = Screen.resolutions;  
            //设置当前分辨率  
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);  
  
            Screen.fullScreen = true;  //设置成全屏,  
       }  
    }  
  
}  