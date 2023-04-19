using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquiment : Singleton<ChangeEquiment>
{
    public void ResetAtributeWeapon(TypeWeaapon currentWeapon, Transform rangeCollider,Transform spriteRange, Charecter player)
    {
        switch (currentWeapon)
        {
            case TypeWeaapon.AXE:
                player.currentWeapon = TypeWeaapon.AXE;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBullet;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBullet / 2.5f, WeaponAtributesFirst.rangeBullet / 2.5f, WeaponAtributesFirst.rangeBullet / 2.5f);
                break;
            case TypeWeaapon.BOOMERANG:
                player.currentWeapon = TypeWeaapon.BOOMERANG;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBoomerang;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBoomerang / 2.5f, WeaponAtributesFirst.rangeBoomerang / 2.5f, WeaponAtributesFirst.rangeBoomerang / 2.5f);
                break;
            case TypeWeaapon.CANDYTREE:
                player.currentWeapon = TypeWeaapon.CANDYTREE;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.Candytree;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.Candytree / 2.5f, WeaponAtributesFirst.Candytree / 2.5f, WeaponAtributesFirst.Candytree / 2.5f);
                break;
            default:
                break;
        }
    }
}
