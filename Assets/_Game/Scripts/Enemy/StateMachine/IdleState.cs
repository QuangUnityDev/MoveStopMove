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
        nextState = Random.Range(1, 4);
    }

    public void OnExcute(BotController botController)
    {
        botController.rb.velocity = Vector3.zero;
        if (botController.isDead)
        {
            botController.ChangState(new DeathState());
        }
        botController.ChangeAnim(GlobalTag.playerAnimIdle);
        time += Time.deltaTime;
        if (botController.IsHadObject() && botController.numberThrowed > 0)
        {
            timeWaitAttack += Time.deltaTime;
            if (timeWaitAttack > 0.2f)
            {                
              botController.ChangState(new AttackState());
            }
        }
        else
        {
            timeWaitAttack = 0;
            if (time > nextState)
            {
                botController.ChangState(new PatrolState());
            }
        }

    }

    public void OnExit(BotController botController)
    {
     
    }

}
