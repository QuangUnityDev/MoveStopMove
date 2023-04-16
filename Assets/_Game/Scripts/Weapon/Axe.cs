using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }
    public override void FixedUpdate()
    {
        if (ShootForce <= 0) return;
        Transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        Transform.localScale = player.transform.localScale;
        if (Vector3.Distance(posStart, new Vector3(Transform.position.x, posStart.y, Transform.position.z)) > rangWeapon)
        {
            Transform.gameObject.SetActive(false);          
            rb.velocity = Vector3.zero;
        }
    }
}
