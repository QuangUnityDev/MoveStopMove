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
<<<<<<<< HEAD:Assets/OnGame/Scripts/Weapon/Boomerang.cs
        backPos = player;
========
>>>>>>>> 2269ca4b1d3238cae9bb3625c0d16147b67ca18e:Assets/OnGame/Scripts/Boomerang.cs
        transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        SetTarGet(() =>
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((backPos.position - transform.position).normalized * 10, ForceMode.VelocityChange);
        }
       );
        if (Vector3.Distance(transform.position, posStart) > rangWeapon + killPlayer * 0.2f && isBack == false)
        {
            isBack = true;
            finishCallBack?.Invoke();          
            Debug.LogError("Dan quay lai");
        }
<<<<<<<< HEAD:Assets/OnGame/Scripts/Weapon/Boomerang.cs
        if(Vector3.Distance(transform.position, new Vector3(backPos.transform.position.x, transform.position.y, backPos.position.z)) < 0.1f && isBack == true)
========
        if(Vector3.Distance(transform.position, posStart) < 0.1f && isBack == true)
>>>>>>>> 2269ca4b1d3238cae9bb3625c0d16147b67ca18e:Assets/OnGame/Scripts/Boomerang.cs
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
