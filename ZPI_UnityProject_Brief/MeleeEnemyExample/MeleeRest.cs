using UnityEngine;

public class MeleeRest : IState
{
    private StateName stateName;
    private EnemySM stateMachine;
    private MeleeSM  mainStateMachine;

    private float stateTime;
    private float stateTimePassed;
    public bool isRestForced;

    private float reactionTime;
    private float reactionTimePassed;

    public MeleeRest(EnemySM stateMachine, MeleeSM enemySpecific)
    {
        stateName = StateName.Rest;
        this.stateMachine = stateMachine;
        this.mainStateMachine = enemySpecific;
        this.stateTime = enemySpecific.GetRestTime();
        isRestForced = false;
    }
    public void OnEnter()
    {
        stateMachine.SetStateStay();
        stateTimePassed = 0f;
        this.stateTime = mainStateMachine.GetRestTime();
        reactionTime = mainStateMachine.GetReactionTime();
        reactionTimePassed = 0f;

        mainStateMachine.SetAnimWalking(false);
    }
    public void UpdateState()
    {
        stateTimePassed += Time.deltaTime;

        if(mainStateMachine.IsGoalInRange())
        {
            reactionTimePassed += Time.deltaTime;
            if (!isRestForced && reactionTimePassed >= reactionTime)
            {
                mainStateMachine.ChangeState(mainStateMachine.ChaseState);
                return;
            }
        }
        if(stateTimePassed >= stateTime)
        {
            mainStateMachine.ChangeState(mainStateMachine.PatrolState);
            return;
        }

    }

    public void OnHurt()
    {
        mainStateMachine.ChangeState(mainStateMachine.HurtState);
    }
    public void OnExit()
    {
        isRestForced = false;
    }
    public StateName GetStateName()
    {
        return stateName;
    }
}
