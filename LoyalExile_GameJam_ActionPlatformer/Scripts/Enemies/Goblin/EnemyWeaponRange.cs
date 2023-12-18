using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponRange : MonoBehaviour
{
    // Start is called before the first frame update

    private bool playerInRange = false;
    void Start()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = false;
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }
}
