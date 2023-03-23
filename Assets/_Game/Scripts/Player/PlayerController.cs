using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public FloatingJoystick variableJoystick;

    protected override void Start()
    {
        currentWeapon = 2;
        base.Start();
        OnInit();
        ChangeEquiment.GetInstance().ChangeWeapon(currentWeapon, colliderRange , spriteRange, typeWeaapon);
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public bool IsStop()
    {
        return Mathf.Abs(variableJoystick.Vertical) < 0.01f || Mathf.Abs(variableJoystick.Horizontal) < 0.01f;
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if(!isAttacking) rb.velocity = direction * speed * Time.fixedDeltaTime;
        if (rb.velocity != Vector3.zero) Transform.rotation = Quaternion.LookRotation(direction);

        if (!IsStop())
        {
            if (!isAttacking)
            {
                ChangeAnim("Run");
                CancelAttack();
            }       
        }
        else
        {
            if (!IsHadObject() && !isAttacking || !isTimeAttackNext) 
            {
            CancelAttack();
            ChangeAnim("Idle"); 
            }
            else
            {
                if (!isTimeAttackNext) return;
                if (!isPrepareAttacking && !isAttacking)
                {
                    CountDownAttack();
                }
                if (isPrepareAttacking)
                {
                    LookTarGet(targetAttack);
                    timeToAttack += Time.fixedDeltaTime;
                    if (timeToAttack > 0.5f && !isAttacking)
                    {                       
                        Attack();
                    }
                }              
            }
        }
    } 
}
