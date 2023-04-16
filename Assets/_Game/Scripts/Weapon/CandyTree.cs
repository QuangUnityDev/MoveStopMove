using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyTree : Weapon
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (ShootForce <= 0) return;
        Transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        rangWeapon = WeaponAtributesFirst.rangeBoomerang + player.killed * 0.54f;
        Transform.localScale = player.transform.localScale;
        if (Vector3.Distance(posStart, new Vector3(Transform.position.x, posStart.y, Transform.position.z)) > rangWeapon)
        {
            Transform.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
    }
}
