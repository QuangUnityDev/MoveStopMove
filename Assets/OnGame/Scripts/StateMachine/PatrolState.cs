using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float time;
    public void OnEnter(BotController botController)
    {
        time = 0;
        botController.nav.speed = botController.speed;
        botController.SetTargetRandom();        
    }

    public void OnExcute(BotController botController)
    {
        time += Time.deltaTime;
        if (botController.target == null)
        {
            botController.SetTargetRandom();
        }
         else
        if (botController.isAttack == true || time > 4f)
        {
            botController.ChangState(new IdleState());
        }
        else
        {
            botController.nav.SetDestination(botController.target.position);
            botController.ChangeAnim("Run");
        }
    }

    public void OnExit(BotController botController)
    {

    }
}
