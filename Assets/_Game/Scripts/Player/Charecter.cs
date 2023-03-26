using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    public List<Charecter> listTargetInRange;
    public Animator anim;
    public TypeWeaapon typeWeaapon;
    public Charecter target;
    [SerializeField] protected DataPlayer dataPlayer;

    public Weapon currentWeaponEquiped; 

    [HideInInspector] public Transform Transform;
    public Transform colliderRange;
    public Transform spriteRange;
    public Transform throwPos;
    public Transform targetAttack;
    private Vector3 dir;
    public SkinnedMeshRenderer materialPlayer;

    private string currentAnim;

    public int killed;
    public int id;
    public int currentWeapon;
    public int hp;

    public float speed;
    public float timeToAttack;
    private float scareValue;

    public bool isPrepareAttacking;
    public bool isAttacking;
    public bool isTimeAttackNext;
    public bool isDead;


    public static Action removeTarget;
    protected virtual void OnEnable()
    {
        OnInit();
    }
    private void Awake()
    {
        Transform = transform;
    }
    protected virtual void Start()
    {
        scareValue = 0.2f * WeaponAtributesFirst.rangeFirst;
    }
    public enum TypeWeaapon
    {
        AXE,
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

    public virtual void OnInit()
    {
        listTargetInRange.RemoveAll(listTargetInRange => listTargetInRange);
        hp = dataPlayer.hp; 
        materialPlayer.material = dataPlayer.GetMat(UnityEngine.Random.Range(0, dataPlayer.materials.Length));
        isPrepareAttacking = false;
        isAttacking = false;
        isTimeAttackNext = true;
        isDead = false;
        ChangeEquiment.GetInstance().ResetAtributeWeapon(currentWeapon, colliderRange, spriteRange, this);
    }

    public void ChangeEquiped(TypeWeaapon type)
    {
        switch (typeWeaapon)
        {
            case TypeWeaapon.AXE:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBullet(throwPos);             
                break;
            case TypeWeaapon.BOOMERANG:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                break;
            case TypeWeaapon.SWORD:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBullet(throwPos);
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
        currentWeaponEquiped.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void WeaponThrowed()
    {
        if (currentWeaponEquiped == null) return;
        currentWeaponEquiped.rb.constraints = RigidbodyConstraints.None;
        currentWeaponEquiped.ShootForce = 10;
        currentWeaponEquiped.transform.SetParent(null);
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
        WeaponThrowed();
        Invoke(nameof(DeAttack), 0.4f);      
    }
    public static void RemoveTarget(Action call = null)
    {
        removeTarget += call;
    }
    public void WeaponGetInfo(Weapon wepon, float rangeFirst)
    {        
        wepon.player = this;
        wepon.GetInfoPlayer(id, throwPos.position);
        wepon.posStart = throwPos.position;       
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
        ChangeEquiped(typeWeaapon);
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
        Transform.LookAt(new Vector3(lookTarget.position.x, Transform.position.y, lookTarget.position.z));
        throwPos.LookAt(new Vector3(lookTarget.position.x, lookTarget.position.y, lookTarget.position.z));
    }
    public bool IsHadObject()
    {
        if (listTargetInRange.Count > 0)
        {
            target = listTargetInRange[listTargetInRange.Count - 1];
            targetAttack = target.Transform;
            RemoveTarget(() => listTargetInRange.Remove(target));
        }
        else targetAttack = null;      
        return targetAttack != null && target.isDead == false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {         
            target = other.GetComponent<Charecter>();
            targetAttack = target.Transform;
            if(!target.isDead)
            listTargetInRange.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {
            listTargetInRange.Remove(other.GetComponent<Charecter>());
        }
    }
    public virtual void OnDeath()
    {
        isDead = true;
        removeTarget?.Invoke();
        Invoke(nameof(Death), 2f);
    }
    public virtual void Death()
    {
        gameObject.SetActive(false);
        if(!currentWeaponEquiped)
        WeaponThrowed();
    }
    public void OnDisable()
    {
        removeTarget = null;
    }
}
