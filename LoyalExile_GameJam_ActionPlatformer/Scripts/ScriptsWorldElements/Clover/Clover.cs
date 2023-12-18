using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover : MonoBehaviour
{
    public LevelState levelStart;
    public LevelState levelCurrent;
    public LevelState levelSaved;

    // Start is called before the first frame update
    void Start()
    {
        if (!levelCurrent.isNewGame)
        {
            if (levelSaved.cloverPicked)
                Destroy();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            levelCurrent.cloverPicked = true;
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
