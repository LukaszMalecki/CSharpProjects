using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    public float hitPoints = 3f;
    private Animator anim;
    private Collider2D hitbox;

    public bool alive = true;

    public float invincibilityTime = 0f;
    public float invincibilityTimeMax = 1.3f;

    public float flashTime = 0f;
    public float flashTimeMax = 0.1f;

    //public StartParamsPlayer paramsPlayer;

    private PlayerMovement playerMove;
    private SpriteRenderer spriteRenderer;

    public int damagingColliders = 0;
    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        anim = GetComponentInParent<Animator>();
        //hitPoints = paramsPlayer.healthPoints;
        playerMove = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( damagingColliders > 0 && invincibilityTime <= 0f)
        {
            Debug.Log("Damage dealt");
            TakeDamage();
            if (hitPoints > 0f)
            {
                invincibilityTime = invincibilityTimeMax;
                flashTime = flashTimeMax;
            }
        }

        if (invincibilityTime > 0f)
        {
            invincibilityTime -= Time.deltaTime;

            if (flashTime > 0f)
            {
                flashTime -= Time.deltaTime;
            }
            else
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                flashTime = flashTimeMax;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" || collision.gameObject.tag == "EnemyHitbox")
        {
            damagingColliders++;
        }
        else if (collision.gameObject.tag == "Collectable")
        {
            StartCoroutine(PickupReward());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" || collision.gameObject.tag == "EnemyHitbox")
        {
            damagingColliders--;
        }
    }

    /*void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" || collision.gameObject.tag == "EnemyHitbox")
        {
            if(invincibilityTime <= 0f)
            {
                TakeDamage();
                invincibilityTime = invincibilityTimeMax;
                flashTime = flashTimeMax;
            }
        }
    }*/

    public IEnumerator PickupReward()
    {
        yield return new WaitForSeconds(0.2f);
        hitPoints = playerMove.levelCurrent.maxHp;
        playerMove.jumpLimit = playerMove.levelCurrent.jumpCount;
        playerMove.currentJumpLimit = playerMove.levelCurrent.jumpCount;
    }

    public void TakeDamageKillZone()
    {
        TakeDamage();
        //playerMove.levelSaved.SaveIntoAfterKillZone(playerMove.levelCurrent);
        if(alive)
            playerMove.RespawnCheckpoint();
    }

    public void TakeDamage()
    {
        hitPoints -= 1f;
        if(hitPoints <= 0f)
        {
            alive = false;
            anim.SetTrigger("death");
            hitbox.enabled = false;

            StartCoroutine(Respawn());

        }
        
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        playerMove.Reload();
    }

    /*void OnTriggerStay2D(Collider2D collision)
    {
        if ( (collision.gameObject.tag == "EnemyAttack"  || collision.gameObject.tag == "EnemyHitbox") && invincibilityTime <= 0)
        {
            //collision.gameObject.SendMessage("ApplyDamage", 10);
            Debug.Log("Damage dealt");
            invincibilityTime = invincibilityTimeMax;
            flashTime = flashTimeMax;
        }
    }*/



}
