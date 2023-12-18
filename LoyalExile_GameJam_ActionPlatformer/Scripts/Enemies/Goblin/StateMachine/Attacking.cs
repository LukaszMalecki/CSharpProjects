using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : IState
{
    private GoblinController controller;

    public float attackCooldown;
    private float timeLeft;

    private float attackDelay;
    private float attackDelayLeft;

    public Attacking(GoblinController goblinController, float attackCooldown = 1f, float attackDelay = 0.3f) 
    {
        controller = goblinController;
        this.attackCooldown = attackCooldown;
        timeLeft = attackCooldown;
        this.attackDelay = attackDelay;
        attackDelayLeft = attackDelay;
    }
    public void OnEnter() 
    {
        timeLeft = 0;
        attackDelayLeft = attackDelay;
        MakeAttack();
        float velocity = controller.GetHorizontalVelocity();
        if (velocity > 0.1f) 
        {
            controller.SetHorizontalVelocity(velocity / 5f);
        }
    }
    public void UpdateState() 
    {
        if (timeLeft <= 0) 
        {
            attackDelayLeft -= Time.deltaTime;
            MakeAttack();
            return;
        }
        if(controller.PlayerInWeaponRange())
        {
            timeLeft -= Time.deltaTime;
            MakeAttack();
        }
        else
        {
            controller.ChangeState(controller.chase);
        }
    }
    public void OnHurt() 
    {
        controller.ChangeState(controller.hurt);
    }
    public void OnExit() 
    {
    }

    private void MakeAttack()
    {
        if(timeLeft <= 0f && attackDelayLeft <= 0f) 
        {
            controller.Attack();
            timeLeft = attackCooldown;
            attackDelayLeft = attackDelay;

        }
        

    }
}
