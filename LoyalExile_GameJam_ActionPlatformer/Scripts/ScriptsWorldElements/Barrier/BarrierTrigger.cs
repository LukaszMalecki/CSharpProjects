using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierTrigger : MonoBehaviour
{
    public Collider2D[] hitbox;
    private BarrierMaster barrierMaster;
    public bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponents<Collider2D>();
        barrierMaster = GetComponentInParent<BarrierMaster>();
        isReady = true;
    }


    public void Enable()
    {
        foreach (Collider2D c in hitbox) 
        {
            c.enabled = true;
        }
    }

    public void Disable()
    {
        foreach (Collider2D c in hitbox)
        {
            c.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "PlayerHitbox")
        {
            barrierMaster.SignalIntruder();
        }
    }
}
