
public interface IState
{
    public void OnEnter();
    public void UpdateState();
    public void OnHurt();
    public void OnExit();
    public StateName GetStateName();
}

public enum StateName
{
    Freeze, Stay, Move, Patrol, Follow, Gaze, Rest, Attack, Chase, Hurt, Flee, Prepare, Focus, Rage
}