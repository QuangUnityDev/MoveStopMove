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
   
    public Transform throwPos;
    public bool isAttack;
    public int killed;
    public int id;
    public int currentWeapon;
    public float timeToAttack;

    public bool isPrepareAttacking;
    public bool isAttacking;
    public bool isTimeAttackNext;
    public float scareValue;
    protected virtual void Start()
    {
        scareValue = GameManager.GetInstance().valueScare * WeaponAtributesFirst.rangeFirst;
        //Debug.LogError(scareValue);
    }
    public enum TypeWeaapon
    {
        BULLET,
        SWORD,
        BOOMERANG,
    }
    public void TakeWeapon()
    {
        //Instantiate()
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
        isPrepareAttacking = false;
        isAttacking = false;
        isTimeAttackNext = true;
        ChangeEquiment.GetInstance().ChangeWeapon(currentWeapon, colliderRange, spriteRange, typeWeaapon);
    }
    Vector3 dir; 
    public virtual void Attack()
    {
        isAttacking = true;
        isTimeAttackNext = false;
        switch (typeWeaapon)
        {
            case TypeWeaapon.BULLET:
                Bullet more = ObjectsPooling.GetInstance().SpawnBullet(throwPos);
                WeaponGetInfo(more,WeaponAtributesFirst.rangeBullet);               
                break;
            case TypeWeaapon.BOOMERANG:
                Boomerang boome = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                WeaponGetInfo(boome, WeaponAtributesFirst.rangeBoomerang);
                break;
            case TypeWeaapon.SWORD:
                //Knife knift = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                ChangeAnim("Attack");
                break;
            default:
                break;
        }
        Invoke(nameof(DeAttack), 0.4f);
    }
    public void WeaponGetInfo(Weapon wepon, float rangeFirst)
    {
        wepon.rb.AddForce(dir.normalized * wepon.shootForce, ForceMode.VelocityChange);
        wepon.GetInfoPlayer(id, throwPos.position);
        wepon.rangWeapon = rangeFirst + killed * scareValue;
    }
    public void NextTimeAttack()
    {
        isTimeAttackNext = true;
        isPrepareAttacking = false;
        timeToAttack = 0;
    }
    protected void DeAttack()
    {
        Invoke(nameof(NextTimeAttack), 0.3f);        
        isAttacking = false;
    }
    public void CancelAttack()
    {
        isPrepareAttacking = false;
        timeToAttack = 0;
    }
    public void CountDownAttack()
    {
        ChangeAnim("Attack");
        isPrepareAttacking = true;
    }
    public void LookTarGet()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        throwPos.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    }
    public bool isHadObject()
    {
        return target != null && target.gameObject.activeSelf == true;
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
