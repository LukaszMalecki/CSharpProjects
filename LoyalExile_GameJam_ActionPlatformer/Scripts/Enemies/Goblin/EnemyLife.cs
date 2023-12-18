using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float hitPoints = 30f;
    private Animator anim;
    private Collider2D hitbox;

    public float invincibilityTime = 0f;
    public float invincibilityTimeMax = 1.3f;
    // Start is called before the first frame update
    public bool alive = true;

    private BarrierMaster barrierMaster;

    private GoblinController goblinController;
    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        anim = GetComponentInParent<Animator>();

        goblinController = GetComponentInParent<GoblinController>();

        
        barrierMaster = this.transform.parent.gameObject.GetComponentInParent<BarrierMaster>();

        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "PlayerAttack" && invincibilityTime <= 0)
        {
            //collision.gameObject.SendMessage("ApplyDamage", 10);
            Debug.Log("Damage dealt to Goblin");
            TakeDamage(10f);
            //invincibilityTime = invincibilityTimeMax;
            //flashTime = flashTimeMax;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack" && invincibilityTime <= 0)
        {
            //collision.gameObject.SendMessage("ApplyDamage", 10);
            if (invincibilityTime <= 0)
            {
                Debug.Log("Damage dealt to Goblin");
                TakeDamage(10f);
            }
            //invincibilityTime = invincibilityTimeMax;
            //flashTime = flashTimeMax;
        }
    }*/

    public void TakeDamage(float damage = 10f)
    {
        hitPoints -= damage;

        goblinController.TakeDamage();

        if( hitPoints <= 0f )
        {
            alive = false;
            hitbox.enabled = false;
            goblinController.Death();
            anim.SetTrigger("death");
            
            if( barrierMaster != null ) 
            {
                barrierMaster.SignalDeath();
            }

            StartCoroutine(Decay(1.5f));
            

        }
        else
        {
            if( !goblinController.noFlinch)
                anim.SetTrigger("takeDamage");
        }

    }

    public IEnumerator Decay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(goblinController.gameObject);
    }
}
