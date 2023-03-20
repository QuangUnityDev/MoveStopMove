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
        if (Vector3.Distance(transform.position, posStart) > rangWeapon)
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
