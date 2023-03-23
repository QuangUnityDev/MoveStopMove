using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    protected Transform backPos;
    public override void OnEnable()
    {
        base.OnEnable();
        Transform.rotation = Quaternion.Euler(90f, 0f, 0f);   
    }

    public override void FixedUpdate()
    {      
        Transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
        SetTarGet(() =>
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((backPos.position - Transform.position).normalized * 10, ForceMode.VelocityChange);
        }
       );
        if (Vector3.Distance(posStart, new Vector3(Transform.position.x, posStart.y, Transform.position.z)) > rangWeapon)
        {
            isBack = true;
            backPos = player.transform;
        }
        if (!backPos) return;
        if (Vector3.Distance(Transform.position, new Vector3(backPos.transform.position.x, Transform.position.y, backPos.position.z)) < 0.1f && isBack == true)
        {
            Transform.gameObject.SetActive(false);
        }
        if (isBack == true) finishCallBack?.Invoke();
        
    }
}
