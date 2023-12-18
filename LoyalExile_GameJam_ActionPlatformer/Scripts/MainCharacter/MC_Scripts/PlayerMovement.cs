using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D mainBody;

    public float horizontalSpeed = 4.5f;
    public float verticalSpeed = 7f;
    private Animator anim;
    private GroundTrigger groundTrigger;
    private PlayerLife playerLife;
    private PlayerSword playerSword;

    private bool flipped = false;

    public int jumpLimit = 1;
    public int currentJumpLimit = 1;
    public float jumpResetDelay = 0.1f;

    public float attackDelay = 0.5f;
    private float attackDelayLeft;

    //public StartParamsPlayer paramsPlayer;

    public LevelState levelStart;
    public LevelState levelCurrent;
    public LevelState levelSaved;
    void Start()
    {
        //Zapewnia, aby poprawnie dzialal reload sceny w ramach powrotu do checkpointu
        if (levelCurrent.isNewGame)
        {
            levelStart.SaveInto(levelCurrent);
            levelStart.SaveInto(levelSaved);
        }
        else
            levelSaved.SaveInto(levelCurrent);
        /*levelCurrent.isNewGame = false;
        levelStart.isNewGame = false;
        levelSaved.isNewGame = false;*/

        mainBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("attackEnd", true);
        
        jumpLimit = levelCurrent.jumpCount;
        currentJumpLimit = jumpLimit;

        

        groundTrigger = GetComponentInChildren<GroundTrigger>();
        GetComponent<Transform>().position = new Vector3(levelCurrent.checkpointX, levelCurrent.checkpointY, 0f);
        playerLife = GetComponentInChildren<PlayerLife>();
        playerSword = GetComponentInChildren<PlayerSword>();

        playerLife.hitPoints = levelCurrent.maxHp;

        attackDelayLeft = attackDelay;


    }

    // Update is called once per frame
    void Update()
    {
        if (attackDelayLeft >= 0)
        { attackDelayLeft -= Time.deltaTime; }
        bool noHor = true;
        if (anim.GetBool("attackEnd") && playerLife.alive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentJumpLimit > 0)
            {
                mainBody.velocity = new Vector3(0, verticalSpeed, 0);
                if( currentJumpLimit == jumpLimit ) 
                {
                    JumpControl();
                }
                currentJumpLimit -= 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                mainBody.velocity = new Vector3(-horizontalSpeed, mainBody.velocity.y, 0);
                noHor = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                mainBody.velocity = new Vector3(horizontalSpeed, mainBody.velocity.y, 0);
                noHor = false;
            }


            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
            }

        }
        if (noHor)
        {
            mainBody.velocity = new Vector3(0, mainBody.velocity.y, 0);
        }
        anim.SetFloat("speed", Mathf.Abs(mainBody.velocity.x));
        anim.SetFloat("speedVer", mainBody.velocity.y);

        if (mainBody.velocity.x < 0)
        {
            flipped = true;
        }
        if (mainBody.velocity.x > 0)
        {
            flipped = false;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

    }

    void Attack()
    {
        if (Mathf.Abs(mainBody.velocity.y) < 0.01f && playerLife.alive && attackDelayLeft <= 0)
        {
            anim.SetTrigger("attack");
            anim.SetBool("attackEnd", false);
            attackDelayLeft = attackDelay;
        }
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
        playerSword.SetSwordHitbox(state);
    }

    

    public void ResetJump()
    {
        currentJumpLimit = jumpLimit;
        //StartCoroutine(groundTrigger.TurnCollisionOn(jumpResetDelay));
    }

    public void JumpControl()
    {
        StartCoroutine(groundTrigger.TurnCollisionOn(jumpResetDelay));
    }

    public void Reload()
    {
        levelCurrent.isNewGame = false;
        levelStart.isNewGame = false;
        levelSaved.isNewGame = false;
        SceneManager.LoadScene(levelCurrent.SceneName);
    }

    public void TurnOffGame()
    {
        levelCurrent.isNewGame = true;
        levelStart.isNewGame = true;
        levelSaved.isNewGame = true;

        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void RespawnCheckpoint()
    {
        GetComponent<Transform>().position = new Vector3(levelCurrent.checkpointX, levelCurrent.checkpointY, 0f);
    }

}
