using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : IState
{
    private GoblinController controller;

    private float waitingTime = 6f;
    private float waitingTimeUsed = 0f;

    private float turnAroundTime = 1f;
    private float turnAroundTimeUsed = 0f;
    public BossChase(GoblinController goblinController)
    {
        controller = goblinController;
    }
    public void OnEnter()
    {
        if (controller.PlayerInWeaponRange())
        {
            controller.ChangeState(controller.attacking);
            return;
        }
        waitingTimeUsed = 0f;
        turnAroundTimeUsed = 0f;
    }
    public void UpdateState()
    {
        bool closeBy = controller.IsPlayerCloseBy();
        if(controller.NeedTurnAroundForTarget() ) 
        {
            controller.TurnAround();
        }
        if (controller.PlayerInWeaponRange())
        {
            controller.ChangeState(controller.attacking);
            return;
        }
        if (waitingTimeUsed >= waitingTime)
        {
            controller.nextAttackAoe = true;
            controller.ChangeState(controller.attacking);
            return;
            //waitingTimeUsed += Time.deltaTime;
            //turnAroundTimeUsed += Time.deltaTime;
            //Waiting();
        }
        else if (controller.ObstacleInWay())
        {
            waitingTimeUsed += Time.deltaTime;
            //Waiting();
        }
        else
        {
            waitingTimeUsed += Time.deltaTime;
            //turnAroundTimeUsed += Time.deltaTime;
            if(!closeBy)
                controller.WalkForward();
        }
    }

    public void Waiting()
    {
        if (waitingTimeUsed >= waitingTime)
        {
            controller.ChangeState(controller.patrol);
            return;
        }
        if (turnAroundTimeUsed >= turnAroundTime)
        {
            controller.TurnAround();
            turnAroundTimeUsed = 0f;
        }
    }
    public void OnHurt()
    {
        controller.ChangeState(controller.hurt);
    }
    public void OnExit() { }
}
