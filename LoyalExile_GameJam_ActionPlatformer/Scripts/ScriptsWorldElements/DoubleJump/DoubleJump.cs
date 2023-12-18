using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleJump : MonoBehaviour
{
    public LevelState levelStart;
    public LevelState levelCurrent;
    public LevelState levelSaved;

    public int arrayId = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!levelCurrent.isNewGame)
        {
            if (levelSaved.jumpUpsPicked[arrayId])
                Destroy();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            levelCurrent.isNewGame = false;
            levelSaved.isNewGame = false;
            levelCurrent.jumpCount++;
            levelCurrent.jumpUpsPicked[arrayId] = true;
            levelSaved.jumpCount++;
            levelSaved.jumpUpsPicked[arrayId] = true;
            levelSaved.SceneName = "Level2";
            levelCurrent.SceneName = "Level2";
            levelSaved.checkpointX = 0f;
            levelSaved.checkpointY = -1f;
            levelCurrent.checkpointX = 0f;
            levelCurrent.checkpointY = -1f;
            //collision.gameObject.GetComponent<PlayerLife>
            SceneManager.LoadScene("Level2");

            Destroy();
        }
    }

    /*private IEnumerator NextScene()
    {
        //musicControl.absoluteMute();
        //yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level2");

        //trophyAnimator.SetInteger("GameState", 0);
    }*/

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
