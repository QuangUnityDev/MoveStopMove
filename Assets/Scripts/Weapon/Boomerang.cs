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
        backPos = player;
        transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        SetTarGet(() =>
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((backPos.position - transform.position).normalized * 10, ForceMode.VelocityChange);
        }
       );
        if (Vector3.Distance(posStart, new Vector3(transform.position.x,posStart.y,transform.position.z)) > rangWeapon && isBack == false)
        {
            isBack = true;
            finishCallBack?.Invoke();
        }
        if(Vector3.Distance(transform.position, new Vector3(backPos.transform.position.x, transform.position.y, backPos.position.z)) < 0.1f && isBack == true)
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
