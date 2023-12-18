using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    public string SceneName = "Level1";
    public bool[] BarriersBroken = new bool[10];

    public bool[] healthUpsPicked = new bool[5]; //might be 2

    public bool[] jumpUpsPicked = new bool[3]; //there will be 1 tho

    public bool cloverPicked = false;

    public float checkpointX;
    public float checkpointY;

    public float defaultCameraScale = 5f;

    public float maxHp = 3f;
    public int jumpCount = 1;

    public bool isNewGame = true;

    public void SaveInto(LevelState otherState)
    {
        otherState.SceneName = SceneName;
        for( int i = 0; i < BarriersBroken.Length; i++ ) 
        {
            otherState.BarriersBroken[i] = BarriersBroken[i];
        }

        for( int i = 0;i < healthUpsPicked.Length; i++ ) 
        {
            otherState.healthUpsPicked[i] = healthUpsPicked[i];
        }

        for( int i = 0 ; i < jumpUpsPicked.Length ; i++ ) 
        {
            otherState.jumpUpsPicked[i] = jumpUpsPicked[i];
        }

        otherState.cloverPicked = cloverPicked;
        otherState.checkpointX = checkpointX;
        otherState.checkpointY = checkpointY;
        otherState.defaultCameraScale = defaultCameraScale;
        otherState.maxHp = maxHp;
        otherState.jumpCount = jumpCount;
        otherState.isNewGame = isNewGame;
    }
    //doesn't reset barriers
    /*public void SaveIntoAfterKillZone(LevelState otherState)
    {
        for (int i = 0; i < healthUpsPicked.Length; i++)
        {
            otherState.healthUpsPicked[i] = healthUpsPicked[i];
        }

        for (int i = 0; i < jumpUpsPicked.Length; i++)
        {
            otherState.jumpUpsPicked[i] = jumpUpsPicked[i];
        }
        otherState.cloverPicked = cloverPicked;
        otherState.maxHp = maxHp;
        otherState.jumpCount = jumpCount;
    }*/
}
