using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time;
    float nextState;
    public void OnEnter(BotController botController)
    {
        time = 0;
        botController.nav.speed = 0;
        nextState = Random.Range(3, 7);
    }

    public void OnExcute(BotController botController)
    {
        botController.ChangeAnim("Idle");
        time += Time.deltaTime;
        if (botController.isAttack == true && time > botController.timeToAttack)
        {
            botController.ChangState(new AttackState());
        }  
        else
        if( time > nextState && botController.isAttack == false)
        {
            botController.ChangState(new PatrolState());
        }
    }

    public void OnExit(BotController botController)
    {
     
    }

}
