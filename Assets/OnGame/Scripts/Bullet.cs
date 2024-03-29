using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
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
        rangWeapon = WeaponAtributesFirst.rangeBullet + killPlayer * 0.2f;
        if (Vector3.Distance(transform.position, posStart) > rangWeapon + killPlayer * 0.2f)
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
