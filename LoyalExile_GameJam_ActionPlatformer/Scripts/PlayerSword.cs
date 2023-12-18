using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D swordHitbox;
    void Start()
    {
        swordHitbox = GetComponent<Collider2D>();
        swordHitbox.enabled = false;
    }

    public void SetSwordHitbox(bool state)
    {
        swordHitbox.enabled = state;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
