using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : Charecter
{
    public NavMeshAgent nav;
    private IState currentState;
    [SerializeField] private SkinnedMeshRenderer mesh;
    private int randomMat;
    public int numberThrowed;
    protected override void Start()
    {
        base.Start();
    }
    private void OnEnable()
    {
        OnInit();
        base.OnInit();
        currentWeapon = 2;      
    #region randomMat
    randomMat = Random.Range(0, 4);
        switch (randomMat)
        {
            case 0:
                mesh.material.color = Color.black;
                break;
            case 1:
                mesh.material.color = Color.yellow;
                break;
            case 2:
                mesh.material.color = Color.green;
                break;
            case 3:
                mesh.material.color = Color.grey;
                break;
            default:
                break;
        }
        #endregion       
    }
    public void SetNumberThrow()
    {
        numberThrowed = Random.Range(0, 3);
    }
    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        ChangState(new IdleState());
        SetNumberThrow();
        nav.speed = speed;
        ChangeEquiment.GetInstance().ChangeWeapon(currentWeapon, colliderRange, spriteRange, typeWeaapon);
    }
    public void SetTargetRandom()
    {
        target = GameManager.GetInstance().listTarget[Random.Range(0, GameManager.GetInstance().listTarget.Count)].transform;
        if(target == transform)
        {
            target = GameManager.GetInstance().listTarget[Random.Range(0, GameManager.GetInstance().listTarget.Count)].transform;
        }
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
