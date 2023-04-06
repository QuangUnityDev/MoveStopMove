using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    public Rigidbody rb;
    public List<Charecter> listTargetInRange;
    public Animator anim;
    public TypeWeaapon typeWeaapon;
    public Charecter target;
    public DataPlayer dataPlayer;

    public Weapon currentWeaponEquiped; 

    [HideInInspector] public Transform _transform;
    public Transform colliderRange;
    public Transform spriteRange;
    public Transform throwPos;
    public Transform targetAttack;
    private Vector3 dir;

    private string currentAnim;

    public int killed;
    public int id;
    public int currentWeapon;
    public float speed;
    public float timeToAttack;
    public int hp;

    private float scareValue;

    public bool isPrepareAttacking;
    public bool isAttacking;
    public bool isTimeAttackNext;
    public bool isDead;
    private CapsuleCollider _collider;
   
    protected virtual void OnEnable()
    {
       
    }
    private void Awake()
    {
        _transform = transform;
        _collider = transform.GetComponent<CapsuleCollider>();
    }
    protected virtual void Start()
    {
        OnInit();
        scareValue = 0.2f * WeaponAtributesFirst.rangeFirst;
    }
    public enum TypeWeaapon
    {
        AXE,      
        BOOMERANG,
        CandyTree,
    }
    public void ChangeSkinWeapon(Material material)
    {
        currentWeaponEquiped.mesh.materials[1] = material;
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

    public virtual void OnInit()
    {
        _collider.center = new Vector3(0, 0, 0);
        listTargetInRange.Clear();
        hp = dataPlayer.hp;        
        isPrepareAttacking = false;
        isAttacking = false;
        isTimeAttackNext = true;
        isDead = false;       
    }

    public void ChangeEquiped(int currentWeapon)
    {
        if (currentWeaponEquiped != null && typeWeaapon == TypeWeaapon.CandyTree) 
        {
            WeaponOnHand();
            return;
        }
        if (currentWeaponEquiped != null) DeActiveWeapon();
        switch (currentWeapon)
        {
            case (int)TypeWeaapon.AXE:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBullet(throwPos);             
                break;
            case (int)TypeWeaapon.BOOMERANG:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                break;
            case (int)TypeWeaapon.CandyTree:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnCandyTree(throwPos);
                break;
            default:
                break;
        }
        WeaponOnHand();
    }
    public void WeaponOnHand()
    {
        WeaponGetInfo(currentWeaponEquiped, WeaponAtributesFirst.rangeBullet);     
        currentWeaponEquiped.transform.SetParent(throwPos);
        currentWeaponEquiped.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        currentWeaponEquiped.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void WeaponGetInfo(Weapon wepon, float rangeFirst)
    {
        wepon.player = this;
        wepon.GetInfoPlayer(id, throwPos.position);
        wepon.posStart = throwPos.position;
        wepon.rangWeapon = rangeFirst + killed * scareValue;
    }
    public void ThrowWeapon()
    {
        if (typeWeaapon == TypeWeaapon.CandyTree) { currentWeaponEquiped.ShootForce = 10; return; }       
        if (currentWeaponEquiped == null) return;
        WeaponGetInfo(currentWeaponEquiped, WeaponAtributesFirst.rangeBullet);
        currentWeaponEquiped.rb.constraints = RigidbodyConstraints.None;      
        currentWeaponEquiped.transform.SetParent(null);
        currentWeaponEquiped.ShootForce = 10;
        currentWeaponEquiped.rb.AddForce(dir.normalized * 10, ForceMode.VelocityChange);       
        currentWeaponEquiped = null;
    }
    public virtual void Attack()
    {      
        if (target != null)
        {
            dir = target.transform.position - throwPos.position;
        }
        isAttacking = true;
        isTimeAttackNext = false;
        ThrowWeapon();
        Invoke(nameof(DeAttack), 0.4f);      
    }
    public void NextTimeAttack()
    {
        isTimeAttackNext = true;
        isPrepareAttacking = false;
        timeToAttack = 0;
    }
    protected virtual void DeAttack()
    {
        ChangeEquiped(currentWeapon);
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
        ChangeAnim(GlobalTag.playerAnimAttack);
        isPrepareAttacking = true;
    }
    public void LookTarGet(Transform lookTarget)
    {
        _transform.LookAt(new Vector3(lookTarget.position.x, _transform.position.y, lookTarget.position.z));
        throwPos.LookAt(new Vector3(lookTarget.position.x, lookTarget.position.y, lookTarget.position.z));
    }
    public void ResetPlayer()
    {
        killed = 0;
        _transform.localScale = new Vector3(1, 1, 1);
    }
    public bool IsHadObject()
    {
        if (listTargetInRange.Count > 0)
        {
            target = listTargetInRange[listTargetInRange.Count - 1];
            targetAttack = target._transform;
        }
        else targetAttack = null;      
        return targetAttack != null && target.isDead == false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {         
            target = other.GetComponent<Charecter>();
            targetAttack = target._transform;
            listTargetInRange.Add(target);    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {
            target = other.GetComponent<Charecter>();
            listTargetInRange.Remove(other.GetComponent<Charecter>());
        }
    }
    protected void DeActiveWeapon()
    {
        if (currentWeaponEquiped!= null) { 
        currentWeaponEquiped.gameObject.SetActive(false);
        currentWeaponEquiped.transform.SetParent(null);
        currentWeaponEquiped = null;
        }
    }
    public virtual void OnDeath()
    {
        _collider.center = new Vector3(0, 10, 0);
        isDead = true;
        rb.velocity = Vector3.zero;
        DeActiveWeapon();
        Invoke(nameof(Death), 2f);
    }
    public virtual void Death()
    {
        gameObject.SetActive(false);
    }
}
