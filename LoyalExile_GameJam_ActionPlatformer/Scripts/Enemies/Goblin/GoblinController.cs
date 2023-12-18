using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoblinController : MonoBehaviour
{
    Rigidbody2D mainBody;

    public float horizontalSpeed = 4.5f;
    public float verticalSpeed = 7f;
    private Animator anim;
    private bool flipped = false;

    private EnemyLife enemyLife;
    private EnemySword enemySword;
    private EnemyWeaponRange enemyWeaponRange;

    public float attackCooldown = 1f;

    public float detectionDistance = 7f;

    public float attackDelayTime = 0.3f;

    public float decayTime = 1f;

    public IState currentState = null;

    //For boss fights
    public bool noFlinch = false;
    public bool isBoss = false;

    public Transform bossTarget = null;

    public int attacksWithoutAoeMax = 4;
    public int attacksWithoutAoeLeft = 2;
    public bool nextAttackAoe = false;

    public float aoeAttackTime = 2f;

    public float bossRestTime = 3f;

    public IState attacking;
    public IState chase;
    public IState hurt;
    public IState patrol;
    public IState rest;

    private SpriteRenderer spriteRenderer;

    public AoeMaster aoeMaster = null;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("attackEnd", true);

        enemyLife = GetComponentInChildren<EnemyLife>();
        enemySword = GetComponentInChildren<EnemySword>();
        enemyWeaponRange = GetComponentInChildren<EnemyWeaponRange>();

        if( noFlinch ) 
        {
            anim.SetBool("noFlinch", true);
        }

        attacking = new Attacking(this, attackCooldown, attackDelayTime);
        chase = new Chase(this);
        hurt = new Hurt(this, decayTime);
        patrol = new Patrol(this);
        rest = new Rest(this);

        currentState = patrol;

        if( isBoss)
        {
            //aoeMaster = GetComponentInChildren<AoeMaster>();
            attacking = new BossAttacking(this, attackCooldown, attackDelayTime, aoeAttackTime);
            chase = new BossChase(this);
            hurt = new BossHurt(this, decayTime);
            patrol = new BossPatrol(this);
            rest = new BossRest(this, bossRestTime);

            currentState = chase;
        }

        if (mainBody.velocity.x < 0)
        {
            flipped = true;
        }
        if (mainBody.velocity.x > 0)
        {
            flipped = false;
        }
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));


        //mainBody.velocity = new Vector3(1f, 0f, 0f);
    }

    //float where = 1f;
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(mainBody.velocity.x));


        /*if (mainBody.velocity.x < 0)
        {
            flipped = true;
        }
        if (mainBody.velocity.x > 0)
        {
            flipped = false;
        }
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));*/

        currentState.UpdateState();

        /*
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            //mainBody.velocity = new Vector3(1f*where, 0f, 0f);
        }
        else
        {
            Attack();
            //where *= -1f;
            //mainBody.velocity = new Vector3(mainBody.velocity.x * (-1f), 0f, 0f);
            attackCooldown = attackCooldownMax;
        }*/



    }

    public void ChangeState(IState state)
    {
        currentState.OnExit();
        currentState = state;
        currentState.OnEnter();
    }

    public void PrepareAoe()
    {
        anim.SetTrigger("aoePrepare");
        aoeMaster.SetState(AoeMasterState.Preparing);
    }

    public void StartAoe()
    {
        anim.SetTrigger("aoeAttack");
        aoeMaster.SetState(AoeMasterState.Attacking);
    }

    public void EndAoe()
    {
        anim.SetTrigger("aoeEnd");
        aoeMaster.SetState(AoeMasterState.Waiting);
    }



    public void TakeDamage()
    {
        if( !noFlinch)
            currentState.OnHurt();
        else
        {
            spriteRenderer.enabled = false;
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
    }



    public bool IsAlive()
    {
        return enemyLife.alive;
    }

    public void Death()
    {
        //hurt.decayTime = 3f;
        currentState.OnHurt();

        if( isBoss)
        {
            StartCoroutine(EndingScene());
        }
    }

    private IEnumerator EndingScene()
    {
        //musicControl.absoluteMute();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Ending_KingsDeath");

        //trophyAnimator.SetInteger("GameState", 0);
    }

    public void TurnAround()
    {
        flipped = !flipped;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        /*float interval = 0.5f;
        float velocity = GetHorizontalVelocity();
        if (Mathf.Abs(velocity) < interval)
        {
            SetHorizontalVelocity(-velocity);
        }
        else if (flipped)
        {
            SetHorizontalVelocity(interval);
        }
        else
        {
            SetHorizontalVelocity(-interval);
        }*/
    }

    public void RunForward()
    {
        if (flipped)
        {
            SetHorizontalVelocity(-horizontalSpeed);
        }
        else
        {
            SetHorizontalVelocity(horizontalSpeed);
        }
    }

    public void WalkForward()
    {
        if (flipped)
        {
            SetHorizontalVelocity(-horizontalSpeed / 2f);
        }
        else
        {
            SetHorizontalVelocity(horizontalSpeed / 2f);
        }
    }

    public float GetHorizontalVelocity()
    {
        return mainBody.velocity.x;
    }

    public void SetHorizontalVelocity(float horVelocity)
    {
        mainBody.velocity = new Vector2(horVelocity, mainBody.velocity.y);
    }

    public bool CanSeePlayer()
    {
        float distance = detectionDistance;
        if (flipped)
        {
            distance *= -1f;
        }
        Transform castPoint = mainBody.transform;
        Vector2 endPos = castPoint.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Solid"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("PlayerHitbox"))
            {
                return true;
            }
        }
        return false;
    }

    public bool NeedTurnAroundForTarget()
    {
        bool isOnLeft = bossTarget.position.x < mainBody.position.x;

        return isOnLeft != flipped;
    }

    public bool IsPlayerCloseBy()
    {
        return Mathf.Abs(bossTarget.position.x - mainBody.position.x) < 0.5f;

    }

    public bool ObstacleInWay()
    {
        float distance = 0.7f;
        if (flipped)
        {
            distance *= -1f;
        }
        Transform castPoint = mainBody.transform;
        Vector2 endPos = castPoint.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Solid"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Solid") || hit.collider.gameObject.CompareTag("Barrier") )
            {
                return true;
            }
        }

        hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("EnemyStopper"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("EnemyStopper"))
            {
                return true;
            }
        }

        return false;
    }

    public bool PlayerInWeaponRange()
    {
        return enemyWeaponRange.IsPlayerInRange();
    }


    public void SetAttackTriggerOn()
    {
        SetAttackTrigger(true);
    }

    public void SetAttackTriggerOff()
    {
        SetAttackTrigger(false);
    }

    public void SetAttackTrigger(bool state)
    {
        enemySword.SetSwordHitbox(state);
    }

    public void Attack()
    {
        if (anim.GetBool("attackEnd"))
        {
            anim.SetTrigger("attack");
            anim.SetBool("attackEnd", false);
        }
    }

    public bool HasAttackEnded()
    {
        return anim.GetBool("attackEnd");
    }

}
