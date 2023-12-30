using UnityEngine;

public class MeleePatrol : IState
{
    private StateName stateName;
    private EnemySM stateMachine;
    private MeleeSM  mainStateMachine;

    private float stateTime;
    private float stateTimePassed;

    public MeleePatrol(EnemySM stateMachine, MeleeSM enemySpecific)
    {
        stateName = StateName.Patrol;
        this.stateMachine = stateMachine;
        this. mainStateMachine = enemySpecific;
        this.stateTime = enemySpecific.GetPatrolTime();
    }
    public void OnEnter()
    {
        stateMachine.SetStatePatrol(false);
        stateTimePassed = 0f;
        this.stateTime = mainStateMachine.GetPatrolTime();

        mainStateMachine.SetAnimWalking(true);
    }
    public void UpdateState()
    {
        stateTimePassed += Time.deltaTime;

        if (mainStateMachine.IsGoalInRange())
        {
            mainStateMachine.ChangeState(mainStateMachine.ChaseState);
            return;
        }

        if (stateTimePassed >= stateTime)
        {
            mainStateMachine.ChangeState(mainStateMachine.RestState);
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
