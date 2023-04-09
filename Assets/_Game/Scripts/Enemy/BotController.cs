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
        ChangeEquiped((int)currentWeapon);
    }
    public void SetNumberThrow()
    {
        StartCoroutine(DelayReload());
    }
    IEnumerator DelayReload()
    {
        yield return new WaitForSeconds(3);
        numberThrowed = Random.Range(1, 3);
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
        if (GameManager.GetInstance().IsPreparing) return;
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
    public override void Death()
    {
        base.Death();
        LevelManager.GetInstance().callSpawn?.Invoke();
    }
}
