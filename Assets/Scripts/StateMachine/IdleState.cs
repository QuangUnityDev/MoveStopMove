using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time;
    float nextState;
    float timeWaitAttack;
    public void OnEnter(BotController botController)
    {
        timeWaitAttack = 0;
        time = 0;
        botController.nav.speed = 0;
        nextState = Random.Range(3, 7);
    }

    public void OnExcute(BotController botController)
    {
        botController.ChangeAnim("Idle");
        time += Time.deltaTime;
        if(botController.isAttack == true)
        {
            timeWaitAttack += Time.deltaTime;
        }
        if (botController.isAttack == true && timeWaitAttack > botController.timeToAttack)
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
