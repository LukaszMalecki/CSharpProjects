using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : IState
{
    private GoblinController controller;

    public float decayTime;
    private float decayTimeUsed = 0f;
    public BossHurt(GoblinController goblinController, float decayTime = 1f)
    {
        controller = goblinController;
        this.decayTime = decayTime;

    }
    public void OnEnter()
    {
        decayTimeUsed = 0f;
        controller.SetHorizontalVelocity(0f);

        if (!controller.IsAlive())
            decayTime = 3f;
    }
    public void UpdateState()
    {
        if (decayTimeUsed >= decayTime)
        {
            controller.ChangeState(controller.chase);
            return;
        }
        decayTimeUsed += Time.deltaTime;
    }
    public void OnHurt()
    {
        controller.ChangeState(controller.hurt);
    }
    public void OnExit() { }
}
