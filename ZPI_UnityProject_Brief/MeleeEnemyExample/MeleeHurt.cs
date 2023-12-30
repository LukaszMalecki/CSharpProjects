public class MeleeHurt : IState
{
    private StateName stateName;
    private EnemySM stateMachine;
    private MeleeSM mainStateMachine;

    private float stateTime;
    private float stateTimePassed;

    public MeleeHurt(EnemySM stateMachine, MeleeSM enemySpecific)
    {
        stateName = StateName.Hurt;
        this.stateMachine = stateMachine;
        this.mainStateMachine = enemySpecific;
        this.stateTime = mainStateMachine.GetHurtTime();
    }
    public void OnEnter()
    {
        stateMachine.SetStateStay();
        stateTimePassed = 0f;
        this.stateTime = mainStateMachine.GetHurtTime();

        mainStateMachine.SetAnimWalking(false);
    }
    public void UpdateState()
    {
        stateTimePassed += stateTime;
        if (stateTimePassed >= stateTime)
        {
            mainStateMachine.ChangeState(mainStateMachine.ChaseState);
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
