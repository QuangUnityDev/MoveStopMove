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

    public float speed;
    public float timeToAttack;
    private float scareValue;

    public bool isPrepareAttacking;
    public bool isAttacking;
    public bool isTimeAttackNext;
     
    public Action removeTarget;   
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
        materialPlayer.material = dataPlayer.GetMat(UnityEngine.Random.Range(0, dataPlayer.materials.Length));
        isPrepareAttacking = false;
        isAttacking = false;
        isTimeAttackNext = true;
        ChangeEquiment.GetInstance().ChangeWeapon(currentWeapon, colliderRange, spriteRange, typeWeaapon);
    }

    public virtual void Attack()
    {
        isAttacking = true;
        isTimeAttackNext = false;
        switch (typeWeaapon)
        {
            case TypeWeaapon.AXE:
                Axe more = ObjectsPooling.GetInstance().SpawnBullet(throwPos);
                WeaponGetInfo(more, WeaponAtributesFirst.rangeBullet);
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
    public void RemoveTarget(Action call = null)
    {
        removeTarget += call;
    }
    public void WeaponGetInfo(Weapon wepon, float rangeFirst)
    {
        if (target != null)
        {
            dir = target.transform.position - throwPos.position;
        }
        wepon.player = this;
        wepon.rb.AddForce(dir.normalized * wepon.ShootForce, ForceMode.VelocityChange);
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
        return targetAttack != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {
            target = other.GetComponent<Charecter>();
            targetAttack = target.Transform;
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
}
