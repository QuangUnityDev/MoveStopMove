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


    public static Action<Charecter> removeTarget;
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
        CandyTree,
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
        listTargetInRange.Clear();
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
        GameManager.GetInstance().SaveData();
        if (currentWeaponEquiped != null && typeWeaapon == TypeWeaapon.CandyTree) 
        {
            WeaponOnHand();
            return;
        }
        if (currentWeaponEquiped != null) currentWeaponEquiped = null;
        switch (typeWeaapon)
        {
            case TypeWeaapon.AXE:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBullet(throwPos);             
                break;
            case TypeWeaapon.BOOMERANG:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos);
                break;
            case TypeWeaapon.CandyTree:
                currentWeaponEquiped = ObjectsPooling.GetInstance().SpawnCandyTree(throwPos);
                break;
            default:
                break;
        }
        WeaponOnHand();
    }
    public void WeaponOnHand()
    {
        if (typeWeaapon == TypeWeaapon.CandyTree) return;      
        currentWeaponEquiped.transform.SetParent(throwPos);
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
        if (typeWeaapon == TypeWeaapon.CandyTree) return;
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
    public static void RemoveTarget(Action<Charecter> call = null)
    {
        removeTarget += call;
    }
 
    public void NextTimeAttack()
    {
        isTimeAttackNext = true;
        isPrepareAttacking = false;
        timeToAttack = 0;
    }
    protected virtual void DeAttack()
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
    public void ResetPlayer()
    {
        killed = 0;
        Transform.localScale = new Vector3(1, 1, 1);
    }
    public bool IsHadObject()
    {
        if (listTargetInRange.Count > 0)
        {
            target = listTargetInRange[listTargetInRange.Count - 1];
            targetAttack = target.Transform;
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
            if(!target.isDead && !listTargetInRange.Contains(target))
            {
                listTargetInRange.Add(target);
                RemoveTarget((target) => listTargetInRange.Remove(target));
            }       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalTag.player) || other.CompareTag(GlobalTag.playerEnemy))
        {
            target = other.GetComponent<Charecter>();
            removeTarget?.Invoke(target);
        }
    }
    void DeActiveWeapon()
    {
        if (!currentWeaponEquiped) return;
        currentWeaponEquiped.gameObject.SetActive(false);
        currentWeaponEquiped = null;
    }
    public virtual void OnDeath()
    {
        removeTarget?.Invoke(this);
        isDead = true;
        rb.velocity = Vector3.zero;
        DeActiveWeapon();
        removeTarget?.Invoke(this);
        Invoke(nameof(Death), 2f);
    }
    public virtual void Death()
    {
        gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        removeTarget = null;
    }
}
