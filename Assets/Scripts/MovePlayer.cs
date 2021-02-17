using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Transform m_camTransform;//摄像机Transform
    private Transform m_transform;//摄像机父物体Transform
    private int rightFlag = 1, leftFlag = 1, upFlag = 1, downFlag = 1;
    public float m_movSpeed_x = 10;//移动系数
    public float m_movSpeed_y = 10;//移动系数
    public GameObject Cube;

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
            var cube = GameObject.Instantiate(Cube);
            // cube.transform.Translate(transform.position);
            // cube.transform.Translate(new Vector2(xm,ym),Space.Self);
            cube.transform.position = transform.position;
            InitBomb(0, 1, cube);
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
        }
        if (coll.contacts[0].normal.y == 1)
        {
            downFlag = 0;
        }
        Debug.Log("rightFlag=" + rightFlag);
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

    public void InitBomb(int range, float delayTime, GameObject bomb)
    {
        // this.explodeRange = range;
        StartCoroutine("explode", bomb);
    }

    IEnumerator explode(GameObject bomb)
    {
        yield return new WaitForSeconds(2);
        // TODO: need quaternion ?
        // Instantiate(BombEffectPrefab, transform.position, Quaternion.identity);
        // generate based on range
        bomb.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * 2.0f;
        bomb.tag = "Explode";
        yield return new WaitForSeconds(2);
        Destroy(bomb);
    }
}