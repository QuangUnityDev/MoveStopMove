using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : Charecter
{
    public NavMeshAgent nav;
    private IState currentState;
    public int numberThrowed;
    public Transform tarGetSeek;
    protected override void Start()
    {
        base.Start();
        SetNumberThrow();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        
        DeActiveWeapon();
        ChangeRandomWeapon();
        ChangeRandomSkin();
    }
    private void ChangeRamdomSkinWeapon()
    {
        ManagerWeapon.GetInstance().ChangeSkinWeaponRandom(currentWeaponEquiped);
    }
    private void ChangeRandomWeapon()
    {
        int intRandomWeapon = Random.Range(0, 3);
        switch (intRandomWeapon)
        {
            case 0:
                currentWeapon = TypeWeaapon.AXE;
                break;
            case 1:
                currentWeapon = TypeWeaapon.BOOMERANG;
                break;
            case 2:
                currentWeapon = TypeWeaapon.CANDYTREE;
                break;

            default:
                break;
        }
        ChangeEquiped(intRandomWeapon);
    }
    private void ChangeRandomSkin()
    {
        int skinRandom = Random.Range(0, ManagerSkinUsing.GetInstance().dataskin.Length);
        IDataSkin idata = ManagerSkinUsing.GetInstance().dataskin[skinRandom].iDataSkin[Random.Range(0, ManagerSkinUsing.GetInstance().dataskin[skinRandom].iDataSkin.Length)];
        transform.GetComponent<ChangSkin>().ChangeSkin(idata.skin, idata.prefabWing, idata.prefabTail, idata.prefabHead, idata.prefabBow, idata.shorts);
        if(idata.skin == null)
        {
            transform.GetComponent<ChangSkin>().ChangeRandomSkinDeafault(dataPlayer);
        }
    }
    public void SetNumberThrow()
    {
        StartCoroutine(DelayReload());
    }
    IEnumerator DelayReload()
    {
        yield return new WaitForSeconds(2);
        numberThrowed = Random.Range(2, 3);
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeEquiment.GetInstance().ResetAtributeWeapon(currentWeapon, colliderRange, spriteRange, this);
        ChangState(new IdleState());
        nav.speed = speed;
    }
    public Charecter SetTargetRandom()
    {
        int numberPlayerInListAll = LevelManager.GetInstance().listAllTarget.Count;
        target = LevelManager.GetInstance().listAllTarget[Random.Range(0, numberPlayerInListAll)];
        if(target.id == id || !target.gameObject.activeSelf)
        {
            target = LevelManager.GetInstance().listAllTarget[Random.Range(0, numberPlayerInListAll)];
        }       
        return target;
    }
    void FixedUpdate()
    {
        if (GameManager.GetInstance().IsPreparing) {
            canvasInfo.OffInfoPlayer();
            return; }
        else canvasInfo.OnInfoPlayer();
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }
    public void ChangState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public override void Attack()
    {
      base.Attack();
    }
    protected override void DeAttack()
    {
        base.DeAttack();
        ChangState(new PatrolState());
    }
    public void NextState()
    {
        Invoke(nameof(BackStateIdle), 0.5f);
    }
    public void BackStateIdle()
    {
        ChangState(new IdleState());
    }
    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.GetInstance().playerAlive--;
    }
    public override void Death()
    {
        base.Death();
        LevelManager.GetInstance().callSpawn?.Invoke();     
    }
}
