using UnityEngine;

public class MeleeChase : IState
{
    private StateName stateName;
    private EnemySM stateMachine;
    private MeleeSM mainStateMachine;

    private float stateTime;
    private float stateTimePassed;

    public MeleeChase(EnemySM stateMachine, MeleeSM enemySpecific)
    {
        stateName = StateName.Chase;
        this.stateMachine = stateMachine;
        this.mainStateMachine = enemySpecific;
        this.stateTime = enemySpecific.GetChaseTime();
    }
    public void OnEnter()
    {
        stateMachine.SetStateFollow();
        stateTimePassed = 0f;
        this.stateTime = mainStateMachine.GetChaseTime();

        mainStateMachine.SetAnimWalking(true);
    }
    public void UpdateState()
    {

        if (!mainStateMachine.IsGoalInRange())
        {
            stateTimePassed += Time.deltaTime;
        }
        else
        {
            stateTimePassed = 0f;
        }
        if(stateTimePassed >= stateTime)
        {
            mainStateMachine.ChangeState(mainStateMachine.PatrolState);
            return;
        }

        if (mainStateMachine.IsGoalInAttackRange())
        {
            mainStateMachine.ChangeState(mainStateMachine.AttackState);
            return;
        }
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
