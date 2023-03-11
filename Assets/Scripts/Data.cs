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
    public int currentWeapon;
    public int[] weaponOwner;
    public int goal;
    public Data (PlayerController player)
    {
        currentWeapon = (int)player.typeWeaapon;
        goal = player.killed;
    }
}

