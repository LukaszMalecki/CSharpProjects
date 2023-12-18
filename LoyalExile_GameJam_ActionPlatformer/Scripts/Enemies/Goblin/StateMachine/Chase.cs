using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

public class Chase : IState
{
    private GoblinController controller;

    private float waitingTime = 3f;
    private float waitingTimeUsed = 0f;

    private float turnAroundTime = 1f;
    private float turnAroundTimeUsed = 0f;
    public Chase(GoblinController goblinController)
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
        if (controller.PlayerInWeaponRange()) 
        {
            controller.ChangeState(controller.attacking);
            return;
        }
        if (!controller.CanSeePlayer())
        {
            waitingTimeUsed += Time.deltaTime;
            turnAroundTimeUsed += Time.deltaTime;
            Waiting();
        }
        else if(controller.ObstacleInWay())
        {
            waitingTimeUsed += Time.deltaTime;
            Waiting();
        }
        else
        {
            waitingTimeUsed = 0f;
            turnAroundTimeUsed = 0f;
            controller.RunForward();
        }
    }

    public void Waiting()
    {
        if(waitingTimeUsed >= waitingTime)
        {
            controller.ChangeState(controller.patrol);
            return;
        }
        if(turnAroundTimeUsed >= turnAroundTime)
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
