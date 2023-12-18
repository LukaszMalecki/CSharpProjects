using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacking : IState
{
    private GoblinController controller;

    public float attackCooldown;
    private float timeLeft;

    private float attackDelay;
    private float attackDelayLeft;

    private bool attacking = false;


    public BossAttacking(GoblinController goblinController, float attackCooldown = 3f, float attackDelay = 1f, float aoeAttackTime = 4f)
    {
        controller = goblinController;
        this.attackCooldown = attackCooldown;
        timeLeft = attackCooldown;
        this.attackDelay = attackDelay;
        attackDelayLeft = attackDelay;
    }
    public void OnEnter()
    {
        attacking = false;
        if (controller.nextAttackAoe)
        {
            timeLeft = attackCooldown;
            controller.PrepareAoe();
        }
        else
        {
            attackDelayLeft = attackDelay;
            timeLeft = 0f;
        }
        //MakeAttack();
        float velocity = controller.GetHorizontalVelocity();
        if (velocity > 0.1f)
        {
            controller.SetHorizontalVelocity(0);
        }
    }
    public void UpdateState()
    {
        if (!controller.nextAttackAoe)
        {
            if (!attacking)
            {
                attackDelayLeft -= Time.deltaTime;
                MakeAttack();
                return;
            }
            else if (controller.HasAttackEnded())
            {
                controller.ChangeState(controller.rest);
                return;
            }
        }
        else
        {
            
            if (!attacking)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0)
                {
                    controller.StartAoe();
                    timeLeft = controller.aoeAttackTime;
                    attacking = true;
                }
            }
            else
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0)
                {
                    controller.EndAoe();
                    timeLeft = controller.aoeAttackTime;
                    controller.ChangeState(controller.rest);
                }
            }
            
            
        }
    }
    public void OnHurt()
    {
        controller.ChangeState(controller.hurt);
    }
    public void OnExit()
    {
        if(controller.nextAttackAoe)
        {
            controller.attacksWithoutAoeLeft = Random.Range(1, controller.attacksWithoutAoeMax + 1);
            controller.nextAttackAoe = false;
        }
        else
        {
            controller.attacksWithoutAoeLeft--;
            if(controller.attacksWithoutAoeLeft <= 0)
            {
                controller.nextAttackAoe = true;
            }
        }
    }

    private void MakeAttack()
    {
        if (timeLeft <= 0f && attackDelayLeft <= 0f)
        {
            controller.Attack();
            timeLeft = attackCooldown;
            attackDelayLeft = attackDelay;
            attacking = true;

        }


    }
}
