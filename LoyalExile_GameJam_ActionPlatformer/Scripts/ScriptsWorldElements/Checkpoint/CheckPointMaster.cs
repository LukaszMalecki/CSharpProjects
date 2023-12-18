using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite checkpointActive;
    public Sprite checkpointInactive;

    public Checkpoint activeCheckpoint = null;

    public LevelState savedState;

    public LevelState currentState;
    void Start()
    {
        var checkpoints = GetComponentsInChildren<Checkpoint>();

        foreach (var checkpoint in checkpoints) 
        {
            if (activeCheckpoint == null && checkpoint.isActive)
            {
                activeCheckpoint = checkpoint;
                checkpoint.TurnOn();
            }
            else if( checkpoint.isActive)
            {
                checkpoint.TurnOff();
            }
        }
        //SaveStatus();
    }

    public void ActivateCheckpoint(Checkpoint checkpoint)
    {
        if(!checkpoint.isMajor)
        {
            SaveCurrentStatus(checkpoint);
            return;
        }
        if(checkpoint != activeCheckpoint) 
        {
            if( activeCheckpoint != null )
                activeCheckpoint.TurnOff();
            activeCheckpoint = checkpoint;
            activeCheckpoint.TurnOn();

            PlayCheckpointSound();
        }
        SaveStatus();
    }

    public void SaveStatus()
    {
        var tran = activeCheckpoint.GetComponent<Transform>();
        currentState.checkpointX = tran.position.x; 
        currentState.checkpointY = tran.position.y;
        currentState.SaveInto(savedState);
    }

    public void SaveCurrentStatus(Checkpoint checkpoint)
    {
        var tran = checkpoint.GetComponent<Transform>();
        currentState.checkpointX = tran.position.x;
        currentState.checkpointY = tran.position.y;
    }

    public void PlayCheckpointSound()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
