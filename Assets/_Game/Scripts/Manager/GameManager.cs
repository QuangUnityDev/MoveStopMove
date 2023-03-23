using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<Charecter> listTarget;
    public static int id = 0;
    [SerializeField] private Data data;
    public int levelCurrent;
    public int gold;
    public int currentWeapon;

    private void Awake()
    {
        levelCurrent = 1;
        id = 0;
        OnInit();
    }
    void OnInit()
    {
    }
    public void SaveData()
    {
        data.currentWeapon = currentWeapon ;
        data.gold = gold;
        data.levelID = levelCurrent;
        SaveLoadData.GetInstance().SaveToFile();
    }
    public void GetData()
    {
        SaveLoadData.GetInstance().LoadFromFile();
        currentWeapon = data.currentWeapon;
        gold  = data.gold;
        levelCurrent = data.levelID ;
    }
    private void Start()
    {
       
    }  
}
