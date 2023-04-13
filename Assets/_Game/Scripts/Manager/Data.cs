using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    [Header("Weapon Owner")]
    public TypeWeaapon currentWeapon;
    public List<int> weaponOwner;

    [Header("Skin Owner")]
    public List<int> skinOwner;
    public List<int> shortsOwner;
    public List<int> hornorsOwner;
    public List<int> armOwner;

    public int currentSkin;
    public int currentSkinArm;
    public int currentSkinHornor;
    public int currentSkinShorts;

    [Header("Skin Weapon Owner")]
    public List<int> skinAxeOwer;
    public List<int> skinBoomerangOwer;
    public List<int> skinCandyTreeOwer;
    public int currentUsingSkinWeapon;

    [Header("Level and gold current")]
    public int gold ;
    public int levelID;
    public bool isFirst;
    public Data(GameManager gameData)
    {
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
        isFirst = gameData.dataPlayer.isFirst;
    }

}

