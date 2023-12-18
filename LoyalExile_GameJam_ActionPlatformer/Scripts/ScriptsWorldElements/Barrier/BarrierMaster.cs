using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMaster : MonoBehaviour
{
    public LevelState levelStart;
    public LevelState levelCurrent;
    public LevelState levelSaved;

    public int arrayId = 0;

    public BarrierState barrierState;

    public BarrierTrigger[] barrierTriggers;
    public BarrierFortifier[] barrierFortifiers;

    public int enemiesLeft = 2;

    private bool allReady = false;
    // Start is called before the first frame update
    void Start()
    {
        //barrierState = BarrierState.Waiting;

        barrierTriggers = GetComponentsInChildren<BarrierTrigger>();
        barrierFortifiers = GetComponentsInChildren<BarrierFortifier>();
        if(AllReady())
        {
            GameStateChange();
        }
        /*if( levelCurrent.isNewGame)
        {
            ChangeState(BarrierState.Waiting);
        }
        else
        {
            if (levelSaved.BarriersBroken[arrayId])
                ChangeState(BarrierState.Broken);
            else
                ChangeState(BarrierState.Waiting);
        }*/
    }

    private void Update()
    {
        if(!allReady)
        {
            if (AllReady()) 
            {
                GameStateChange();
            }
        }
    }

    private void GameStateChange()
    {
        if (levelCurrent.isNewGame)
        {
            ChangeState(BarrierState.Waiting);
        }
        else
        {
            if (levelSaved.BarriersBroken[arrayId])
                ChangeState(BarrierState.Broken);
            else
                ChangeState(BarrierState.Waiting);
        }
    }

    public bool AllReady()
    {
        foreach (BarrierTrigger trigger in barrierTriggers)
        {
            if (!trigger.isReady)
                return false;
        }

        foreach (BarrierFortifier fortifier in barrierFortifiers)
        {
            if (!fortifier.isReady)
                return false;
        }
        allReady = true;
        return true;
    }
    public void CheckState()
    {
        
        if( enemiesLeft <= 0 && barrierState != BarrierState.Broken ) 
        {
            ChangeState(BarrierState.Broken);
        }
    }

    public void ChangeState(BarrierState state)
    {
        barrierState = state;
        if( state == BarrierState.Broken )
        {
            foreach( BarrierTrigger trigger in barrierTriggers )
            {
                trigger.Disable();
            }

            foreach( BarrierFortifier fortifier in barrierFortifiers )
            {
                fortifier.SetBroken();
            }
            levelCurrent.BarriersBroken[arrayId] = true;

            return;
        }
        if (state == BarrierState.Active)
        {
            foreach (BarrierTrigger trigger in barrierTriggers)
            {
                trigger.Disable();
            }

            foreach (BarrierFortifier fortifier in barrierFortifiers)
            {
                fortifier.SetActive();
            }

            return;
        }
        if (state == BarrierState.Waiting)
        {
            foreach (BarrierTrigger trigger in barrierTriggers)
            {
                trigger.Enable();
            }

            foreach (BarrierFortifier fortifier in barrierFortifiers)
            {
                fortifier.SetWaiting();
            }

            return;
        }
    }

    public void SignalDeath()
    {
        enemiesLeft--;
        CheckState();
    }

    public void SignalIntruder()
    {
        if( barrierState == BarrierState.Waiting )
        {
            ChangeState(BarrierState.Active);
        }
    }
}

public enum BarrierState
{
    Waiting, //Yellow
    Active,  //Red
    Broken   //Green
}
