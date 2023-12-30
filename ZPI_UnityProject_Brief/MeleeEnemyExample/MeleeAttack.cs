using UnityEngine;

public class MeleeAttack : IState
{
    private StateName stateName;
    private EnemySM stateMachine;
    private MeleeSM  mainStateMachine;

    private float stateTime;
    private float stateTimePassed;

    private bool IsAttacking()
    {
        return mainStateMachine.GetAttackOngoing();
    }

    public MeleeAttack(EnemySM stateMachine, MeleeSM enemySpecific)
    {
        stateName = StateName.Attack;
        this.stateMachine = stateMachine;
        this. mainStateMachine = enemySpecific;
        this.stateTime = enemySpecific.GetAttackCooldown();
    }
    public void OnEnter()
    {
        stateMachine.SetStateGaze();
        stateTimePassed = stateTime;
        this.stateTime = mainStateMachine.GetAttackCooldown();

        mainStateMachine.SetAnimWalking(false);
    }

    public void UpdateState()
    {
        if (IsAttacking())
        {
            return;
        }
        stateTimePassed += Time.deltaTime;

        if (!mainStateMachine.IsGoalInAttackRange())
        {
            mainStateMachine.ChangeState(mainStateMachine.ChaseState);
            return;
        }

        if (stateTimePassed >= stateTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        mainStateMachine.Attack();
        stateTimePassed = 0f;
    }
    public void OnHurt()
    {
        mainStateMachine.ChangeState(mainStateMachine.HurtState);
    }
    public void OnExit()
    {

    }
    public StateName GetStateName()
    {
        return stateName;
    }
}
