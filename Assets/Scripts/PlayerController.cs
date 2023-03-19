using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public VariableJoystick variableJoystick;

    protected override void Start()
    {
        currentWeapon = 2;
        base.Start();
        OnInit();       
    }
    public override void OnInit()
    {
        base.OnInit();       
    }
    public bool isStop()
    {
        return Mathf.Abs(variableJoystick.Vertical) < 0.01f || Mathf.Abs(variableJoystick.Horizontal) < 0.01f;
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if(!isAttacking) rb.velocity = direction * speed * Time.fixedDeltaTime;
        if (rb.velocity != Vector3.zero) transform.rotation = Quaternion.LookRotation(direction);

        if (!isStop() && !isAttacking)
        {
            ChangeAnim("Run");
            CancelAttack();
        }
        else
        {
            if (isAttack == false && !isAttacking) 
            {
            CancelAttack();
            ChangeAnim("Idle"); 
            }
            else
            {
                if (!isHadObject() || isTimeAttackNext == false) return;
                if (isPrepareAttacking == false && !isAttacking)
                {
                    LookTarGet();
                    CountDownAttack();
                }
                if (isPrepareAttacking)
                {
                    timeToAttack += Time.fixedDeltaTime;
                    if (timeToAttack > 0.5f && !isAttacking)
                    {
                        LookTarGet();
                        Attack();
                    }
                }              
            }
        }
    } 
}
