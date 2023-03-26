using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    //public int health;
    //public int money;
    //public string playerName;
    public int currentWeapon ;
    public int[] weaponOwner ;
    public int gold ;
    public int levelID;
    public Data(GameManager gameData)
    {
        //DATA PLAYER
        currentWeapon = gameData.currentWeapon;
        gold = gameData.gold;
        levelID = gameData.levelCurrent;
    }
}

