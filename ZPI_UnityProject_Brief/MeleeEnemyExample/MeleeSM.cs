using UnityEngine;

public class MeleeSM : EnemySpecific
{
    public MeleeRest RestState { get; private set; }
    public MeleePatrol PatrolState { get; private set; }
    public MeleeChase ChaseState { get; private set; }
    public MeleeAttack AttackState { get; private set; }
    public MeleeHurt HurtState { get; private set; }

    [Header("State times")]
    [SerializeField]
    private float restTime = 3f;
    [SerializeField]
    private float patrolTime = 7f;
    [SerializeField]
    private float chaseTime = 2f;
    [SerializeField]
    private float hurtTime = 0.3f;
    [SerializeField]
    private float reactionTime = 0.5f;
    [Header("Range")]
    [SerializeField]
    private float attackRange = 1.0f;
    [SerializeField]
    private float detectionRange = 1.0f;
    [SerializeField]
    [Header("Attack")]
    private float attackCooldown = 0.2f;

    private void Awake()
    {
        AwakeInitialize();
    }
    protected override void AwakeInitialize()
    {
        base.AwakeInitialize();

        RestState = new MeleeRest(enemySM, this);
        PatrolState = new MeleePatrol(enemySM, this);
        ChaseState = new MeleeChase(enemySM, this);
        AttackState = new MeleeAttack(enemySM, this);
        HurtState = new MeleeHurt(enemySM, this);

        currentState = RestState;
    }

    protected override void Initialize()
    {
        
    }
    protected override void RandomizeStateTimes()
    {
        RandomizeStateTime(ref restTime);
        RandomizeStateTime(ref patrolTime);
        RandomizeStateTime(ref chaseTime);
        RandomizeStateTime(ref hurtTime);
        RandomizeStateTime(ref reactionTime);
        RandomizeStateTime(ref attackCooldown);
    }
    public void TriggerAnimAttack()
    {
        SetSpell(GetRandomSpellIndex());
        anim.SetTrigger("Attack");
    }

    public float GetRestTime()
    {
        return restTime;
    }
    public float GetPatrolTime()
    {
        return patrolTime;
    }
    public float GetChaseTime()
    {
        return chaseTime;
    }
    public float GetHurtTime()
    {
        return hurtTime;
    }
    public float GetReactionTime()
    {
        return reactionTime;
    }
    public float GetAttackCooldown()
    {
        return attackCooldown;
    }    
    public float GetDetectionRange()
    {
        return detectionRange;
    }
    
    public void SetRestForced()
    {
        RestState.isRestForced = true;
    }
    public bool IsGoalInRange()
    {
        if(enemySM.GetGoalDynamic() == null)
        {
            return false;
        }
        return enemySM.IsInRangeXZ(detectionRange);
    }
    public bool IsGoalInAttackRange()
    {
        if (enemySM.GetGoalDynamic() == null)
        {
            return false;
        }
        return enemySM.IsInRangeXZ(attackRange);
    }

    public void Attack()
    {
        TriggerAnimAttack();
        SetAttackOngoing(true);
    }
    public void ForceRest()
    {
        SetRestForced();
        ChangeState(RestState);
    }
    public override void ForceChase()
    {
        ChangeState(ChaseState);
    }
}
