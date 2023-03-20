using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }
    public override void FixedUpdate()
    {
<<<<<<<< HEAD:Assets/OnGame/Scripts/Weapon/Axe.cs
        if (Vector3.Distance(transform.position, posStart) > rangWeapon)
========
        rangWeapon = WeaponAtributesFirst.rangeBullet + killPlayer * 0.2f;
        if (Vector3.Distance(transform.position, posStart) > rangWeapon + killPlayer * 0.2f)
>>>>>>>> 2269ca4b1d3238cae9bb3625c0d16147b67ca18e:Assets/OnGame/Scripts/Bullet.cs
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
