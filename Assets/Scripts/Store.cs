using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public int coins_per_goods_1 = 1;
    public int coins_per_goods_2 = 1;
    public int coins_per_goods_3 = 1;
    public int coins_per_goods_4 = 1;
    // public int coins_per_goods_5 ;

    public Text coins_each_1, coins_each_2, coins_each_3, coins_each_4;

    public Button btn_goods_1_more, btn_goods_1_less;
    public Button btn_goods_2_more, btn_goods_2_less;
    public Button btn_goods_3_more, btn_goods_3_less;
    public Button btn_goods_4_more, btn_goods_4_less;
    public Button btn_reset, btn_confirm, btn_return;
    public Text txt_goods_1, txt_goods_2, txt_goods_3, txt_goods_4;
    public Text txt_coins, txt_final_1, txt_final_2, txt_final_3, txt_final_4;

    void Start()
    {
        coins_per_goods_1 = int.Parse(coins_each_1.text);
        coins_per_goods_2 = int.Parse(coins_each_2.text);
        coins_per_goods_3 = int.Parse(coins_each_3.text);
        coins_per_goods_4 = int.Parse(coins_each_4.text);
        
        int[] basics = GameData.Instance.GetStoreList();
        txt_coins.text = basics[0].ToString();
        txt_final_1.text = basics[1].ToString();
        txt_final_2.text = basics[2].ToString();
        txt_final_3.text = basics[3].ToString();
        txt_final_4.text = basics[4].ToString();
    }

    private void Awake()
    {

        btn_return.onClick.AddListener(() => {
            
            // TODO 这里的result数组，是{金币，弓，剑，紫药水，黄药水}传给下一个scene
            int[] result = new int[] {int.Parse(txt_coins.text), int.Parse(txt_final_1.text), int.Parse(txt_final_2.text), int.Parse(txt_final_3.text), int.Parse(txt_final_4.text)};
            GameData.Instance.SetupStore(result);
            SceneManager.LoadScene(GameData.Instance.GetLevel() + 1);
         });

        btn_goods_1_more.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin - coins_per_goods_1;
            int before_goods = int.Parse(txt_goods_1.text);
            int after_goods = before_goods + 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_1.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_1_less.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin + coins_per_goods_1;
            int before_goods = int.Parse(txt_goods_1.text);
            int after_goods = before_goods - 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_1.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_2_more.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin - coins_per_goods_2;
            int before_goods = int.Parse(txt_goods_2.text);
            int after_goods = before_goods + 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_2.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_2_less.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin + coins_per_goods_2;
            int before_goods = int.Parse(txt_goods_2.text);
            int after_goods = before_goods - 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_2.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });
        
        btn_goods_3_more.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin - coins_per_goods_3;
            int before_goods = int.Parse(txt_goods_3.text);
            int after_goods = before_goods + 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_3.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_3_less.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin + coins_per_goods_3;
            int before_goods = int.Parse(txt_goods_3.text);
            int after_goods = before_goods - 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_3.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_4_more.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin - coins_per_goods_4;
            int before_goods = int.Parse(txt_goods_4.text);
            int after_goods = before_goods + 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_4.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_goods_4_less.onClick.AddListener(() => {

            int before_coin = int.Parse(txt_coins.text);
            int after_coin = before_coin + coins_per_goods_4;
            int before_goods = int.Parse(txt_goods_4.text);
            int after_goods = before_goods - 1;
 
            if (after_coin < 0 || after_goods < 0)
            {
                return ;
            }
            txt_goods_4.text = after_goods.ToString();
            txt_coins.text = after_coin.ToString();
         });

        btn_reset.onClick.AddListener(() => {
            int after_coin = int.Parse(txt_coins.text);

            after_coin = after_coin + int.Parse(txt_goods_1.text) * coins_per_goods_1;
            after_coin = after_coin + int.Parse(txt_goods_2.text) * coins_per_goods_2;
            after_coin = after_coin + int.Parse(txt_goods_3.text) * coins_per_goods_3;
            after_coin = after_coin + int.Parse(txt_goods_4.text) * coins_per_goods_4;
            txt_goods_1.text = "0";
            txt_goods_2.text = "0";
            txt_goods_3.text = "0";
            txt_goods_4.text = "0";
            txt_coins.text = after_coin.ToString();
         });

        btn_confirm.onClick.AddListener(() => {
            txt_final_1.text = (int.Parse(txt_final_1.text) + int.Parse(txt_goods_1.text)).ToString();
            txt_final_2.text = (int.Parse(txt_final_2.text) + int.Parse(txt_goods_2.text)).ToString();
            txt_final_3.text = (int.Parse(txt_final_3.text) + int.Parse(txt_goods_3.text)).ToString();
            txt_final_4.text = (int.Parse(txt_final_4.text) + int.Parse(txt_goods_4.text)).ToString();
            txt_goods_1.text = "0";
            txt_goods_2.text = "0";
            txt_goods_3.text = "0";
            txt_goods_4.text = "0";
         });
    }
}
