using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{  
    public override void OnEnable()
    {        
        base.OnEnable();
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    public override void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        SetTarGet(() =>
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((posStart - transform.position).normalized * 10, ForceMode.VelocityChange);
        }
       );
        if (Vector3.Distance(transform.position, posStart) > rangWeapon + killPlayer * 0.2f && isBack == false)
        {
            isBack = true;
            finishCallBack?.Invoke();          
            Debug.LogError("Dan quay lai");
        }
        if(Vector3.Distance(transform.position, posStart) < 0.1f && isBack == true)
        {
            gameObject.SetActive(false);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
}
