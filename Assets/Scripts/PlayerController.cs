using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public VariableJoystick variableJoystick;
    private void Start()
    {
        OnInit();
        //switch (SaveLoadData.GetInstance().data.currentWeapon)
        //{
        //    case 0:
        //        typeWeaapon = TypeWeaapon.BULLET;
        //        break;
        //    case 1:
        //        typeWeaapon = TypeWeaapon.SWORD;
        //        break;
        //    case 2:
        //        typeWeaapon = TypeWeaapon.BOOMERANG;
        //        break;
        //    default:
        //        break;
        //}
    }
    public override void OnInit()
    {
        id = GameManager.id;
        timeAttackNext = 0;
        GameManager.GetInstance().listTarget.Add(gameObject);
    }
    public void FixedUpdate()
    {     
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        if ( rb.velocity != Vector3.zero)  transform.rotation = Quaternion.LookRotation(direction);
        
        if (Mathf.Abs(variableJoystick.Vertical) > 0 || Mathf.Abs(variableJoystick.Horizontal) > 0)
        {
            time = 0;           
            ChangeAnim("Run");
        }
        else 
        {
            if (isAttacking == false)
                ChangeAnim("Idle");
        }
        if (target == null || target.gameObject.activeSelf == false) return;
      
        if (target.gameObject.activeSelf == false)
        {
            target = null;
        }
        
        if (isAttack == true)
        {
           
            if (Mathf.Abs(variableJoystick.Vertical) < 0.1f || Mathf.Abs(variableJoystick.Horizontal) < 0.1f)
            {             
                timeAttackNext += Time.deltaTime;
                if (timeAttackNext > timeToAttack)
                {
                    throwPos.transform.LookAt(target);
                    transform.LookAt(new Vector3(target.localPosition.x, transform.position.y, target.localPosition.z));
                    if (isAttacking == false)
                    {
                        Attack();
                    }                  
                }
            }
            else timeAttackNext = 0;           
        }
    }
    public override void Attack()
    {
        base.Attack(); 
    }
}
