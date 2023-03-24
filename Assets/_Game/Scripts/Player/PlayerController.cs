using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public FloatingJoystick floatingJoystick;    
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
        return Mathf.Abs(floatingJoystick.Vertical) < 0.01f || Mathf.Abs(floatingJoystick.Horizontal) < 0.01f;
    }
    public void FixedUpdate()
    {
        if (isDead) return;
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
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
                    if (timeToAttack > 0.4f && !isAttacking)
                    {                       
                        Attack();
                    }
                }              
            }
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();
        ChangeAnim(GlobalTag.playerAnimDeath);
    }
}
