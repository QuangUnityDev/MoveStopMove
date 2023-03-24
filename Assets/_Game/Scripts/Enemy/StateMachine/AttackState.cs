using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(BotController botController)
    {
        botController.LookTarGet(botController.targetAttack);
        botController.nav.speed = 0;
        botController.isTimeAttackNext = true;
        botController.timeToAttack = 0;
    }

    public void OnExcute(BotController botController)
    {
        if (botController.isDead)
        {
            botController.ChangState(new DeathState());
        }
        if (!botController.IsHadObject())
        {     
           botController.ChangState(new PatrolState());
        }
        if (botController.numberThrowed > 0)
        {
            if (botController.isAttacking || !botController.IsHadObject() || botController.isTimeAttackNext == false) return;
            if (!botController.isPrepareAttacking)
            {
                botController.CountDownAttack();
            }
            if (botController.isPrepareAttacking)
            {
                botController.timeToAttack += Time.fixedDeltaTime;
                botController.LookTarGet(botController.targetAttack);
                if (botController.timeToAttack > 0.5f)
                {                  
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
