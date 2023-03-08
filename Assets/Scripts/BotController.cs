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
    private void OnEnable()
    {
        OnInit();
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
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
        nav.speed = speed;
    }
    public void SetTargetRandom()
    {
        target = GameManager.GetInstance().listTarget[Random.Range(0, GameManager.GetInstance().listTarget.Count)].transform;
        while(target == transform)
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
        //Debug.LogError(newState);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerEnemy") || other.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            transform.LookAt(new Vector3(target.localPosition.x, transform.position.y, target.localPosition.z));
            throwPos.transform.LookAt(target);
        }
    }
}
