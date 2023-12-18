using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : IState
{
    private GoblinController controller;

    public float walkingTime = 6f;
    private float walkingTimeUsed = 0f;
    public BossPatrol(GoblinController goblinController)
    {
        controller = goblinController;
    }
    public void OnEnter()
    {
        if (ReactToPlayer())
            return;
        walkingTimeUsed = 0f;
        if (Random.Range(0, 2) == 0)
            controller.TurnAround();
    }
    public void UpdateState()
    {
        if (ReactToPlayer())
            return;

        if (walkingTimeUsed > walkingTime)
        {
            controller.ChangeState(controller.rest);
            return;
        }
        walkingTimeUsed += Time.deltaTime;

        if (controller.ObstacleInWay())
        {
            controller.TurnAround();
        }
        else
        {
            controller.WalkForward();
        }

    }

    private bool ReactToPlayer()
    {
        if (controller.CanSeePlayer())
        {
            controller.ChangeState(controller.chase);
            return true;
        }
        return false;
    }
    public void OnHurt()
    {
        controller.ChangeState(controller.hurt);
    }
    public void OnExit() { }
}
