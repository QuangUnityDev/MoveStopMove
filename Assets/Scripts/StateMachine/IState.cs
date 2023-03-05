using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(BotController botController);
    void OnExcute(BotController botController);
    void OnExit(BotController botController);
}
