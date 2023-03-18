using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquiment : Singleton<ChangeEquiment>
{
    public int currentWeapon;
    public void ChangeWeapon(Charecter.TypeWeaapon typeWeaapon, Transform rangeCollider,Transform spriteRange)
    {
        switch (ChangeEquiment.GetInstance().currentWeapon)
        {
            case 0:
                typeWeaapon = Charecter.TypeWeaapon.BULLET;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBullet;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f);
                break;
            case 1:
                typeWeaapon = Charecter.TypeWeaapon.SWORD;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBullet;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f);
                break;
            case 2:
                typeWeaapon = Charecter.TypeWeaapon.BOOMERANG;
                rangeCollider.GetComponent<SphereCollider>().radius = WeaponAtributesFirst.rangeBullet;
                spriteRange.localScale = new Vector3(WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f, WeaponAtributesFirst.rangeBullet / 2.56f);
                break;
            default:
                break;
        }
    }
}
