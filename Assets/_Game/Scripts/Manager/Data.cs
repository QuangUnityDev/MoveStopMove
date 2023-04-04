using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public int currentWeapon = 0 ;
    public int[] weaponOwner = {0} ;

    public int[] skinOwner = { 0 };
    public int[] shortsOwner = { 0 };
    public int[] hornorsOwner = { 0 };
    public int[] armOwner = { 0 };

    public int currentSkin;
    public int currentSkinArm;
    public int currentSkinHornor;
    public int currentSkinShorts;

    public int[] skinAxeOwer = { 0 };
    public int[] skinBoomerangOwer = { 0 };
    public int[] skinCandyTreeOwer = { 0 };
    public int currentUsingSkinWeapon;

    public int gold ;
    public int levelID;
    public Data(GameManager gameData)
    {
        //DATA PLAYER
        currentWeapon = gameData.dataPlayer.currentWeapon;
        gold = gameData.dataPlayer.gold;
        levelID = gameData.dataPlayer.levelID;
        weaponOwner = gameData.dataPlayer.weaponOwner;
        currentUsingSkinWeapon = gameData.dataPlayer.currentUsingSkinWeapon;
    }
}

