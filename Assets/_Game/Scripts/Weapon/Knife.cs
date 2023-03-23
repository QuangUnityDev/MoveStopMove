using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rangWeapon = WeaponAtributesFirst.rangeBoomerang + player.killed * 0.54f;
    }
}
