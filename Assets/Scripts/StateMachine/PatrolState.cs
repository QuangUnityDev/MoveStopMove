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
        botController.SetTargetRandom();
        nextStateTime = 0;
    }

    public void OnExcute(BotController botController)
    {
        time += Time.deltaTime;
        if (!botController.isHadObject())
        {
            botController.SetTargetRandom();
        }
        botController.ChangeAnim("Run");
        if (botController.isAttack == true)
        {
            nextStateTime += Time.deltaTime;
            if (nextStateTime > 0.5f)
            {
                botController.ChangState(new AttackState());
            }
        }
        else if (time > 4) botController.ChangState(new IdleState());
        else
        {
            botController.nav.SetDestination(botController.target.position);
        }
    }

    public void OnExit(BotController botController)
    {

    }
}
