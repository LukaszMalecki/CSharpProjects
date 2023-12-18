using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CheckPointMaster master;

    public bool isActive = false;

    public bool isMajor = true;

    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        master = GetComponentInParent<CheckPointMaster>();

        if (isMajor)
        {
            if (!isActive)
                ChangeSprite(master.checkpointInactive);
            else
            {
                ChangeSprite(master.checkpointActive);
            }
        }

        col = GetComponent<Collider2D>();
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void TurnOff()
    {
        if (isActive)
            ChangeSprite(master.checkpointInactive);
        isActive = false;
    }

    public void TurnOn()
    {
        if (!isActive)
            ChangeSprite(master.checkpointActive);
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            master.ActivateCheckpoint(this);
        }
    }
}
