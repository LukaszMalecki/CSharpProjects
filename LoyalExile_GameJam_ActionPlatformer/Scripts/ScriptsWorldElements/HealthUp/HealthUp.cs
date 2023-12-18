using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelState levelStart;
    public LevelState levelCurrent;
    public LevelState levelSaved;

    public int arrayId = 0;

    void Start()
    {
        if (!levelCurrent.isNewGame)
        {
            if (levelSaved.healthUpsPicked[arrayId])
                Destroy();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            levelCurrent.maxHp++;
            levelCurrent.healthUpsPicked[arrayId] = true;
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
