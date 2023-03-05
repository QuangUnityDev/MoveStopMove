using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Charecter
{
    public VariableJoystick variableJoystick;
    private float timeAttackNext;
    private void Start()
    {
        OnInit();
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
        if ( rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        if (Mathf.Abs(variableJoystick.Vertical) > 0 || Mathf.Abs(variableJoystick.Horizontal) > 0)
        {
            time = 0;
            ChangeAnim("Run");
        }
        else 
        { 
            ChangeAnim("Idle");
        }
        if (isAttack == true && rb.velocity == Vector3.zero)
        {           
            timeAttackNext += Time.deltaTime;
            if(timeAttackNext > 2)
            {
                Attack();
                timeAttackNext = 0;
            }          
        }
        transform.localScale = new Vector3(1 + killed * 0.2f, 1 + killed * 0.2f, 1 + killed * 0.2f);
    }
    public void GetKill(int idBullet)
    {
        if (idBullet == id)
        {
            killed++;
        }
    }
    public override void Attack()
    {
        base.Attack(); 
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerEnemy") && rb.velocity == Vector3.zero)
        {
            target = other.gameObject.transform;
            transform.LookAt(new Vector3(target.localPosition.x, transform.position.y, target.localPosition.z));
            throwPos.transform.LookAt(target);
        }       
    }
}
