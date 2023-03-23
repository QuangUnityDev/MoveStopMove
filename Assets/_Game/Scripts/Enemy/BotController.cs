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
    }
    private void OnEnable()
    {
        OnInit();
        base.OnInit();
        currentWeapon = 2;            
    }
    public void SetNumberThrow()
    {
        numberThrowed = Random.Range(0, 3);
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
        SetNumberThrow();
        nav.speed = speed;
        ChangeEquiment.GetInstance().ChangeWeapon(currentWeapon, colliderRange, spriteRange, typeWeaapon);
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
        if (isAttacking == false && isPrepareAttacking == false)
        {
            ChangState(new PatrolState());
        }
    }
    public void NextState()
    {
        Invoke(nameof(BackStateIdle), 0.5f);
    }
    public void BackStateIdle()
    {
        ChangState(new IdleState());
    }
}
