using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private float rayDistanceX, rayDistanceY;
    private float maxSpeed = 0.05f;
    private Vector2 movement;
    // 0: top-left, 1: top-right, 2: down-left, 3: down-right
    private List<int> currentDir;
    private float timeLeft;
    private bool isDead = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        NormalizeMovement();

        Vector3 length = GetComponent<Collider2D>().bounds.size;
        rayDistanceX = length.x / 2 + 0.2f;
        rayDistanceY = length.y / 2 + 0.2f;
    }

    void Update()
    {
        rb.MovePosition((Vector2)transform.position + movement * maxSpeed);
        List<bool> newDir = FindNewDirectionByRay();
        currentDir = GetCurrentDirection();

        if (currentDir[0] != -1 && (newDir[currentDir[0]] == false || newDir[currentDir[1]] == false))
        {
            GenerateMovement(newDir);
        }

        else if (currentDir[0] == -1 && newDir[currentDir[1]] == false)
        {
            GenerateMovement(newDir);
        }

    }

    // [left, right, up, down] = [0, 1, 2, 3]
    private void GenerateMovement(List<bool> newDir)
    {
        Vector2 horizontal = new Vector2(-1f, 1f);
        Vector2 vertical = new Vector2(-1f, 1f);

        if (newDir[0] == true && newDir[1] == false)
        {
            horizontal.y = 0;
        }
        else if (newDir[0] == false && newDir[1] == true)
        {
            horizontal.x = 0;
        }
        else if (newDir[0] == false && newDir[1] == false)
        {
            horizontal.x = horizontal.y = 0;
        }


        if (newDir[2] == true && newDir[3] == false)
        {
            vertical.x = 0;
        }
        else if (newDir[2] == false && newDir[3] == true)
        {
            vertical.y = 0;
        }
        else if (newDir[2] == false && newDir[3] == false)
        {
            vertical.x = vertical.y = 0;
        }

        movement = new Vector2(Random.Range(horizontal.x, horizontal.y), Random.Range(vertical.x, vertical.y));
        NormalizeMovement();
    }

    private void NormalizeMovement()
    {
        if (movement.x < 0 && movement.x > -0.2f) movement.x = -0.2f;
        if (movement.y < 0 && movement.y > -0.2f) movement.y = -0.2f;
        if (movement.x > 0 && movement.x < 0.2f) movement.x = 0.2f;
        if (movement.y > 0 && movement.y < 0.2f) movement.y = 0.2f;
    }


    // [left, right, up, down] = [0, 1, 2, 3]
    // TODO: Error handling: (m.x || m.y) == 0
    private List<int> GetCurrentDirection()
    {
        if (movement.x < 0 && movement.y > 0) return new List<int>() { 0, 2 };
        else if (movement.x > 0 && movement.y > 0) return new List<int>() { 1, 2 };
        else if (movement.x < 0 && movement.y < 0) return new List<int>() { 0, 3 };
        else if (movement.x > 0 && movement.y < 0) return new List<int>() { 1, 3 };
        
        else if (movement.x == 0 && movement.y < 0)
        {
            print("edge case 1");
            return new List<int>() { -1, 3 };
        }
        else if (movement.x == 0 && movement.y > 0)
        {
            print("edge case 2");
            return new List<int>() { -1, 2 };
        }
        else if (movement.x > 0 && movement.y == 0)
        {
            print("edge case 3");
            return new List<int>() { -1, 1 };
        }
        else if (movement.x < 0 && movement.y == 0)
        {
            print("edge case 4");
            return new List<int>() { -1, 0 };
        }

        return new List<int>() { -1, -1 };
    }

    private void DestroyAndGetKey()
    {
        // fix multiple keys bug
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (gameObject.layer == 10)
        {
            GetKeyHelper.Instance.GenerateKey(transform.position);
        }

        Destroy(gameObject);
    }

    public bool DestroyConditionTrigger(Collider2D other)
    {
        if (other.CompareTag("Killer"))
        {
            return true;
        }

        if (other.CompareTag("GreenBomb") && gameObject.tag == "GreenEnemy")
        {
            return true;
        }

        if (other.CompareTag("BlueBomb") && gameObject.tag == "BlueEnemy")
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DestroyConditionTrigger(other))
        {
            DestroyAndGetKey();
        }

        // else if (other.CompareTag("Wall"))
        // {
        //     GetNewDirections();
        // }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (DestroyConditionTrigger(other))
        {
            DestroyAndGetKey();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (DestroyConditionTrigger(other))
        {
            DestroyAndGetKey();
        }
    }

    // return a list of possible directions [left, right, up, down]
    private List<bool> FindNewDirectionByRay()
    {
        bool left = false, right = false, up = false, down = false;

        if (Physics2D.Raycast(transform.position, Vector2.up, rayDistanceY, 1 << 8) == false)
        {
            up = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, rayDistanceY, 1 << 8) == false)
        {
            down = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, rayDistanceX, 1 << 8) == false)
        {
            left = true;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, rayDistanceX, 1 << 8) == false)
        {
            right = true;
        }

        return new List<bool>() { left, right, up, down };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 1 * rayDistanceY, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -1 * rayDistanceY, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(1 * rayDistanceX, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-1 * rayDistanceX, 0, 0));
    }

    // private void GetNewDirections()
    // {
    //     //print("1" + movement);
    //     movement = new Vector2(GetNewXorY(movement.x), GetNewXorY(movement.y));
    //     //print("2" + movement);
    //     rb.MovePosition((Vector2)transform.position + movement * maxSpeed);
    // }

    // private float GetNewXorY(float z)
    // {
    //     z = z * Random.Range(-2f, -1f);
    //     if (z > 1) z = z - 1;
    //     if (z < -1) z = z + 1;
    //     if (z < 0.1 && z > 0) z = 0.1f;
    //     if (z > -0.1 && z < 0) z = -0.1f;
    //     return z;
    // }
}
