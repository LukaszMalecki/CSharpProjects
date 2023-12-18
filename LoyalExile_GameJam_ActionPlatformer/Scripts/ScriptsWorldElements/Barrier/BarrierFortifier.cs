using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierFortifier : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    public bool isReady = false;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isReady = true;
        //SetWaiting();

    }

    public void SetWaiting()
    {
        collider.enabled = false;
        spriteRenderer.color = new Color32(0xF8, 0xC8, 0x53, 125); //yellow - F8C853
    }

    public void SetActive()
    {
        collider.enabled = true;
        spriteRenderer.color = new Color32(0xF8, 0x5B, 0x53, 125); //red - F85B53
    }

    public void SetBroken()
    {
        collider.enabled = false;
        spriteRenderer.color = new Color32(0x53, 0xF8, 0x8F, 125); //green - 53F88F
    }


}
