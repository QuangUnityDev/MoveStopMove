using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float time;
    float nextStateTime;
    public void OnEnter(BotController botController)
    {
        time = 0;
        botController.nav.speed = botController.speed;
        botController.tarGetSeek = botController.SetTargetRandom().transform;
        nextStateTime = 0;
    }

    public void OnExcute(BotController botController)
    {
        time += Time.deltaTime;
        if (botController.tarGetSeek == null || time > 4)
        {
            botController.ChangState(new IdleState());
        }
        else
        {
            botController.ChangeAnim(GlobalTag.playerAnimRun);            
            botController.nav.SetDestination(botController.tarGetSeek.position);
            botController.LookTarGet(botController.tarGetSeek);
        }
        if (botController.IsHadObject() == true)
        {
            nextStateTime += Time.deltaTime;
            if (nextStateTime > 0.5f)
            {
                botController.ChangState(new AttackState());
            }
        }      
    }

    public void OnExit(BotController botController)
    {

    }
}
