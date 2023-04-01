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
            return;
        }
        if (!botController.IsHadObject())
        {     
           botController.ChangState(new PatrolState());
            return;
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
                botController.rb.velocity = Vector3.zero;
                if (botController.timeToAttack > 0.5f)
                {
                    botController.timeToAttack = 0;
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
