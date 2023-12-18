using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : IState
{
    private GoblinController controller;

    private float waitingTime = 4f;
    private float waitingTimeUsed = 0f;
    public Rest(GoblinController goblinController, float waitingTime = 4f)
    {
        controller = goblinController;
        this.waitingTime = waitingTime;
    }
    public void OnEnter() 
    {
        waitingTimeUsed = 0f;
    }
    public void UpdateState() 
    {
        if (ReactToPlayer())
            return;

        if (waitingTimeUsed > waitingTime)
        {
            controller.ChangeState(controller.patrol);
            return;
        }
        waitingTimeUsed += Time.deltaTime;
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
