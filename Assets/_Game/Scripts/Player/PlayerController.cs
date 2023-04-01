using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public FloatingJoystick floatingJoystick;    
    protected override void Start()
    {      
        base.Start();       
    }
    public override void OnInit()
    {
        currentWeapon = GameManager.GetInstance().currentWeapon;
        base.OnInit();        
        ChangeEquiped(typeWeaapon);
        GameManager.GetInstance().ShowRangePlayer((isTrue) => {
            spriteRange.gameObject.SetActive(isTrue);
            Transform.position = new Vector3(0, -0.44f, 0);
        }, (isTrue) => Transform.gameObject.SetActive(isTrue));
    }
    public bool IsStop()
    {
        return Mathf.Abs(floatingJoystick.Vertical) < 0.01f && Mathf.Abs(floatingJoystick.Horizontal) < 0.01f;
    }
    public void FixedUpdate()
    {
        if (GameManager.GetInstance().IsPreparing) return;
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
        if(currentWeaponEquiped)
        ThrowWeapon();
        base.OnDeath();
        if(GameManager.numberOfReviveInOneTimesPlay > 0)
        {
            GameManager.numberOfReviveInOneTimesPlay--;
            GameManager.GetInstance().GameRevival();
        }
        else GameManager.GetInstance().GameOver();

        ChangeAnim(GlobalTag.playerAnimDeath);
    }
}
