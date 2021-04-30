using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System;


public class Store : MonoBehaviour
{
    // btns for add/remove goods
    public Button btn_goods_1_more, btn_goods_1_less;
    public Button btn_goods_2_more, btn_goods_2_less;
    public Button btn_goods_3_more, btn_goods_3_less;
    public Button btn_reset, btn_confirm, btn_return;

    // UI label to show info  of goods 
    public Text txt_goods_1, txt_goods_2, txt_goods_3; // selected num of goods
    public int coins_per_goods_1 = 1; // show price 
    public int coins_per_goods_2 = 1;
    public int coins_per_goods_3 = 1;
    public Image img1, img2, img3; // show img 
    public Sprite sprite_key, sprite_yellow, sprite_purple, sprite_row, sprite_sword;
    // UI labls to show num of existed goods 
    public Text final_1, final_2, final_3;

    // goods setting
    public Dictionary<string, object>[] selected_goods;
    // data load from goods setting 
    public Text coins_each_1, coins_each_2, coins_each_3;

    // data load from game round
    public Text txt_coins;
    public Dictionary<string, int> total_count;

    void Start()
    {   
        // loads data and set UI from our goods setting
        LoadGoods(); 
        coins_per_goods_1 = (int) selected_goods[0]["price"];
        coins_per_goods_2 = (int) selected_goods[1]["price"];
        coins_per_goods_3 = (int) selected_goods[2]["price"];
        coins_each_1.text = coins_per_goods_1.ToString();
        coins_each_2.text = coins_per_goods_2.ToString();
        coins_each_3.text = coins_per_goods_3.ToString();
        img1.sprite = (Sprite)selected_goods[0]["img"];
        img2.sprite = (Sprite) selected_goods[1]["img"];
        img3.sprite = (Sprite) selected_goods[2]["img"];


        // loda data from current level
        //with order { 金币，弓，剑，紫药水，黄药水}
        int[] basics = GameData.Instance.GetStoreList();
        txt_coins.text = basics[0].ToString();
        total_count = new Dictionary<string, int>();
        total_count["bow"] = basics[1];
        total_count["sword"] = basics[2];
        total_count["purple"] = basics[3];
        total_count["yellow"] = basics[4];

        // show num of existed goods
        final_1.text = total_count[(string)selected_goods[0]["name"]].ToString();
        final_2.text = total_count[(string)selected_goods[1]["name"]].ToString();
        final_3.text = total_count[(string)selected_goods[2]["name"]].ToString();

        //debug info
        PrintDebugInfo();
    }

    public void LoadGoods()
    {
        /* Loads avaliable goods from Diction and select
         * random 3 of them 
         */
        Dictionary<string, object>[] all_goods = new Dictionary<string, object>[] {
            //new Dictionary<string, object>(){{"name","key"},{"img",sprite_key},{"price",1}},
            new Dictionary<string, object>(){{"name","bow"},{"img",sprite_row},{"price",3}},
            new Dictionary<string, object>(){{"name","sword"},{"img",sprite_sword},{"price",2}},
            new Dictionary<string, object>(){{"name","purple"},{"img",sprite_purple },{"price",2}},
            new Dictionary<string, object>(){{"name","yellow"},{"img", sprite_yellow },{"price",2}}
        };

        // select 3 random goods idx
        System.Random rand = new System.Random();
        int[] idx = new int[] { -1, -1, -1 };
        while (Array.IndexOf(idx, -1) != -1)
        {
            int tmp_id = Array.IndexOf(idx, -1);
            int rand_num = rand.Next(all_goods.Length);
            if (Array.IndexOf(idx, rand_num) == -1)
            {
                idx[tmp_id] = rand_num;
            }
        }
        this.selected_goods = new Dictionary<string, object>[] {
            all_goods[idx[0]],all_goods[idx[1]],all_goods[idx[2]]
        };
    }

    public void PrintDebugInfo()
    {
        Debug.Log("===========Goods Info ===============");
        Debug.Log("Coins: " + txt_coins.text);
        Debug.Log("bow: " + total_count["bow"]);
        Debug.Log("sword: " + total_count["sword"]);
        Debug.Log("purple: " + total_count["purple"]);
        Debug.Log("yellow: " + total_count["yellow"]);
    }

    private void Awake()
    {
        // btn name in UI is "next level"
        btn_return.onClick.AddListener(() => {

            // TODO 这里的result数组，是{金币，弓，剑，紫药水，黄药水}传给下一个scene
            int[] result = new int[] {int.Parse(txt_coins.text), total_count["bow"], total_count["sword"], total_count["purple"], total_count["yellow"] };
            GameData.Instance.SetupStore(result);
            
            // data for Analytics
            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("coins", txt_coins.text);
            customParams.Add("row", total_count["bow"]);
            customParams.Add("sword", total_count["sword"]);
            customParams.Add("purple", total_count["purple"]);
            customParams.Add("yellow", total_count["yellow"]);
            AnalyticsEvent.LevelQuit(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().buildIndex, customParams);

            PrintDebugInfo();
            // load next round scene
            SceneManager.LoadScene(GameData.Instance.GetLevel()+1);

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

       

        btn_reset.onClick.AddListener(() => {
            int after_coin = int.Parse(txt_coins.text);

            after_coin = after_coin + int.Parse(txt_goods_1.text) * coins_per_goods_1;
            after_coin = after_coin + int.Parse(txt_goods_2.text) * coins_per_goods_2;
            after_coin = after_coin + int.Parse(txt_goods_3.text) * coins_per_goods_3;
            txt_goods_1.text = "0";
            txt_goods_2.text = "0";
            txt_goods_3.text = "0";
            txt_coins.text = after_coin.ToString();
         });

        // checkout btn
        btn_confirm.onClick.AddListener(() => {
            total_count[(string)selected_goods[0]["name"]] += int.Parse(txt_goods_1.text);
            total_count[(string)selected_goods[1]["name"]] += int.Parse(txt_goods_2.text);
            total_count[(string)selected_goods[2]["name"]] += int.Parse(txt_goods_3.text);
            txt_goods_1.text = "0";
            txt_goods_2.text = "0";
            txt_goods_3.text = "0";
            final_1.text = total_count[(string)selected_goods[0]["name"]].ToString();
            final_2.text = total_count[(string)selected_goods[1]["name"]].ToString();
            final_3.text = total_count[(string)selected_goods[2]["name"]].ToString();


        });

    }
}
