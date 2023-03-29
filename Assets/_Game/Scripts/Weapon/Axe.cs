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
        if (ShootForce <= 0) return;
        if (Vector3.Distance(posStart, new Vector3(Transform.position.x, posStart.y, Transform.position.z)) > rangWeapon)
        {
            Transform.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
    }
}
