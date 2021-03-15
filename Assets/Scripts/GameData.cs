using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public int level = 1;
    public int HP;
    public int BombCount;
    public int Money;
    public int BowCount;
    public int SwordCount;

    private void Awake()
    {
        Instance = this;
    }

    public void SetupLevel(int level)
    {
        this.level = level;
    }

    public void SetupStore(int[] storeList)
    {
        this.Money = storeList[0];
        this.BowCount = storeList[1];
        this.SwordCount = storeList[2];
        this.HP = storeList[3];
        this.BombCount = storeList[4];
    }

    public int[] GetStoreList()
    {
        return new int[] {Money, BowCount, SwordCount, HP, BombCount};
    }

    public int GetLevel()
    {
        return level;
    }

    private void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
