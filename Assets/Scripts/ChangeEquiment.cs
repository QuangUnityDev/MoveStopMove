using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquiment : Singleton<ChangeEquiment>
{
    public void ChangeWeapon(int currentWeapon, Transform rangeCollider,Transform spriteRange,Charecter.TypeWeaapon typeWeaapon)
    {
        switch (currentWeapon)
        {
            case 0:
                typeWeaapon = Charecter.TypeWeaapon.BULLET;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBullet;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBullet / 2.5f, WeaponAtributesFirst.rangeBullet / 2.5f, WeaponAtributesFirst.rangeBullet / 2.5f);
                break;
            case 1:
                typeWeaapon = Charecter.TypeWeaapon.SWORD;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeSword;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeSword / 2.5f, WeaponAtributesFirst.rangeSword / 2.5f, WeaponAtributesFirst.rangeSword / 2.5f);
                break;
            case 2:
                typeWeaapon = Charecter.TypeWeaapon.BOOMERANG;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBoomerang;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBoomerang / 2.5f, WeaponAtributesFirst.rangeBoomerang / 2.5f, WeaponAtributesFirst.rangeBoomerang / 2.5f);
                break;
            default:
                break;
        }
    }
}
