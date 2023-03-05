using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time;
    private bool isFirstAttack;
    public void OnEnter(BotController botController)
    {
        time = 0;
        isFirstAttack = true;
    }

    public void OnExcute(BotController botController)
    {       
        botController.transform.LookAt(new Vector3(botController.target.position.x,botController.transform.position.y,botController.target.position.z));
        botController.throwPos.transform.LookAt(botController.target);
        time += Time.deltaTime;
        if(isFirstAttack == true && time > 1 && botController.isAttack == true)
        {
            botController.ChangeAnim("Attack");
            botController.Attack();
            isFirstAttack = false;
        }
        if (botController.isAttack == false || !botController.target.gameObject.activeSelf)
        {
            botController.ChangState(new PatrolState());
        }
        else
        if (time > 4 && botController.isAttack == true && botController.target.gameObject.activeSelf)
        {
            Debug.LogError("Tan cong lan 2");
            botController.ChangeAnim("Attack");
            botController.Attack();
            botController.isAttack = false;
        }      
       
    }

    public void OnExit(BotController botController)
    {

    }
}
