using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(BotController botController)
    {
        botController.LookTarGet();
        botController.nav.speed = 0;
        botController.isTimeAttackNext = true;
        botController.timeToAttack = 0;
    }

    public void OnExcute(BotController botController)
    {
        if (!botController.isHadObject())
        {     
           botController.ChangState(new PatrolState());
        }
        if (botController.numberThrowed > 0)
        {

            if (botController.isAttacking || !botController.isAttack || botController.isTimeAttackNext == false) return;
            if (botController.isPrepareAttacking == false)
            {
                botController.LookTarGet();
                botController.CountDownAttack();
            }
            if (botController.isPrepareAttacking && !botController.isAttacking)
            {
                botController.timeToAttack += Time.fixedDeltaTime;
                if (botController.timeToAttack > 0.5f)
                {
                    botController.LookTarGet();
                    botController.Attack();
                    botController.numberThrowed--;
                }
            }
        }       
        else
        {
            botController.SetNumberThrow();
            botController.ChangState(new PatrolState());
        }
    }
    public void OnExit(BotController botController)
    {

    }
}
