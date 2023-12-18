using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private Collider2D collider;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Solid") 
        {
            player.ResetJump();
            collider.enabled = false;
        }
    }

    public IEnumerator TurnCollisionOn(float delay) 
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;

    }
}
