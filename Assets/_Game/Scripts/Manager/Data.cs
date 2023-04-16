using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    [Header("Weapon Owner")] 
    public List<int> weaponOwner;
    public TypeWeaapon equipedWeapon;

    [Header("Skin Owner")]
    public List<int> skinOwner;
    public List<int> shortsOwner;
    public List<int> hornorsOwner;
    public List<int> armOwner;

    public int currentSkin;
    public int currentItemSkin;

    [Header("Skin Weapon Owner")]
    public List<int> skinAxeOwer;
    public List<int> skinBoomerangOwer;
    public List<int> skinCandyTreeOwer;

    public int equipedSkinWeapon;

    [Header("Level and gold current")]
    public int gold ;
    public int levelID;
    public bool isFirst;
    public Data(GameManager gameData)
    {
        gold = gameData.dataPlayer.gold;
        levelID = gameData.dataPlayer.levelID;

        weaponOwner = gameData.dataPlayer.weaponOwner;
        equipedWeapon = gameData.dataPlayer.equipedWeapon;
        equipedSkinWeapon = gameData.dataPlayer.equipedSkinWeapon;


        skinOwner = gameData.dataPlayer.skinOwner;
        shortsOwner  = gameData.dataPlayer.shortsOwner;
        armOwner = gameData.dataPlayer.armOwner;
        hornorsOwner = gameData.dataPlayer.hornorsOwner;

        currentSkin = gameData.dataPlayer.currentSkin;
        currentItemSkin = gameData.dataPlayer.currentItemSkin;

        skinAxeOwer = gameData.dataPlayer.skinAxeOwer;
        skinBoomerangOwer = gameData.dataPlayer.skinBoomerangOwer;
        skinCandyTreeOwer = gameData.dataPlayer.skinCandyTreeOwer;
        isFirst = gameData.dataPlayer.isFirst;
    }

}

