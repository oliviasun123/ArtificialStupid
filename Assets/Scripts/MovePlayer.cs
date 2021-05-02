using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System;

public class MovePlayer : MonoBehaviour
{
    public GameObject bombPrefab;
    private Transform m_camTransform;//摄像机Transform
    private Transform m_transform;//摄像机父物体Transform
    private int rightFlag = 1, leftFlag = 1, upFlag = 1, downFlag = 1;
    public float m_movSpeed_x = 10;//移动系数
    public float m_movSpeed_y = 10;//移动系数
    private Scene thisScene;

    private int BombCount;
    private int HP;
    private int SwordCount = 0;
    private int MoneyCount = 0;
    private int GemCount = 0;
    private float explodeRange = 2.0f;
    private float SafeTime = 2.0f;
    private bool SafeFlag = false;
    private bool isGreen;
    private bool hasKey = false;

    private GameObject player;

    // private AudioSource touchMusic;


    // private void InitExit()
    // {   
    //     GameObject.DontDestroyOnLoad(btn_exit);

    //     btn_exit.onClick.AddListener(() =>
    //     {
    //         Destroy(gameObject);
    //     });
    // }

    // Start is called before the first frame update
    void Start()
    {
        print("Player Start func");

        isGreen = false;
        thisScene = SceneManager.GetActiveScene();

        if (thisScene.name != "sample")
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }
        // InitExit();

        int[] basics = UIController.Instance.GetBasicInfo();
        HP = basics[0];
        BombCount = basics[1];

        m_camTransform = Camera.main.transform;
        m_transform = GetComponent<Transform>();
        // touchMusic = GetComponent<AudioSource>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (SwordCount > 0)
        {
            gameObject.tag = "Killer";
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Store");
        }

        // 定义3个值控制移动
        float xm = 0, ym = 0;

