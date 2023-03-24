using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    public void OnEnter(BotController botController)
    {
        botController.nav.speed = 0;        
    }

    public void OnExcute(BotController botController)
    {
        botController.ChangeAnim(GlobalTag.playerAnimDeath);
    }

    public void OnExit(BotController botController)
    {
     
    }

}
