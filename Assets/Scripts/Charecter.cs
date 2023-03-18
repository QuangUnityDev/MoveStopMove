using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    [SerializeField]protected Rigidbody rb;
    public Animator anim;

    public TypeWeaapon typeWeaapon;
    public Transform target;
    public Transform colliderRange;
    public Transform spriteRange;
    public float speed;
    private string currentAnim;
   
    public float timeToAttack;
    public Transform throwPos;
    public float time;
    public bool isAttack;
    public int killed;
    public int id;
    public Knife knife;
   
    
    public bool isAttacking;
    protected float timeAttackNext;
    public float scareValue;
    protected virtual void Start()
    {
        scareValue = GameManager.GetInstance().valueScare * WeaponAtributesFirst.rangeFirst;
        Debug.LogError(scareValue);
    }
    public enum TypeWeaapon
    {
        BULLET,
        SWORD,
        BOOMERANG,
    }

    public void ChangeAnim(string animName)
    {
        if (animName != currentAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            dir = target.position - throwPos.position;
            if (!target.gameObject.activeSelf)
            {
                isAttack = false;
            }
        }      
    }
    public virtual void OnInit()
    {
        isAttack = false;
     
    }
    Vector3 dir; 
    public virtual void Attack()
    {
        switch (typeWeaapon)
        {
            case TypeWeaapon.BULLET:
                Bullet more = ObjectsPooling.GetInstance().SpawnBullet(throwPos);
                WeaponGetInfo(more,WeaponAtributesFirst.rangeBullet);
                break;
            case TypeWeaapon.BOOMERANG:
                Boomerang boome = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                WeaponGetInfo(boome, WeaponAtributesFirst.rangeBoomerang);
                DeAttack();
                break;
            case TypeWeaapon.SWORD:
                //Knife knift = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                isAttacking = true;
                ChangeAnim("Attack");
                timeToAttack += 3;
                Invoke(nameof(DeAttack),2.25f);
                break;
            default:
                break;
        }
    }
    public void WeaponGetInfo(Weapon wepon, float range)
    {
        wepon.rb.AddForce(dir.normalized * wepon.shootForce, ForceMode.VelocityChange);
        wepon.GetInfoPlayer(id, throwPos.position);
        isAttacking = true;
        wepon.rangWeapon = range + killed * scareValue + killed * 0.1f;
    }
    protected void DeAttack()
    {
        timeAttackNext = 0;
        isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnemy"))
        {
            isAttack = true;
            target = other.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnemy"))
        {
            isAttack = false;
            target = null;
        }
    }
}
