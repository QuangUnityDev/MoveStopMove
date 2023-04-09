using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    [Header("Weapon Owner")]
    public TypeWeaapon currentWeapon = 0 ;
    public List<int> weaponOwner = new List<int> { 0 };

    [Header("Skin Owner")]
    public List<int> skinOwner = new List<int>(0);
    public List<int> shortsOwner = new List<int> { 0};
    public List<int> hornorsOwner = new List<int> { 0 };
    public List<int> armOwner = new List<int> { 0 };

    public int currentSkin;
    public int currentSkinArm;
    public int currentSkinHornor;
    public int currentSkinShorts;

    [Header("Skin Weapon Owner")]
    public List<int> skinAxeOwer = new List<int> { 0 };
    public List<int> skinBoomerangOwer = new List<int> { 0 };
    public List<int> skinCandyTreeOwer = new List<int> { 0 };
    public int currentUsingSkinWeapon;

    [Header("Level and gold current")]
    public int gold ;
    public int levelID = 1;
    public Data(GameManager gameData)
    {
        //DATA PLAYER
       
        gold = gameData.dataPlayer.gold;
        levelID = gameData.dataPlayer.levelID;

        weaponOwner = gameData.dataPlayer.weaponOwner;
        currentWeapon = gameData.dataPlayer.currentWeapon;
        currentUsingSkinWeapon = gameData.dataPlayer.currentUsingSkinWeapon;

        skinOwner = gameData.dataPlayer.skinOwner;
        shortsOwner  = gameData.dataPlayer.shortsOwner;
        armOwner = gameData.dataPlayer.armOwner;
        hornorsOwner = gameData.dataPlayer.hornorsOwner;

        currentSkin = gameData.dataPlayer.currentSkin;
        currentSkinArm = gameData.dataPlayer.currentSkinArm;
        currentSkinHornor = gameData.dataPlayer.currentSkinHornor;
        currentSkinShorts = gameData.dataPlayer.currentSkinShorts;

        skinAxeOwer = gameData.dataPlayer.skinOwner;
        skinBoomerangOwer = gameData.dataPlayer.skinBoomerangOwer;
        skinCandyTreeOwer = gameData.dataPlayer.skinCandyTreeOwer;
    }

}

