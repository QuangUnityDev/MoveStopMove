using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public FloatingJoystick floatingJoystick;
    GameManager data;
    protected override void OnEnable()
    {
        base.OnEnable();    
    }
    public override void OnInit()
    {
        base.OnInit();
        data = GameManager.GetInstance();
        currentWeapon = data.dataPlayer.equipedWeapon;
        ChangeEquiment.GetInstance().ResetAtributeWeapon(data.dataPlayer.equipedWeapon, colliderRange, spriteRange, this);       
        ChangeEquiped((int)currentWeapon);
        ManagerWeapon.GetInstance().ChangeSkinWeapon(currentWeaponEquiped);
        data.ShowRangePlayer((isTrue) => {
            spriteRange.gameObject.SetActive(isTrue);
            _transform.position = new Vector3(0, -0.44f, 0);
        }, (isTrue) => _transform.gameObject.SetActive(isTrue));
    }
    public bool IsStop()
    {
        return Mathf.Abs(floatingJoystick.Vertical) < 0.01f && Mathf.Abs(floatingJoystick.Horizontal) < 0.01f;
    }
    public void FixedUpdate()
    {
        if (data.IsPreparing) return;
        if (isDead) return;
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if(!isAttacking) rb.velocity = direction * speed * Time.fixedDeltaTime;
        if (rb.velocity != Vector3.zero) _transform.rotation = Quaternion.LookRotation(direction);

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
            if (IsHadObject() && !isAttacking ) 
            {
                if (!isPrepareAttacking && isTimeAttackNext)
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
            else
            {
                CancelAttack();
                ChangeAnim("Idle");
            }
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();     
        ChangeAnim(GlobalTag.playerAnimDeath);
    }
    public override void Death()
    {
        base.Death();
        if (GameManager.numberOfReviveInOneTimesPlay > 0)
        {
            GameManager.numberOfReviveInOneTimesPlay--;
            data.GameRevival();
        }
        else data.GameOver();

    }
}
