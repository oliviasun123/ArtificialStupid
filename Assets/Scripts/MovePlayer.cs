using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.UI;

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
        isGreen = false;

        GameObject.DontDestroyOnLoad(gameObject);
        // InitExit();

        int[] basics = UIController.Instance.GetBasicInfo();
        HP = basics[0];
        BombCount = basics[1];

        m_camTransform = Camera.main.transform;
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Store");
        }

        // 定义3个值控制移动
        float xm = 0, ym = 0;

        //按键盘W向上移动
        if (Input.GetKey(KeyCode.W) && upFlag == 1 && !Input.GetKey(KeyCode.S))
        {
            ym += m_movSpeed_y * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && downFlag == 1 && !Input.GetKey(KeyCode.W))//按键盘S向下移动
        {
            ym -= m_movSpeed_y * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) && leftFlag == 1 && !Input.GetKey(KeyCode.D))//按键盘A向左移动
        {
            xm -= m_movSpeed_x * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && rightFlag == 1 && !Input.GetKey(KeyCode.A))//按键盘D向右移动
        {
            xm += m_movSpeed_x * Time.deltaTime;
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

    // 碰撞开始
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.contacts[0].normal.x == -1)
        {
            rightFlag = 0;
            // Debug.Log("RIGHT!!");
        }
        if (coll.contacts[0].normal.x == 1)
        {
            leftFlag = 0;
            // Debug.Log("LEFT!!");
        }
        if (coll.contacts[0].normal.y == -1)
        {
            upFlag = 0;
            // Debug.Log("UP!!");
        }
        if (coll.contacts[0].normal.y == 1)
        {
            downFlag = 0;
            // Debug.Log("DOWN!!");
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        rightFlag = 1;
        leftFlag = 1;
        upFlag = 1;
        downFlag = 1;
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.contacts[0].normal.x == -1)
        {
            rightFlag = 0;
        }
        if (coll.contacts[0].normal.x == 1)
        {
            leftFlag = 0;
        }
        if (coll.contacts[0].normal.y == -1)
        {
            upFlag = 0;
        }
        if (coll.contacts[0].normal.y == 1)
        {
            downFlag = 0;
        }
    }

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
            HP++;
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("BombPotion"))
        {
            BombCount++;
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("Key"))
        {
            UIController.Instance.DisplayKey();
            hasKey = true;
        }

        if (hasKey && other.CompareTag("Box"))
        {
            hasKey = false;
        }

        if (other.CompareTag("Gem"))
        {
            GemCount++;
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
        }

        if (other.CompareTag("Bow"))
        {
            explodeRange = explodeRange + 2.0f;
        }

        if (other.CompareTag("Money"))
        {
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
            UIController.Instance.RefreshInfo(HP, BombCount, SwordCount, MoneyCount, GemCount);
            gameObject.tag = "Killer";
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
            SceneManager.LoadScene("Start");
        }
    }

    IEnumerator HideLevelAnimation()
    {
        yield return new WaitForSeconds(2);
        UIController.Instance.HideLevelScene();
    }

    void OnLevelWasLoaded(int level)
    {
        thisScene = SceneManager.GetActiveScene();

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
            print("level2");
            gameObject.transform.position = new Vector3(-2, 16, 0);
        }

        if (level == 3)
        {
            gameObject.transform.position = new Vector3(-4, 15, 0);
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