        //按键盘W向上移动
        if ((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)))
        {
            ym += m_movSpeed_y * Time.deltaTime;
            if (player.GetComponent<SpriteRenderer>().sprite.name == "up_0")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("up_1") as Sprite;
            }
            else if (player.GetComponent<SpriteRenderer>().sprite.name == "up_1")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("up_2") as Sprite;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("up_0") as Sprite;
            }
        }
        else if ((Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
        {
            ym -= m_movSpeed_y * Time.deltaTime;
            if (player.GetComponent<SpriteRenderer>().sprite.name == "down_0")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("down_1") as Sprite;
            }
            else if (player.GetComponent<SpriteRenderer>().sprite.name == "down_1")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("down_2") as Sprite;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("down_0") as Sprite;
            }
        }

        if ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            xm -= m_movSpeed_x * Time.deltaTime;
            if (player.GetComponent<SpriteRenderer>().sprite.name == "left_0")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("left_1") as Sprite;
            }
            else if (player.GetComponent<SpriteRenderer>().sprite.name == "left_1")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("left_2") as Sprite;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("left_0") as Sprite;
            }
        }
        else if ((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            xm += m_movSpeed_x * Time.deltaTime;
            if (player.GetComponent<SpriteRenderer>().sprite.name == "right_0")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("right_1") as Sprite;
            }
            else if (player.GetComponent<SpriteRenderer>().sprite.name == "right_1")
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("right_2") as Sprite;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("right_0") as Sprite;
            }
        }
        m_transform.Translate(new Vector2(xm, ym), Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log(BombNum.num);
            // if (BombNum.num == 0) {
            //     return;
            // }
            if (BombCount > 0)
            {
                BombCount--;
                UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
                GameObject bomb = GameObject.Instantiate(bombPrefab);

                bomb.transform.position = transform.position;
                bomb.GetComponent<Bomb>().InitBomb(explodeRange, isGreen);

                if (isGreen)
                {
                    UIController.Instance.ChangeBlue();
                }
                else
                {
                    UIController.Instance.ChangeGreen();
                }

                isGreen = isGreen == true ? false : true;
            }

            // InitBomb(0, 1, cube);
        }
    }

    // // 碰撞开始
    // void OnCollisionEnter2D(Collision2D coll)
    // {
    //     if (coll.contacts[0].normal.x == -1)
    //     {
    //         rightFlag = 0;
    //         // Debug.Log("RIGHT!!");
    //     }
    //     if (coll.contacts[0].normal.x == 1)
    //     {
    //         leftFlag = 0;
    //         // Debug.Log("LEFT!!");
    //     }
    //     if (coll.contacts[0].normal.y == -1)
    //     {
    //         upFlag = 0;
    //         // Debug.Log("UP!!");
    //     }
    //     if (coll.contacts[0].normal.y == 1)
    //     {
    //         downFlag = 0;
    //         // Debug.Log("DOWN!!");
    //     }
    // }

    // void OnCollisionExit2D(Collision2D coll)
    // {
    //     rightFlag = 1;
    //     leftFlag = 1;
    //     upFlag = 1;
    //     downFlag = 1;
    // }

    // void OnCollisionStay2D(Collision2D coll)
    // {
    //     if (coll.contacts[0].normal.x == -1)
    //     {
    //         rightFlag = 0;
    //     }
    //     if (coll.contacts[0].normal.x == 1)
    //     {
    //         leftFlag = 0;
    //     }
    //     if (coll.contacts[0].normal.y == -1)
    //     {
    //         upFlag = 0;
    //     }
    //     if (coll.contacts[0].normal.y == 1)
    //     {
    //         downFlag = 0;
    //     }
    // }

    private bool isBomb(Collider2D other)
    {
        return other.CompareTag("BlueBomb") || other.CompareTag("GreenBomb");
    }

    private bool isEnemy(Collider2D other)
    {
        return other.CompareTag("Enemy") || other.CompareTag("BlueEnemy") || other.CompareTag("GreenEnemy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPotion"))
        {   
            AudioManager.instance.PlayPotionGet();
            HP++;
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("BombPotion"))
        {
            AudioManager.instance.PlayPotionGet();
            BombCount++;
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("Key"))
        {
            AudioManager.instance.PlayCoinGet();
            UIController.Instance.DisplayKey();
            hasKey = true;
        }

        if (hasKey && other.CompareTag("Box"))
        {
            AudioManager.instance.PlayOpenBox();
            hasKey = false;
        }

        if (other.CompareTag("Gem"))
        {
            GemCount++;
            AudioManager.instance.PlayCoinGet();
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("Bow"))
        {
            AudioManager.instance.PlayBowGet();
            explodeRange = explodeRange + 2.0f;
        }

        if (other.CompareTag("Money"))
        {   
            AudioManager.instance.PlayCoinGet();
            MoneyCount += 2;    
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (isEnemy(other) || isBomb(other))
        {
            // safe time
            if (SwordCount == 0 && SafeFlag)
            {
                return;
            }

            if (HP > 0)
            {
                if (isEnemy(other) && SwordCount > 0)
                {
                    SwordCount = SwordCount - 1;
                    StartCoroutine(BackToPlayer());
                }
                else
                {
                    HP--;
                    AudioManager.instance.PlayTouch();
                    SafeFlag = true;
                    StartCoroutine(GraceTime(SafeTime));
                }
            }

            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);

            if (HP == 0)
            {
                UIController.Instance.ShowGameOver();
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Sword"))
        {
            SwordCount++;
            AudioManager.instance.PlaySwordGet();
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
            // gameObject.tag = "Killer";
        }

        if (other.CompareTag("Door_sample"))
        {
            SceneManager.LoadScene("Start");
        }

        if (other.CompareTag("Door_level1"))
        {
            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("bombs_remain", UIController.Instance.GetBombCount());
            customParams.Add("life_remain", UIController.Instance.GetHP());

            hasKey = UIController.Instance.KeyAvailable();

            AnalyticsEvent.LevelComplete(thisScene.name, thisScene.buildIndex, customParams);
            SceneManager.LoadScene("Store");
        }

        if (other.CompareTag("Door_level2"))
        {
            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("bombs_remain", UIController.Instance.GetBombCount());
            customParams.Add("life_remain", UIController.Instance.GetHP());

            hasKey = UIController.Instance.KeyAvailable();

            AnalyticsEvent.LevelComplete(thisScene.name, thisScene.buildIndex, customParams);
            SceneManager.LoadScene("Store");
        }

        if (other.CompareTag("Door_level3"))
        {
            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("bombs_remain", UIController.Instance.GetBombCount());
            customParams.Add("life_remain", UIController.Instance.GetHP());

            hasKey = UIController.Instance.KeyAvailable();

            AnalyticsEvent.LevelComplete(thisScene.name, thisScene.buildIndex, customParams);
            SceneManager.LoadScene("Congratulation");
        }
        // touchMusic.Play();
    }

    IEnumerator HideLevelAnimation()
    {
        yield return new WaitForSeconds(2);
        UIController.Instance.HideLevelScene();
    }

    // private int FixUpLevelNum(int level)
    // {
    //     int level_res = 0;
    //     // accomplish random level cases
    //     if (level == 7 || level == 8) 
    //     {
    //         level_res = 1;
    //     }
    //     else if (level == 9 || level == 10)
    //     {
    //         level_res = 2;
    //     }
    //     else 
    //     {
    //         return level;
    //     }

    //     return level_res;
    // }

    void OnLevelWasLoaded(int level)
    {
        // int level_res = FixUpLevelNum(level);

        print("OnLevelWasLoaded " + level);

        thisScene = SceneManager.GetActiveScene();

        if (level == 6)
        {
            // tutorial level 
            return;
        }

        if (level != 5)
        {
            GameData.Instance.SetupLevel(level);
            StartCoroutine(HideLevelAnimation());
        }

        if (level == 1)
        {
            return;
        }

        if (level != 5 && isGreen)
        {
            UIController.Instance.ChangeGreen();
        }
        else if (level != 5 && !isGreen)
        {
            UIController.Instance.ChangeBlue();
        }


        if (level == 5)
        {
            int[] list = new int[] { MoneyCount, (int)explodeRange / 2 - 1, SwordCount, HP, BombCount };
            GameData.Instance.SetupStore(list);
            return;
        }

        HP = GameData.Instance.HP;
        MoneyCount = GameData.Instance.Money;
        explodeRange = (GameData.Instance.BowCount + 1) * 2.0f;
        SwordCount = GameData.Instance.SwordCount;
        BombCount = GameData.Instance.BombCount;

        HP += 1;
        BombCount += 3;
        UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);

        // print(level);
        if (level == 2)
        {
            gameObject.transform.position = new Vector3(-2, 16, 0);
        }

        else if (level == 3)
        {   
            System.Random rand = new System.Random();
            List<Vector3> FINAL_LOCATION = new List<Vector3>(){new Vector3(-15, 9, 0), new Vector3(-11, -2, 0), new Vector3(23, -3, 0), new Vector3(50, 14, 0), new Vector3(33, 8, 0)};
            int randIdx = rand.Next(0, 5);
            gameObject.transform.position = FINAL_LOCATION[randIdx];
        }

        else if (level == 7)
        {
            gameObject.transform.position = new Vector3(-5, 19, 0);
        }

        else if (level == 8) 
        {
            gameObject.transform.position = new Vector3(-2, 15, 0);
        }

        else if (level == 9) 
        {
            gameObject.transform.position = new Vector3(-2, 18, 0);
        }

        else if (level == 10) 
        {
            gameObject.transform.position = new Vector3(-2, 17, 0);
        }

        if (!hasKey)
        {
            UIController.Instance.HideKey();
        }

        UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isBomb(other))
        {
            // safe time
            if (SafeFlag)
            {
                return;
            }

            if (HP > 0)
            {
                HP--;
                SafeFlag = true;
                StartCoroutine(GraceTime(SafeTime));
            }

            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);

            if (HP == 0)
            {
                UIController.Instance.ShowGameOver();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator GraceTime(float graceTime)
    {
        yield return new WaitForSeconds(graceTime);
        SafeFlag = false;
    }

    IEnumerator BackToPlayer()
    {
        yield return new WaitForEndOfFrame();

        if (SwordCount == 0)
        {
            gameObject.tag = "Player";
        }
    }



}