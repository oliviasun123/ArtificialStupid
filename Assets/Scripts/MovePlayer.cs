using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    public GameObject bombPrefab;
    private Transform m_camTransform;//摄像机Transform
    private Transform m_transform;//摄像机父物体Transform
    private int rightFlag = 1, leftFlag = 1, upFlag = 1, downFlag = 1;
    public float m_movSpeed_x = 10;//移动系数
    public float m_movSpeed_y = 10;//移动系数

    private int BombCount = 4;
    private int HP = 3;


    // Start is called before the first frame update
    void Start()
    {
        m_camTransform = Camera.main.transform;
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
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
                UIController.Instance.RefreshInfo(HP, BombCount);
                GameObject bomb = GameObject.Instantiate(bombPrefab);
                bomb.transform.position = transform.position;
                bomb.GetComponent<Bomb>().InitBomb(0, 1);
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
            Debug.Log("RIGHT!!");
        }
        if (coll.contacts[0].normal.x == 1)
        {
            leftFlag = 0;
            Debug.Log("LEFT!!");
        }
        if (coll.contacts[0].normal.y == -1)
        {
            upFlag = 0;
            Debug.Log("UP!!");
        }
        if (coll.contacts[0].normal.y == 1)
        {
            downFlag = 0;
            Debug.Log("DOWN!!");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Explode"))
        {   
            if(HP > 0) HP--;
            UIController.Instance.RefreshInfo(HP, BombCount);
            if(HP==0) Destroy(gameObject);
        }
        if (other.CompareTag("Door"))
        {
            SceneManager.LoadScene("Level1");
        }
    }


}